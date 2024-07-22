using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BMother : MonoBehaviour, IDamage
{
    //Luke

    [Header("----- Health -----")]
    [SerializeField] int HP;
    [SerializeField] GameObject healthBar;

    [Header("----- AI -----")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] Animator anim;
    [SerializeField] int animTranSpeed;
    [SerializeField] Renderer model;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] GameObject timeDrop;


    [Header("----- Attack -----")]
    [SerializeField] Transform[] spawnPos;
    [SerializeField] GameObject spawn;
    [SerializeField] float spawnRate;

    Vector3 playerDir;
    Vector3 playerPos;

    bool isshooting;
    bool canSeePlayer;
    bool playerInRange;
    float StartHP;
    bool isSlowed = false;
    bool isFrozen = false;
    float originalSpeed;
    float origAttackRate;

    void Start()
    {
        StartHP = HP;
        Instantiate(spawnEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
        transform.GetComponent<SphereCollider>().radius = agent.stoppingDistance;
        UIManager.instance.UpdateEnemyDisplay(1);
    }
    private void Update()
    {
        if (!isFrozen)
        {
            Quaternion rot = Quaternion.LookRotation(-new Vector3(playerDir.x, 0, playerDir.z));
            healthBar.transform.rotation = rot;
            float agentSpeed = agent.velocity.normalized.magnitude;
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agentSpeed, Time.deltaTime * animTranSpeed));
            playerPos = EnemyManager.instance.player.transform.position;
            playerDir = playerPos - transform.position;
            faceTarget();
            agent.SetDestination(transform.position -= transform.forward * Time.deltaTime * agent.speed);
            if (!playerInRange)
            {
                agent.SetDestination(playerPos);
            }
            else
            {
                agent.SetDestination(transform.position -= transform.forward * Time.deltaTime * agent.speed);
            }
            if (!isshooting)
            {
                StartCoroutine(Spawn());
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
            spawnRate = slowAmount;
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
        spawnRate = origAttackRate;

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
        anim.enabled = false;

        yield return new WaitForSeconds(duration);
        anim.enabled = true;
        agent.speed = originalSpeed;
        model.material.color = Color.white;
        Debug.Log(gameObject.name + " Enemy Unfrozen");
        Destroy(FreeezEffect);
        isFrozen = false;
    }

    //Status Effect Implementation end

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
    }

    IEnumerator Spawn()
    {
        isshooting = true;
        yield return new WaitForSeconds(spawnRate);
        anim.SetTrigger("Attack");
        isshooting = false;
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        healthBar.transform.localScale = new Vector3(HP / StartHP * 2, healthBar.transform.localScale.y, transform.transform.localScale.z);
        StartCoroutine(flashDamage());

        if (HP <= 0)
        {
            Death();
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }

    public void Death()
    {
        Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation);
        Destroy(gameObject);
        Instantiate(timeDrop, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), transform.rotation);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }

    public void Sting()
    {
        for (int i = 0; i < spawnPos.Length; i++)
            Instantiate(spawn, spawnPos[i].position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
    }
}
