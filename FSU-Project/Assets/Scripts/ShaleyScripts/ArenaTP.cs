using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTP : MonoBehaviour
{
    [SerializeField] Arena2Script arena;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            arena.StartArena();
            Arena2Script.isPlayerSpawned = false;
        }
    }
}
