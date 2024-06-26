using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class PresetArena1 : MonoBehaviour
{
    [SerializeField] GameObject spawnPos;
    [SerializeField] CharacterController CharacterCtrl;

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
        //PlayerController.canMove = false;
        //player.transform.position = spawnPos.transform.position;
        //Arena2Script.isPlayerSpawned = true;
        //StartCoroutine(movementDelay());

        GameObject player = GameObject.FindWithTag("Player");

        Debug.Log("Player position before teleport: " + player.transform.position);
        Debug.Log("spawnPos reference: " + spawnPos.name);
        Debug.Log("Spawn position: " + spawnPos.transform.position);

        CharacterCtrl.enabled = false;
        PlayerController playerCtrl = FindObjectOfType<PlayerController>();
        playerCtrl.enabled = false;
        player.transform.position = spawnPos.transform.position;
        
        Debug.Log("Player position after teleport: " + player.transform.position);

        Arena2Script.isPlayerSpawned = true;
        StartCoroutine(movementDelay());
    }

    IEnumerator movementDelay()
    {
        yield return new WaitForSeconds(0.1f);
        CharacterCtrl.enabled = true;
        PlayerController playerCtrl = FindObjectOfType<PlayerController>();
        playerCtrl.enabled = true;
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
