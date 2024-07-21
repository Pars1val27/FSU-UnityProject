using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class turretFixed : MonoBehaviour, IDamage
{
    //Luke
    [Header("----- Health -----")]
    [SerializeField] int HP;
    [SerializeField] GameObject healthBar;

    [Header("----- AI -----")]
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] ParticleSystem spark;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] GameObject swivel;
    


    [Header("----- Attack -----")]
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] float shootRate;
    [SerializeField] float rotateSpeed;

    bool isshooting;
    float StartHP;

    Vector3 playerDir;
    Vector3 playerPos;

    void Start()
    {
        StartHP = HP;
        Instantiate(spawnEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
        UIManager.instance.UpdateEnemyDisplay(1);
    }

    private void Update()
    {
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        Quaternion rot = Quaternion.LookRotation(-new Vector3(playerDir.x, 0, playerDir.z));
        healthBar.transform.rotation = rot;
        swivel.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        if (!isshooting)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        isshooting = true;
        yield return new WaitForSeconds(shootRate);
        Instantiate(projectile, shootPos.position, new Quaternion(swivel.transform.rotation.x, swivel.transform.rotation.y, swivel.transform.rotation.z, swivel.transform.rotation.w));
        isshooting = false;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        healthBar.transform.localScale = new Vector3(HP / StartHP * 2, healthBar.transform.localScale.y, transform.transform.localScale.z);
        flashDamage();

        if (HP <= 0)
        {
            Death();
        }
    }

    void flashDamage()
    {
        Instantiate(spark, transform.position, transform.rotation);
    }

    public void Death()
    {
        Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation);
        Destroy(gameObject);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
}
