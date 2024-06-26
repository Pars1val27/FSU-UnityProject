using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class PresetArena1 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawnPos;


    private void Start()
    {
        StartArenaEnemy();
    }
    void SpawnEnemy()
    {
        Instantiate(RandEnemy(), spawnPos.transform);
    }

    void SpawnPlayer()
    {
        player.transform.position = spawnPos.transform.position;
        Arena2Script.isPlayerSpawned = true;
    }
    GameObject RandEnemy()
    {
        int enemy = UnityEngine.Random.Range(1, EnemyManager.instance.enemies.Length);
        return EnemyManager.instance.enemies[enemy];
    }

    public void StartArenaEnemy()
    {
        if (Arena2Script.isPlayerSpawned)
        {
            SpawnEnemy();
        }
        else
        {
            SpawnPlayer();
        }
    }
}
