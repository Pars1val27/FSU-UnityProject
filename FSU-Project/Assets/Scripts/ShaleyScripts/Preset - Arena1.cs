using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class PresetArena1 : MonoBehaviour
{
    [SerializeField] GameObject spawnPos;
    //[SerializeField] CharacterController characterCtrl;

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
        GameObject player = GameObject.FindWithTag("Player");

        //PlayerController playerCtrl = FindObjectOfType<PlayerController>();
        //CharacterController characterCtrl = FindObjectOfType<CharacterController>();
        //playerCtrl.enabled = false;
        //characterCtrl.enabled = false;
        player.transform.position = spawnPos.transform.position;

        Arena2Script.isPlayerSpawned = true;
        //characterCtrl.enabled = true;
        //playerCtrl.enabled = true;
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
