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
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1);
        Instantiate(RandEnemy(), spawnPos.transform);
    }

    

    GameObject RandEnemy()
    {
        int enemy = UnityEngine.Random.Range(1, EnemyManager.instance.enemies.Length);
        return EnemyManager.instance.enemies[enemy];
    }

    public void StartArenaEnemy()
    {
        if (!Arena2Script.isPlayerSpawned)
        {
            StartCoroutine(SpawnPlayer());
        }
        else
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnPlayer()
    {
        Arena2Script.isPlayerSpawned = true;
        GameObject player = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(0.1f);
        player.transform.position = new UnityEngine.Vector3(spawnPos.transform.position.x, spawnPos.transform.position.y, spawnPos.transform.position.z);
    }
}
