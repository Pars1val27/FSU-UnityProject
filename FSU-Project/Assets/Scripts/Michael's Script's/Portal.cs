using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour , IDamage
{
    [Header("----- Health -----")]
    [SerializeField] int HP;
    [SerializeField] GameObject HealthBar;

    [Header("----- Model -----")]
    [SerializeField] Renderer[] model;
    [SerializeField] ParticleSystem DeathEffect;
    [SerializeField] ParticleSystem HitEffect;

    [Header("----- Spawn Settings ----")]
    [SerializeField] Transform[] spawnPOS;
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject[] Enemys;

    bool isSpawning;
    float StartHP;

    Vector3 playerDir;
    Vector3 playerPos;
    void Start()
    {
       
        StartHP = HP;
    }
    void Update()
    {
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        
        Quaternion rot = Quaternion.LookRotation(-new Vector3(playerDir.x, 0, playerDir.z));
        HealthBar.transform.rotation = rot;

        faceTarget();

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
    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 180);
    }
    public void TakeDamage(int amount)
    {
        HP -= amount;


        HealthBar.transform.localScale = new Vector3(HP / StartHP * 2, HealthBar.transform.localScale.y, transform.transform.localScale.z);
        if (HP <= 0)
        {
            Destroy(gameObject);
            Instantiate(DeathEffect, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(HitEffect, transform.position, transform.rotation);
        }

    }
    
}
