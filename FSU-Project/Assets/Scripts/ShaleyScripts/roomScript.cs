using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPos;
    [SerializeField] GameObject[] spawnPosHard;
    [SerializeField] GameObject[] spawnPosStationary;
    bool collisionOccured;
    //private void Start()
    //{
    //    spawnPosAll.Add(spawnPos);
    //    spawnPosAll.Add(spawnPosHard);
    //    spawnPosAll.Add(spawnPosStationary);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (collisionOccured) 
        { 
            return; 
        }
        if(other == CompareTag("Player"))
        {
            for (int posIndex = 0; posIndex < spawnPos.Length; posIndex++)
            {
                RandEnemy(spawnPos[posIndex]);
            }
            for (int posIndex = 0; posIndex < spawnPosHard.Length; posIndex++)
            {
                RandEnemyHard(spawnPos[posIndex]);
            }
            for (int posIndex = 0; posIndex < spawnPosStationary.Length; posIndex++)
            {
                RandEnemyStationary(spawnPos[posIndex]);
            }
        }
    }

    void RandEnemy(GameObject spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemies[mapScript.mapLevel.level].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.enemies[mapScript.mapLevel.level][chosenEnemy]);
        enemy.transform.position = spawnPos.transform.position;
        enemy.transform.rotation = spawnPos.transform.rotation;
    }
    void RandEnemyHard(GameObject spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemiesHard[mapScript.mapLevel.level].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.enemiesHard[mapScript.mapLevel.level][chosenEnemy]);
        enemy.transform.position = spawnPos.transform.position;
        enemy.transform.rotation = spawnPos.transform.rotation;
    }
    void RandEnemyStationary(GameObject spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemiesStationary[mapScript.mapLevel.level].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.enemiesStationary[mapScript.mapLevel.level][chosenEnemy]);
        enemy.transform.position = spawnPos.transform.position;
        enemy.transform.rotation = spawnPos.transform.rotation;
    }
}
