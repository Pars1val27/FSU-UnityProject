using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class maps : ScriptableObject
{
    [SerializeField] public GameObject roomSpawn;
    [SerializeField] public GameObject roomShop;
    [SerializeField] public GameObject roomItem;
    [SerializeField] public GameObject[] rooms;
    [SerializeField] public GameObject[] roomBosses;
    [SerializeField] public int maxRooms;

    [SerializeField] public GameObject wall;
    [SerializeField] public GameObject door;
}
