using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour , IDamage
{
    [Header("----- Health -----")]
    [SerializeField] int HP;

    [Header("----- Model -----")]
    [SerializeField] Renderer[] model;

    [Header("----- Spawn Settings ----")]
    [SerializeField] Transform[] spawnPOS;
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject[] Enemys;

    bool isSpawning;

   
    void Update()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        int Randenemy = Random.Range(0, Enemys.Length);
        int RandPOS = Random.Range(0,spawnPOS.Length);
        Instantiate(Enemys[Randenemy], spawnPOS[RandPOS].position, spawnPOS[RandPOS].rotation);
        
        yield return new WaitForSeconds(spawnInterval);
        isSpawning = false;
    }
    public void TakeDamage(int amount)
    {
        HP -= amount;

        StartCoroutine(flashDamage());

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator flashDamage()
    {
        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.white;
        }

    }
}
