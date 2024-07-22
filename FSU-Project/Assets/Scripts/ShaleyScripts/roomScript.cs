using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class roomScript : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoss;
    [SerializeField] Transform[] spawnPosHard;
    [SerializeField] Transform[] spawnPosStationary;
    [SerializeField] GameObject doors;
    [SerializeField] GameObject doorColliders;
    [SerializeField] float doorMoveDist;
    [SerializeField] float doorMoveSpeed;
    bool collisionOccured;
    bool doorsReopened;
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
            //Debug.Log("collisionOccured");
            return; 
        }
        if(other.CompareTag("Player"))
        {
            //Debug.Log("player entered room");
            for (int posIndex = 0; posIndex < spawnPoss.Length; posIndex++)
            {
                //Debug.Log("spawned enemy");
                RandEnemy(spawnPoss[posIndex]);
            }
            for (int posIndex = 0; posIndex < spawnPosHard.Length; posIndex++)
            {
                RandEnemyHard(spawnPosHard[posIndex]);
            }
            for (int posIndex = 0; posIndex < spawnPosStationary.Length; posIndex++)
            {
                RandEnemyStationary(spawnPosStationary[posIndex]);
            }
            doorColliders.SetActive(true);
            doors.transform.localPosition += new Vector3(0, doors.transform.localPosition.y - doorMoveDist, 0);
            collisionOccured = true;
        }
        //Debug.Log("trigger finished");
    }

    private void OnTriggerStay (Collider other)
    {
        if (doorsReopened == false && other.CompareTag("Player"))
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        if (UIManager.instance.enemyCount <= 0)
        {
            //doors.transform.localPosition += new Vector3(0, Mathf.Lerp(1, -doorMoveDist, doorMoveSpeed * Time.deltaTime), 0);
            doors.transform.localPosition += new Vector3(0, doors.transform.localPosition.y + doorMoveDist, 0);
            doorColliders.SetActive(false);
            doorsReopened = true;
        }
    }

    void RandEnemy(Transform spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemies[mapManager.instance.mapLevel.level-1].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.enemies[mapManager.instance.mapLevel.level-1][chosenEnemy], 
        spawnPos.transform.position, spawnPos.transform.rotation);
    }
    void RandEnemyHard(Transform spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemiesHard[mapManager.instance.mapLevel.level-1].Length);
        GameObject enemy = Instantiate(EnemyManager.instance.enemiesHard[mapManager.instance.mapLevel.level-1][chosenEnemy],
        spawnPos.transform.position, spawnPos.transform.rotation);
    }
    void RandEnemyStationary(Transform spawnPos)
    {
        int chosenEnemy = UnityEngine.Random.Range(0, EnemyManager.instance.enemiesStationary[mapManager.instance.mapLevel.level-1].Length);
        Debug.Log("Chosen enemy num: " + chosenEnemy);
        GameObject enemy = Instantiate(EnemyManager.instance.enemiesStationary[mapManager.instance.mapLevel.level-1][chosenEnemy],
        spawnPos.transform.position, spawnPos.transform.rotation);
        Debug.Log("Stationary enemy length: " + EnemyManager.instance.enemiesStationary[mapManager.instance.mapLevel.level - 1].Length);
        //Debug.Log("Chosen enemy: " + enemy);
    }
}
