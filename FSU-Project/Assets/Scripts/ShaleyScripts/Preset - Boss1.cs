using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetBoss1 : MonoBehaviour
{
    [SerializeField] GameObject bossPos;
    [SerializeField] GameObject spawnPos;

    private void Start()
    {
        StartArenaEnemy();
    }
    void SpawnEnemy()
    {
        Instantiate(RandEnemy(), new Vector3(bossPos.transform.position.x, bossPos.transform.position.y, bossPos.transform.position.z),
            new Quaternion(bossPos.transform.rotation.x, bossPos.transform.rotation.y,
            bossPos.transform.rotation.z, bossPos.transform.rotation.w));
    }

    GameObject RandEnemy()
    {
        int enemy = UnityEngine.Random.Range(0, EnemyManager.instance.bosses.Length);
        return EnemyManager.instance.bosses[enemy];
    }

    public void StartArenaEnemy()
    {
        gameManager.instance.SpawnPlayer(spawnPos);

        SpawnEnemy();

        UIManager.instance.StartBoss();
    }
}
