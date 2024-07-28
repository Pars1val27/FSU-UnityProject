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
    bool isSlowed = false;
    bool isFrozen = false;
    float originalSpeed;
    float origAttackRate;

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
        if (!isFrozen)
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
    }

    // Status Effect  Implementaion
    public void ApplyFireDamage(int fireDamage, float duration, GameObject fireEffect)
    {
        Debug.Log("brute current Hp" + HP);
        GameObject FireEffect = Instantiate(fireEffect, transform.position, Quaternion.identity, transform);
        FireEffect.transform.SetParent(transform);
        StartCoroutine(FireDamageCoroutine(fireDamage, duration, FireEffect));
    }

    IEnumerator FireDamageCoroutine(int fireDamage, float duration, GameObject FireEffect)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Debug.Log("brute Hp" + HP);
            TakeDamage(fireDamage);
            elapsed += 1f;
            Debug.Log(fireDamage + " FireDamage");
            yield return new WaitForSeconds(1f);

        }
        Destroy(FireEffect);
    }
    public void ApplyPoisonDamage(int PosionDamage, float duration, GameObject poisonEffect)
    {
        GameObject PoisonEffect = Instantiate(poisonEffect, transform.position, Quaternion.identity, transform);
        PoisonEffect.transform.SetParent(transform);
        StartCoroutine(PoisonDamageCoroutine(PosionDamage, duration, PoisonEffect));
    }


    private IEnumerator PoisonDamageCoroutine(int PosionDamage, float duration, GameObject PoisonEffect)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Debug.Log("brute Hp" + HP);

            TakeDamage(PosionDamage);
            elapsed += 1f;
            Debug.Log(PosionDamage + " PoisonDamage");
            yield return new WaitForSeconds(1f);

        }
        Destroy(PoisonEffect);
    }



    public void ApplySlow(float slowAmount, float duration, GameObject slowEffect)
    {
        if (!isSlowed)
        {
            isSlowed = true;

            Debug.Log(agent.speed + " normal speed");

            agent.speed = slowAmount;
            shootRate = slowAmount;
            Debug.Log(agent.speed + " Slow Speed Slow Start ");

            GameObject SlowEffect = Instantiate(slowEffect, transform.position, Quaternion.identity, transform);
            SlowEffect.transform.SetParent(transform);
            SlowEffect.transform.localScale = Vector3.one;
            StartCoroutine(SlowCoroutine(duration, SlowEffect));
        }
    }
    private IEnumerator SlowCoroutine(float duration, GameObject SlowEffect)
    {

        yield return new WaitForSeconds(duration);
        RemoveSlow();
        Destroy(SlowEffect);
    }
    public void RemoveSlow()
    {
        isSlowed = false;

        agent.speed = originalSpeed;
        shootRate = origAttackRate;

        Debug.Log(agent.speed + " Slow end normal speed");

    }




    public void ApplyFreeze(float duration, GameObject freezeEffect)
    {
        if (!isFrozen)
        {
            Debug.Log(gameObject.name + " Enemy Frozen");
            isFrozen = true;
            GameObject FreezeEffect = Instantiate(freezeEffect, transform.position, Quaternion.identity, transform);
            FreezeEffect.transform.SetParent(transform);
            StartCoroutine(FreezeCoroutine(duration, FreezeEffect));
        }
    }

    private IEnumerator FreezeCoroutine(float duration, GameObject FreeezEffect)
    {

        agent.speed = 0f;

        yield return new WaitForSeconds(duration);
        agent.speed = originalSpeed;
        Debug.Log(gameObject.name + " Enemy Unfrozen");
        Destroy(FreeezEffect);
        isFrozen = false;
    }

    //Status Effect Implementation end

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
        healthBar.transform.localScale = new Vector3(HP / StartHP * 50, healthBar.transform.localScale.y, transform.transform.localScale.z);
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
