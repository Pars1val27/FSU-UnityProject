using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

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

    //void SpawnPlayer()
    //{
    //    /*  GameObject player = GameObject.FindWithTag("Player");*/

    //    //PlayerController playerCtrl = FindObjectOfType<PlayerController>();
    //    //CharacterController characterCtrl = FindObjectOfType<CharacterController>();
    //    //playerCtrl.enabled = false;
    //    //characterCtrl.enabled = false;

    //    /*  Debug.Log("Player reference: " + player);*/
    //    /*    player.transform.position = new UnityEngine.Vector3(spawnPos.transform.position.x,spawnPos.transform.position.y,spawnPos.transform.position.z);*/
    //    StartCoroutine(Spawn());
    //    Arena2Script.isPlayerSpawned = true;
    //    //characterCtrl.enabled = true;
    //    //playerCtrl.enabled = true;
    //}

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
            SpawnEnemy();
        }
    }

    IEnumerator SpawnPlayer()
    {
        //Michael
        Arena2Script.isPlayerSpawned = true;
        GameObject player = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(0.1f);
        player.transform.position = new UnityEngine.Vector3(spawnPos.transform.position.x, spawnPos.transform.position.y, spawnPos.transform.position.z);
    }
}
