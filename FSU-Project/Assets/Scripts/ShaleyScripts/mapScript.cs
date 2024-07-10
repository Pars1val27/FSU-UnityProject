using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    //the array position is map level - 1
    [SerializeField] maps[] maps;

    int lastDir;
    int roomWidth;

    void Start()
    {

    }

    void Update()
    {

    }

    void GenerateMap(ScriptableObject mapLevel)
    {

    }

    void GenerateRoomWalls()
    {
        
    }

    //normal room
    void GenerateRoom()
    {
        
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