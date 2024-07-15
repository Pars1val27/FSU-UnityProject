using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public static maps mapLevel;
    Vector3 pos;
    Vector3[] usedRoomPos;
    List<GameObject[]> usedWallPosRoom;
    List<GameObject> usedWallPos;

void Start()
    {
        mapLevel = maps[0];
        usedRoomPos = new Vector3[mapLevel.maxRooms];
        usedWallPosRoom = new List<GameObject[]>();
        usedWallPos = new List<GameObject>();
        GameObject player = GameObject.FindWithTag("Player");
        pos = new Vector3(player.transform.position.x, 0, player.transform.position.y);
        GenerateMap(mapLevel);
        gameManager.instance.surface.BuildNavMesh();
        UpdateDoors(mapLevel);
    }

    void NextPos(maps mapLevel)
    {
        for (int posIndex = 0; posIndex < usedRoomPos.Length; posIndex++)
        {
            if (pos == usedRoomPos[posIndex])
            {
                float moveDist = GetRoomWidth(mapLevel) * 2;
                int dir = RandDir();
                Vector3 newPos = pos;
                if (dir == 0)
                {
                    newPos = new Vector3(moveDist, 0, 0);
                }
                else if (dir == 90)
                {
                    newPos = new Vector3(0, 0, moveDist);
                }
                else if (dir == -90)
                {
                    newPos = new Vector3(0, 0, -moveDist);
                }
                else
                {
                    newPos = new Vector3(-moveDist, 0, 0);
                }
                pos += newPos;
                NextPos(mapLevel);
            }
            else
            {
                //do nothing
            }
        }
    }

    float GetRoomWidth(maps mapLevel)
    {
        return (mapLevel.rooms[0].transform.localScale.x) * 7.5f;
    }
    void GenerateMap(maps mapLevel)
    {
        //Vector3[] usedPos = new Vector3[mapLevel.maxRooms];
        //-3 for mandatory rooms
        GenerateRoomSpawn(mapLevel);
        usedRoomPos[roomCount - 1] = pos;
        GenerateRoomWalls(mapLevel);
        while (roomCount < mapLevel.maxRooms - 3)
        {
            NextPos(mapLevel);
            GenerateRoom(mapLevel);
            usedRoomPos[roomCount - 1] = pos;
            GenerateRoomWalls(mapLevel);
        }
        //While less than maxRooms(for adding mandatory rooms, pick rand used pos and nextPos
        //spawn shop and do for each, also add code to have the first one be spawn
        PickRandPos();
        NextPos(mapLevel);
        GenerateRoomShop(mapLevel);
        usedRoomPos[roomCount - 1] = pos;
        GenerateRoomWalls(mapLevel);

        PickRandPos();
        NextPos(mapLevel);
        GenerateRoomItem(mapLevel);
        usedRoomPos[roomCount - 1] = pos;
        GenerateRoomWalls(mapLevel);

        PickRandPos();
        NextPos(mapLevel);
        GenerateRoomBoss(mapLevel);
        usedRoomPos[roomCount - 1] = pos;
        GenerateRoomWalls(mapLevel);
    }

    void PickRandPos()
    {
        int posIndex = UnityEngine.Random.Range(0, usedRoomPos.Length);
        pos = usedRoomPos[posIndex];
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
        roomCount++;
    }

    void GenerateRoomBoss(maps mapLevel)
    {
        GameObject room = Instantiate(RandRoomBoss(mapLevel));
        room.transform.localPosition = pos;
        int dir = RandDir();
        room.transform.localEulerAngles = new Vector3(0, dir, 0);
        roomCount++;
    }

    void GenerateRoomShop(maps mapLevel)
    {
        GameObject room = Instantiate(RandRoomShop(mapLevel));
        room.transform.localPosition = pos;
        int dir = RandDir();
        room.transform.localEulerAngles = new Vector3(0, dir, 0);
        roomCount++;
    }
    void GenerateRoomItem(maps mapLevel)
    {
        GameObject room = Instantiate(RandRoomItem(mapLevel));
        room.transform.localPosition = pos;
        int dir = RandDir();
        room.transform.localEulerAngles = new Vector3(0, dir, 0);
        roomCount++;
    }
    void GenerateRoomSpawn(maps mapLevel)
    {
        GameObject room = Instantiate(RandRoomSpawn(mapLevel));
        room.transform.localPosition = pos;
        int dir = RandDir();
        room.transform.localEulerAngles = new Vector3(0, dir, 0);
        roomCount++;
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
    GameObject RandRoomBoss(maps mapLevel)
    {
        int room = UnityEngine.Random.Range(0, mapLevel.roomBosses.Length);
        if (lastRoom == room)
        {
            room = UnityEngine.Random.Range(0, mapLevel.roomBosses.Length);
        }
        lastRoom = room;
        return mapLevel.roomBosses[room];
    }
    GameObject RandRoomShop(maps mapLevel)
    {
        int room = UnityEngine.Random.Range(0, mapLevel.roomShops.Length);
        if (lastRoom == room)
        {
            room = UnityEngine.Random.Range(0, mapLevel.roomShops.Length);
        }
        lastRoom = room;
        return mapLevel.roomShops[room];
    }
    GameObject RandRoomItem(maps mapLevel)
    {
        int room = UnityEngine.Random.Range(0, mapLevel.roomItems.Length);
        if (lastRoom == room)
        {
            room = UnityEngine.Random.Range(0, mapLevel.roomItems.Length);
        }
        lastRoom = room;
        return mapLevel.roomItems[room];
    }
    GameObject RandRoomSpawn(maps mapLevel)
    {
        int room = UnityEngine.Random.Range(0, mapLevel.roomSpawns.Length);
        if (lastRoom == room)
        {
            room = UnityEngine.Random.Range(0, mapLevel.roomSpawns.Length);
        }
        lastRoom = room;
        return mapLevel.roomSpawns[room];
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
                //if(doorCount > 1)
                //{
                //    int chance = UnityEngine.Random.Range(0, 4);
                //    if(chance > 0)
                //    {
                //        int doorToDestroy = PickDoor(mapLevel, usedWallPosRoom[currRoom]);
                //        Debug.Log("doorToDestroy: " + doorToDestroy);
                //        if(doorToDestroy == 5)
                //        {
                //            break;
                //        }
                //        GameObject wall = Instantiate(mapLevel.wall);
                //        wall.transform.position = usedWallPosRoom[currRoom][doorToDestroy].transform.position;
                //        wall.transform.rotation = usedWallPosRoom[currRoom][doorToDestroy].transform.rotation;
                //        Destroy(usedWallPosRoom[currRoom][doorToDestroy]);
                //        usedWallPosRoom[currRoom][doorToDestroy] = wall;
                //        doorCount--;
                //    }
                //}
            }
            currRoom++;
        }
    }

    //int PickDoor(maps mapLevel, GameObject[] usedWallPosRoom)
    //{
    //    int doorSpot1 = 5;
    //    int doorSpot2 = 5;
    //    for(int wallIndex = 0; wallIndex < usedWallPosRoom.Length; wallIndex++)
    //    {
    //        if (usedWallPosRoom[wallIndex].GetPrefabDefinition() == mapLevel.door.GetPrefabDefinition())
    //        {
    //            if(doorSpot1 == 5)
    //            {
    //                doorSpot1 = wallIndex;
    //            }
    //            else 
    //            {
    //                doorSpot2 = wallIndex;
    //            }
    //        }
    //    }
    //    int chance = UnityEngine.Random.Range(0, 2);
    //    if(chance == 0)
    //    {
    //        return doorSpot1;
    //    }
    //    else
    //    {
    //        return doorSpot2;
    //    }
    //}

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