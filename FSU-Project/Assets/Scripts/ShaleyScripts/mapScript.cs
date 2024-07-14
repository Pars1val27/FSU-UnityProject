using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    //the array position is map level - 1
    [SerializeField] maps[] maps;
    //public static mapScript instance;

    int lastDir;
    int lastRoom;
    int lastChance;
    int roomCount = 0;
    //wallScript currRoom;
    //public GameObject[] roomWalls;
    maps mapLevel;
    Vector3 pos;
    List<GameObject[]> usedWallPosRoom;
    List<GameObject> usedWallPos;

void Start()
    {
        mapLevel = maps[0];
        usedWallPosRoom = new List<GameObject[]>();
        usedWallPos = new List<GameObject>();
        GameObject player = GameObject.FindWithTag("Player");
        pos = new Vector3(player.transform.position.x, 0, player.transform.position.y);
        GenerateMap(mapLevel);
        UpdateDoors(mapLevel);
    }

    void NextPos(maps mapLevel)
    {
        float moveDist = GetRoomWidth(mapLevel) * 2;
        int dir = RandDir();
        Vector3 newPos = pos;
        if(dir == 0)
        {
            newPos = new Vector3(moveDist, 0, 0);
        }
        else if(dir == 90)
        {
            newPos = new Vector3(0, 0, moveDist);
        }
        else if(dir == -90)
        {
            newPos = new Vector3(0, 0, -moveDist);
        }
        else
        {
            newPos = new Vector3(-moveDist, 0, 0);
        }
        pos = pos + newPos;
    }

    float GetRoomWidth(maps mapLevel)
    {
        return (mapLevel.rooms[0].transform.localScale.x) * 7.5f;
    }
    void GenerateMap(maps mapLevel)
    {
        Vector3[] usedPos = new Vector3[mapLevel.maxRooms];
        //-3 for mandatory rooms
        while (roomCount < mapLevel.maxRooms)
        {
            bool isTaken = false;
            for(int posIndex = 0; posIndex < usedPos.Length; posIndex++)
            {
                if(pos == usedPos[posIndex])
                {
                    isTaken = true;
                    break;
                }
                else
                {
                    //do nothing
                }
            }
            if (isTaken)
            {
                //do Nothing
            }
            else
            {
                GenerateRoom(mapLevel);
                GenerateRoomWalls(mapLevel);
                roomCount++;
                usedPos[roomCount - 1] = pos;
            }
            NextPos(mapLevel);
        }
        //While less than maxRooms(for adding mandatory rooms, pick rand used pos
    }

    void GenerateRoomWalls(maps mapLevel)
    {
        GameObject[] roomWalls = new GameObject[4];
        float roomWidth = GetRoomWidth(mapLevel);
        float wallPos = roomWidth;
        //sides
        roomWalls[0] = GenerateWall(mapLevel, 90, wallPos, 0);
        roomWalls[1] = GenerateWall(mapLevel, 90, -wallPos, 0);
        //top/bottom
        roomWalls[2] = GenerateWall(mapLevel, 0, 0, wallPos);
        roomWalls[3] = GenerateWall(mapLevel, 0, 0, -wallPos);
        usedWallPosRoom.Add(roomWalls);
    }

    GameObject GenerateWall(maps mapLevel, int dir, float x, float z)
    {
        GameObject wall = Instantiate(mapLevel.wall);
        Debug.Log("Wall Object: " + wall);
        float wallHeight = (wall.transform.localScale.y) / 2;
        wall.transform.localPosition = new Vector3(x, wallHeight, z) + pos;
        wall.transform.localEulerAngles = new Vector3(0, dir, 0);
        usedWallPos.Add(wall);
        return wall;
    }

    void GenerateRoom(maps mapLevel)
    {
        GameObject room = Instantiate(RandRoom(mapLevel));
        room.transform.localPosition = pos;
        int dir = RandDir();
        room.transform.localEulerAngles = new Vector3(0, dir, 0);
    }

    //normal room
    GameObject RandRoom(maps mapLevel)
    {
        int room = UnityEngine.Random.Range(0, mapLevel.rooms.Length);
        if(lastRoom == room)
        {
            room = UnityEngine.Random.Range(0, mapLevel.rooms.Length);
        }
        lastRoom = room;
        return mapLevel.rooms[room];
    }

    int RandDir()
    {
        int dir = UnityEngine.Random.Range(-1, 3);
        if (lastDir == dir)
        {
            dir = UnityEngine.Random.Range(-1, 3);
        }
        lastDir = dir;
        return dir * 90;
    }

    void UpdateDoors(maps mapLevel)
    {
        int currRoom = 0;
        while(currRoom < usedWallPosRoom.Count) {
            int doorCount = 0;
            for (int wallIndex = 0; wallIndex < 4; wallIndex++)
            {
                int matchCount = 0;
                for (int otherWallIndex = 0; otherWallIndex < usedWallPos.Count; otherWallIndex++)
                {
                    if (usedWallPosRoom[currRoom][wallIndex].transform.position == usedWallPos[otherWallIndex].transform.position 
                        && usedWallPosRoom[currRoom][wallIndex].GetInstanceID() != usedWallPos[otherWallIndex].GetInstanceID())
                    {
                        matchCount++;
                        if(matchCount > 0)
                        {
                            GameObject door = AddDoor(mapLevel, usedWallPos[otherWallIndex]);
                            doorCount++;
                            Destroy(usedWallPosRoom[currRoom][wallIndex]);
                            Destroy(usedWallPos[otherWallIndex]);
                            usedWallPos[otherWallIndex] = door;
                            usedWallPosRoom[currRoom][wallIndex] = door;
                            break;
                        }
                    }
                }
                if(doorCount > 1)
                {
                    int chance = UnityEngine.Random.Range(0, 2);
                    if(chance == 0)
                    {
                        int doorToDestroy = PickDoor(mapLevel, usedWallPosRoom[currRoom]);
                        if(doorToDestroy == 5)
                        {
                            break;
                        }
                        GameObject wall = Instantiate(mapLevel.wall);
                        wall.transform.position = usedWallPosRoom[currRoom][doorToDestroy].transform.position;
                        wall.transform.rotation = usedWallPosRoom[currRoom][doorToDestroy].transform.rotation;
                        Destroy(usedWallPosRoom[currRoom][doorToDestroy]);
                        usedWallPosRoom[currRoom][doorToDestroy] = wall;
                    }
                }
            }
            currRoom++;
        }
    }

    int PickDoor(maps mapLevel, GameObject[] usedWallPosRoom)
    {
        int doorSpot1 = 5;
        int doorSpot2 = 5;
        for(int wallIndex = 0; wallIndex < usedWallPosRoom.Length; wallIndex++)
        {
            if (usedWallPosRoom[wallIndex] == mapLevel.door)
            {
                if(doorSpot1 == 5)
                {
                    doorSpot1 = wallIndex;
                }
                else 
                {
                    doorSpot2 = wallIndex;
                }
            }
        }
        int chance = UnityEngine.Random.Range(0, 2);
        if(chance == 0)
        {
            return doorSpot1;
        }
        else
        {
            return doorSpot2;
        }
    }
    //void UpdateDoors(maps mapLevel)
    //{
    //    Debug.Log("Update Doors Called");
    //    int doorCount = 0;
    //    for (int wallIndex = 0; wallIndex < roomWalls.Length; wallIndex++)
    //    {
    //        Debug.Log("hasCollisionWall = " + roomWalls[wallIndex].GetComponent<wallScript>().hasCollisionWall);
    //        Debug.Log("hasCollisionDoor = " + roomWalls[wallIndex].GetComponent<wallScript>().hasCollisionDoor);
    //        if (roomWalls[wallIndex].GetComponent<wallScript>().hasCollisionWall)
    //        {
    //            if (doorCount == 0)
    //            {
    //                wallScript.AddDoor(mapLevel, roomWalls[wallIndex]);
    //                Destroy(roomWalls[wallIndex]);
    //                doorCount++;
    //            }
    //            else
    //            {
    //                int chance = RandChance();
    //                if (chance == 0)
    //                {
    //                    wallScript.AddDoor(mapLevel, roomWalls[wallIndex]);
    //                    Destroy(roomWalls[wallIndex]);
    //                    doorCount++;
    //                }
    //                else
    //                {
    //                    //do nothing
    //                }
    //            }
    //        }
    //        else if (roomWalls[wallIndex].GetComponent<wallScript>().hasCollisionDoor)
    //        {
    //            doorCount++;
    //            Destroy(roomWalls[wallIndex]);
    //        }
    //        else
    //        {
    //            //no collision do nothing
    //        }
    //    }
    //    Debug.Log("Update Doors End");
    //}

    GameObject AddDoor(maps mapLevel, GameObject wall)
    {
        GameObject door = Instantiate(mapLevel.door);
        door.transform.position = wall.transform.position;
        door.transform.rotation = wall.transform.rotation;
        return door;
        //Destroy(wall);
    }

    int RandChance()
    {
        int chance = UnityEngine.Random.Range(0, 2);
        if (chance == lastChance)
        {
            chance = UnityEngine.Random.Range(0, 2);
        }
        lastChance = chance;
        return chance;
    }
}