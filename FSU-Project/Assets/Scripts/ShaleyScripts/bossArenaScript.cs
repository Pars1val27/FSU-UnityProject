using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossArenaScript : MonoBehaviour
{
    [SerializeField] Transform spawnPos;
    void Start()
    {
        RandEnemyBoss(spawnPos);
    }
    void RandEnemyBoss(Transform spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.bosses[mapManager.instance.mapLevel.level - 1].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.bosses[mapManager.instance.mapLevel.level - 1][chosenEnemy],
        spawnPos.transform.position, spawnPos.transform.rotation);
    }
}
