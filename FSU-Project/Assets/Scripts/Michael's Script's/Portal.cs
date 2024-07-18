using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour , IDamage
{
    [Header("----- Health -----")]
    [SerializeField] int HP;

    [Header("----- Model -----")]
    [SerializeField] Renderer[] model;
    [SerializeField] ParticleSystem DeathEffect;
    [SerializeField] ParticleSystem HitEffect;

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



        if (HP <= 0)
        {
            Destroy(gameObject);
            Instantiate(DeathEffect, new Vector3(transform.position.x, transform.position.y , transform.position.z), transform.rotation);
        }
        else
        {
            Instantiate(HitEffect, new Vector3(transform.position.x, transform.position.y , transform.position.z), transform.rotation);
        }

    }
    
}
