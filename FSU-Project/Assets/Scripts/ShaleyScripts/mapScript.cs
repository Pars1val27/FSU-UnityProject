using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    //the array position is map level - 1
    [SerializeField] public maps[] maps;
    public static mapScript instance;

    int lastDir;
    int lastRoom;
    int lastChance;
    int roomCount = 0;
    //wallScript currRoom;
    public GameObject[] roomWalls;
    Vector3 pos;

    private void Awake()
    {
        instance = this;
    }
void Start()
    {
        roomWalls = new GameObject[4];
        GameObject player = GameObject.FindWithTag("Player");
        pos = new Vector3(player.transform.position.x, 0, player.transform.position.y);
        //GenerateRoomWalls(maps[0]);
        GenerateMap(maps[0]);
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
        float roomWidth = GetRoomWidth(mapLevel);
        float wallPos = roomWidth;
        //sides
        roomWalls[0] = GenerateWall(mapLevel, 90, wallPos, 0);
        roomWalls[1] = GenerateWall(mapLevel, 90, -wallPos, 0);
        //top/bottom
        roomWalls[2] = GenerateWall(mapLevel, 0, 0, wallPos);
        roomWalls[3] = GenerateWall(mapLevel, 0, 0, -wallPos);
        //UpdateDoors(mapLevel);
    }

    GameObject GenerateWall(maps mapLevel, int dir, float x, float z)
    {
        GameObject wall = Instantiate(mapLevel.wall);
        float wallHeight = (wall.transform.localScale.y) / 2;
        wall.transform.localPosition = new Vector3(x, wallHeight, z) + pos;
        wall.transform.localEulerAngles = new Vector3(0, dir, 0);
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

    public void UpdateDoors(maps mapLevel)
    {
        Debug.Log("Update Doors Called");
        int doorCount = 0;
        for (int wallIndex = 0; wallIndex < mapScript.instance.roomWalls.Length; wallIndex++)
        {
            //Debug.Log("Wall object: " + roomWalls[wallIndex].wall);
            if (mapScript.instance.roomWalls[wallIndex].GetComponent<wallScript>().hasCollisionWall)
            {
                if (doorCount == 0)
                {
                    wallScript.AddDoor(mapLevel, mapScript.instance.roomWalls[wallIndex]);
                    doorCount++;
                }
                else
                {
                    int chance = RandChance();
                    if (chance == 0)
                    {
                        wallScript.AddDoor(mapLevel, mapScript.instance.roomWalls[wallIndex]);
                        doorCount++;
                    }
                    else
                    {
                        //do nothing
                    }
                }
            }
            else if (roomWalls[wallIndex].GetComponent<wallScript>().hasCollisionDoor)
            {
                doorCount++;
                Destroy(mapScript.instance.roomWalls[wallIndex]);
            }
            else
            {
                //no collision do nothing
            }
        }
    }

    //void AddDoor(maps mapLevel, GameObject wall)
    //{
    //    GameObject door = Instantiate(mapLevel.door);
    //    door.transform.position = wall.transform.position;
    //    door.transform.rotation = wall.transform.rotation;
    //    Destroy(wall);
    //}

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