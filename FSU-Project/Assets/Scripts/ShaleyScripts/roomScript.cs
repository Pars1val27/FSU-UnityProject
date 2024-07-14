using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPos;
    [SerializeField] GameObject[] spawnPosHard;
    [SerializeField] GameObject[] spawnPosStationary;
    List<GameObject[]> spawnPosAll;
    private void Start()
    {
        spawnPosAll.Add(spawnPos);
        spawnPosAll.Add(spawnPosHard);
        spawnPosAll.Add(spawnPosStationary);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == CompareTag("Player"))
        {
            for(int arrayIndex = 0; arrayIndex < spawnPosAll.Count; arrayIndex++)
            {
                for(int spawnIndex = 0; spawnIndex < spawnPosAll[arrayIndex].Length; spawnIndex++)
                {

                }
            }
        }
    }

    void RandEnemy(GameObject spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.)
        GameObject enemy 
    }
}
