using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetBoss1 : MonoBehaviour
{
    //[SerializeField] CharacterController characterCtrl;

    private void Start()
    {
        StartArenaEnemy();
    }
    void SpawnEnemy()
    {
        Instantiate(RandEnemy(), this.transform);
    }

    void SpawnPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        //PlayerController playerCtrl = FindObjectOfType<PlayerController>();
        //playerCtrl.enabled = false;
        player.transform.position = new Vector3(0, 0, -4);
        //BossArena1.isPlayerSpawned = true;
        //playerCtrl.enabled = true;
    }

    GameObject RandEnemy()
    {
        int enemy = UnityEngine.Random.Range(1, EnemyManager.instance.enemies.Length);
        return EnemyManager.instance.enemies[enemy];
    }

    public void StartArenaEnemy()
    {
        SpawnEnemy();
        SpawnPlayer();
    }
}
