using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class maps : ScriptableObject
{
    [SerializeField] public GameObject[] roomSpawns;
    [SerializeField] public GameObject[] roomShops;
    [SerializeField] public GameObject[] roomItems;
    [SerializeField] public GameObject[] rooms;
    //boss tp rooms
    [SerializeField] public GameObject[] roomBosses;
    //boss fight rooms
    [SerializeField] public GameObject[] bossArenas;
    [SerializeField] public int maxRooms;
    [SerializeField] public int level;

    [SerializeField] public GameObject wall;
    [SerializeField] public GameObject door;
}
