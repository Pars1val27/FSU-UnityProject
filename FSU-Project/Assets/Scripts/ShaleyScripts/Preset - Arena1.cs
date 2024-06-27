using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
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
        Instantiate(RandEnemy(), new UnityEngine.Vector3(
            spawnPos.transform.position.x, spawnPos.transform.position.y, spawnPos.transform.position.z), 
            new UnityEngine.Quaternion(spawnPos.transform.rotation.x, spawnPos.transform.rotation.y, 
            spawnPos.transform.rotation.z, spawnPos.transform.rotation.w));
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
        int enemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemies.Length);
        return EnemyManager.instance.enemies[enemy];
    }

    public void StartArenaEnemy()
    {
        if (!Arena2Script.isPlayerSpawned)
        {
            gameManager.instance.SpawnPlayer(spawnPos);
            Arena2Script.isPlayerSpawned = true;
            
        }
        else
        {
            SpawnEnemy();
        }
    }
}
