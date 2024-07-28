using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kamikaze : MonoBehaviour, IDamage
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
    [SerializeField] ParticleSystem spark;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] ParticleSystem explodeEffect;
    [SerializeField] GameObject timeDrop;
    


    [Header("----- Attack -----")]
    [SerializeField] int damage;
    [SerializeField] float attackRate;

    IDamage dmg = null;
    bool isAttacking;
    bool playerInRange;
    float StartHP;
    bool isSlowed = false;
    bool isFrozen = false;
    float originalSpeed;
    float origAttackRate;
    Vector3 playerDir;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        StartHP = HP;
        Instantiate(spawnEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
        transform.GetComponent<SphereCollider>().radius = agent.stoppingDistance;
        UIManager.instance.UpdateEnemyDisplay(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            Quaternion rot = Quaternion.LookRotation(-new Vector3(playerDir.x, 0, playerDir.z));
            healthBar.transform.rotation = rot;
            float agentSpeed = agent.velocity.normalized.magnitude;
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agentSpeed, Time.deltaTime * animTranSpeed));
            playerPos = EnemyManager.instance.player.transform.position;
            playerDir = playerPos - transform.position;
            agent.SetDestination(playerPos);
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
            attackRate = slowAmount;
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
        attackRate = origAttackRate;

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
    private void OnTriggerEnter(Collider other)
    {
        playerInRange = true;
        if (other.tag == "Player")
        {

            dmg = other.GetComponent<IDamage>();

            if (dmg != null)
            {
                agent.isStopped = true;
                anim.SetTrigger("Attack");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        healthBar.transform.localScale = new Vector3(HP / StartHP * 2, healthBar.transform.localScale.y, transform.transform.localScale.z);
        flashDamage();

        if (HP <= 0)
        {
            anim.SetTrigger("Attack");
        }
    }

    void flashDamage()
    {
        Instantiate(spark, transform.position, transform.rotation);
    }

    public void Explode()
    {
        Instantiate(explodeEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Instantiate(timeDrop, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation);
        if (dmg != null && playerInRange)
            dmg.TakeDamage(damage);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
}
