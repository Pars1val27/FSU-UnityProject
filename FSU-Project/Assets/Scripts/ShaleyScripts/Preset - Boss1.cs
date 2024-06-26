using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetBoss1 : MonoBehaviour
{
    //[SerializeField] CharacterController characterCtrl;

    private void Start()
    {
        StartArenaEnemy();
    }
    void SpawnEnemy()
    {
        Instantiate(RandEnemy(), this.transform);
    }

    IEnumerator SpawnPlayer()
    {
        //Michael
        GameObject player = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(0.1f);
        player.transform.position = new UnityEngine.Vector3(0, 0, 0);
    }

    GameObject RandEnemy()
    {
        int enemy = UnityEngine.Random.Range(1, EnemyManager.instance.enemies.Length);
        return EnemyManager.instance.enemies[enemy];
    }

    public void StartArenaEnemy()
    {
        SpawnEnemy();
        StartCoroutine(SpawnPlayer());
    }
}
