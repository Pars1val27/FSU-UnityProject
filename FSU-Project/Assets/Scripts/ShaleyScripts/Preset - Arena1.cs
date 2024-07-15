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

    /*private void Start()
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
    }*/
}
