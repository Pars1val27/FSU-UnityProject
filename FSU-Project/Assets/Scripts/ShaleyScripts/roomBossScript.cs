using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomBossScript : MonoBehaviour
{
    [SerializeField] Transform[] spawnPosBoss;
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
        if (other.CompareTag("Player"))
        {
            Debug.Log("player entered room");
            for (int posIndex = 0; posIndex < spawnPosBoss.Length; posIndex++)
            {
                Debug.Log("spawned enemy");
                RandEnemyBoss(spawnPosBoss[posIndex]);
                UIManager.instance.bossHealth.SetActive(true);
            }
            collisionOccured = true;
        }
        Debug.Log("trigger finished");
    }

    void RandEnemyBoss(Transform spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.bosses[mapScript.mapLevel.level - 1].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.bosses[mapScript.mapLevel.level - 1][chosenEnemy],
        spawnPos.transform.position, spawnPos.transform.rotation);
    }
}
