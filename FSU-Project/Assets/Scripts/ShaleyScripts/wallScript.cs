using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    GameObject wall;
    static GameObject toDestroy;
    public bool hasCollisionWall;
    public bool hasCollisionDoor;

    private void Start()
    {
        mapScript.instance.UpdateDoors(mapScript.instance.maps[0]);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            hasCollisionWall = true;
            Debug.Log("Other in collsion: " + other.gameObject);
            toDestroy = other.gameObject;
        }
        else if(other.gameObject.CompareTag("Door"))
        {
            //Destroy(this.gameObject);
            Debug.Log("Other in collsion: " + other.gameObject);
            hasCollisionDoor = true;
        }
        else
        {
            //do nothing
        }
    }

    //public static void UpdateDoors(maps mapLevel)
    //{
    //    Debug.Log("Update Doors Called");
    //    int doorCount = 0;
    //    for (int wallIndex = 0; wallIndex < mapScript.instance.roomWalls.Length; wallIndex++)
    //    {
    //        //Debug.Log("Wall object: " + roomWalls[wallIndex].wall);
    //        if (mapScript.instance.roomWalls[wallIndex].GetComponent<wallScript>().hasCollisionWall)
    //        {
    //            if (doorCount == 0)
    //            {
    //                AddDoor(mapLevel, mapScript.instance.roomWalls[wallIndex]);
    //                doorCount++;
    //            }
    //            else
    //            {
    //                int chance = RandChance();
    //                if (chance == 0)
    //                {
    //                    AddDoor(mapLevel, mapScript.instance.roomWalls[wallIndex]);
    //                    doorCount++;
    //                }
    //                else
    //                {
    //                    //do nothing
    //                }
    //            }
    //        }
    //        else if (mapScript.instance.roomWalls[wallIndex].GetComponent<wallScript>().hasCollisionDoor)
    //        {
    //            doorCount++;
    //            Destroy(mapScript.instance.roomWalls[wallIndex]);
    //        }
    //        else
    //        {
    //            //no collision do nothing
    //        }
    //    }
    //}

    public static void AddDoor(maps mapLevel, GameObject wall)
    {
        GameObject door = Instantiate(mapLevel.door);
        door.transform.position = wall.transform.position;
        door.transform.rotation = wall.transform.rotation;
        Destroy(toDestroy);
        Destroy(wall);
    }

    //static int RandChance()
    //{
    //    int chance = UnityEngine.Random.Range(0, 2);
    //    if (chance == lastChance)
    //    {
    //        chance = UnityEngine.Random.Range(0, 2);
    //    }
    //    lastChance = chance;
    //    return chance;
    //}
}
