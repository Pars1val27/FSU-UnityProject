using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetBoss1 : MonoBehaviour
{
    //[SerializeField] CharacterController characterCtrl;
    [SerializeField] GameObject bossPos;
    [SerializeField] GameObject spawnPos;

    private void Start()
    {
        StartArenaEnemy();
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(RandEnemy(), new UnityEngine.Vector3(
            bossPos.transform.position.x, bossPos.transform.position.y, bossPos.transform.position.z),
            new UnityEngine.Quaternion(bossPos.transform.rotation.x, bossPos.transform.rotation.y,
            bossPos.transform.rotation.z, bossPos.transform.rotation.w));
        EnemyManager.instance.FindPlayer();
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
        StartCoroutine(SpawnPlayer());
        
        StartCoroutine(SpawnEnemy());

        UIManager.instance.StartBoss();
    }
}
