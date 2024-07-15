using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoss;
    [SerializeField] Transform[] spawnPosHard;
    [SerializeField] Transform[] spawnPosStationary;
    bool collisionOccured;
    //private void Start()
    //{
    //    spawnPosAll.Add(spawnPos);
    //    spawnPosAll.Add(spawnPosHard);
    //    spawnPosAll.Add(spawnPosStationary);
    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("in room");
        if (collisionOccured) 
        {
            Debug.Log("collisionOccured");
            return; 
        }
        if(other.CompareTag("Player"))
        {
            Debug.Log("player entered room");
            for (int posIndex = 0; posIndex < spawnPoss.Length; posIndex++)
            {
                Debug.Log("spawned enemy");
                RandEnemy(spawnPoss[posIndex]);
            }
            for (int posIndex = 0; posIndex < spawnPosHard.Length; posIndex++)
            {
                RandEnemyHard(spawnPoss[posIndex]);
            }
            for (int posIndex = 0; posIndex < spawnPosStationary.Length; posIndex++)
            {
                RandEnemyStationary(spawnPoss[posIndex]);
            }
            collisionOccured = true;
        }
        Debug.Log("trigger finished");
    }

    void RandEnemy(Transform spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemies[mapScript.mapLevel.level-1].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.enemies[mapScript.mapLevel.level-1][chosenEnemy], 
        spawnPos.transform.position, spawnPos.transform.rotation);
    }
    void RandEnemyHard(Transform spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemiesHard[mapScript.mapLevel.level-1].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.enemiesHard[mapScript.mapLevel.level-1][chosenEnemy],
        spawnPos.transform.position, spawnPos.transform.rotation);
    }
    void RandEnemyStationary(Transform spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemiesStationary[mapScript.mapLevel.level-1].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.enemiesStationary[mapScript.mapLevel.level-1][chosenEnemy],
        spawnPos.transform.position, spawnPos.transform.rotation);
    }
}
