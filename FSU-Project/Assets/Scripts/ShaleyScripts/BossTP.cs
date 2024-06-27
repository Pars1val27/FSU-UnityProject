using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTP : MonoBehaviour
{
    [SerializeField] BossArena1 arena;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            arena.StartArena();
            Arena2Script.isPlayerSpawned = false;
        }
    }
}
