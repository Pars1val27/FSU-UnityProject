using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    //the array position is map level - 1
    [SerializeField] maps[] maps;

    int lastDir;
    int lastRoom;
    Vector3 pos;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pos = new Vector3(player.transform.position.x, 0, player.transform.position.y);
        GenerateRoomWalls(maps[0]);
        GenerateRoom(maps[0]);
        NextPos(maps[0]);
        GenerateRoomWalls(maps[0]);
        GenerateRoom(maps[0]);
    }

    void Update()
    {

    }

    void NextPos(maps mapLevel)
    {
        float moveDist = GetRoomWidth(mapLevel) * 2.5f;
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

    }

    void GenerateRoomWalls(maps mapLevel)
    {
        float roomWidth = GetRoomWidth(mapLevel);
        float wallPos = roomWidth;
        //sides
        GenerateWall(mapLevel, 90, wallPos, 0);
        GenerateWall(mapLevel, 90, -wallPos, 0);
        //top/bottom
        GenerateWall(mapLevel, 0, 0, wallPos);
        GenerateWall(mapLevel, 0, 0, -wallPos);
    }

    void GenerateWall(maps mapLevel, int dir, float x, float z)
    {
        float wallHeight = (mapLevel.wall.transform.localScale.y) / 2;
        GameObject wall = Instantiate(mapLevel.wall);
        wall.transform.localPosition = new Vector3(x, wallHeight, z) + pos;
        wall.transform.localEulerAngles = new Vector3(0, dir, 0);
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
}