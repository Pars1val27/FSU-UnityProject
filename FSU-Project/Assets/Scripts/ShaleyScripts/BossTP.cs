using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mapManager.instance.DestroyMap();
            Vector3 pos = new Vector3(other.transform.position.x - 50, other.transform.position.y - 3, other.transform.position.z);
            Instantiate(RandRBossArena(mapManager.instance.mapLevel), pos, new Quaternion(0, 0, 0, 1));
        }
    }

    GameObject RandRBossArena(maps mapLevel)
    {
        int room = UnityEngine.Random.Range(0, mapLevel.bossArenas.Length);
        return mapLevel.bossArenas[room];
    }
}
