using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetBoss1 : MonoBehaviour
{
    //[SerializeField] CharacterController characterCtrl;
    [SerializeField] GameObject spawnPos;

    private void Start()
    {
        StartArenaEnemy();
    }
    void SpawnEnemy()
    {
        Instantiate(RandEnemy(), new Vector3(0,0,0), new Quaternion(0,0,0,0));
    }

    IEnumerator SpawnPlayer()
    {
        Arena2Script.isPlayerSpawned = true;
        GameObject player = GameObject.FindWithTag("Player");
        GameObject temp = player;
        yield return new WaitForSeconds(0.1f);
        Instantiate(player, new UnityEngine.Vector3(
            spawnPos.transform.position.x, spawnPos.transform.position.y, spawnPos.transform.position.z),
            new UnityEngine.Quaternion(spawnPos.transform.rotation.x, spawnPos.transform.rotation.y,
            spawnPos.transform.rotation.z, spawnPos.transform.rotation.w));
        Destroy(temp);
    }

    GameObject RandEnemy()
    {
        int enemy = UnityEngine.Random.Range(0, EnemyManager.instance.bosses.Length);
        return EnemyManager.instance.bosses[enemy];
    }

    public void StartArenaEnemy()
    {
        SpawnEnemy();
        StartCoroutine(SpawnPlayer());
    }
}
