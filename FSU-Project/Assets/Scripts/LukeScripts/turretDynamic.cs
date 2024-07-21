using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class turretDynamic : MonoBehaviour, IDamage
{
    //Luke

    [Header("----- Health -----")]
    [SerializeField] int HP;
    [SerializeField] GameObject healthBar;

    [Header("----- AI -----")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject timeDrop;

    [Header("----- Animation's -----")]
    [SerializeField] Animator anim;
    [SerializeField] Renderer[] model;
    [SerializeField] GameObject swivel;
    [SerializeField] ParticleSystem spark;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] ParticleSystem deathEffect;


    [Header("----- Attack -----")]
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] float shootRate;

    Vector3 playerDir;
    Vector3 playerPos;

    bool isshooting;
    float StartHP;

    void Start()
    {
        StartHP = HP;
        Instantiate(spawnEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
        UIManager.instance.UpdateEnemyDisplay(1);
    }

    private void Update()
    {
        Quaternion rot = Quaternion.LookRotation(-new Vector3(playerDir.x, 0, playerDir.z));
        healthBar.transform.rotation = rot;
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        faceTarget();
        if (!isshooting)
        {
            StartCoroutine(shoot());
        }
        
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        swivel.transform.rotation = Quaternion.Lerp(swivel.transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }

    IEnumerator shoot()
    {
        isshooting = true;
        yield return new WaitForSeconds(shootRate);
        anim.SetTrigger("Shot");
        Instantiate(projectile, shootPos.position, new Quaternion(shootPos.transform.rotation.x, shootPos.transform.rotation.y, shootPos.transform.rotation.z, shootPos.transform.rotation.w));
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
        Instantiate(timeDrop, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
}
