using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemy : MonoBehaviour , IDamage, IFireDamage, ISlow, IPoisonDamage, IFreeze
{
    [Header("----- Health -----")]
    [SerializeField] int HP;
    [SerializeField] GameObject HealthBar;

    [Header("----- AI -----")]
    [SerializeField] int faceTaregtSpeed;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject timeDrop;

    [Header("----- Animation's -----")]
    [SerializeField] Renderer[] model;
    [SerializeField] Animator anim;
    [SerializeField] int animTranSpeed;
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] ParticleSystem SpawnEffect;

    [Header("----- Attack -----")]
    [SerializeField] Transform attackPos;
    [SerializeField] GameObject attack;
    [SerializeField] float attackRate;
    [SerializeField] GameObject splitSlime;

    Vector3 playerDir;
    Vector3 playerPos;

    float angleToPlayer;
    float SavedTime = 0;
    float StartHP;

    bool playerInRange;
    bool isAttacking;

    bool isSlowed = false;
    bool isFrozen = false;

    float originalSpeed;
    float origAttackRate;

    // Start is called before the first frame update
    void Start()
    {
        StartHP = HP;
        Instantiate(SpawnEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.green;
        }
        transform.GetComponent<SphereCollider>().radius = agent.stoppingDistance;
        UIManager.instance.UpdateEnemyDisplay(1);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            float agentSpeed = agent.velocity.normalized.magnitude;
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agentSpeed, Time.deltaTime * animTranSpeed));
            playerPos = EnemyManager.instance.player.transform.position;
            playerDir = playerPos - transform.position;
            angleToPlayer = Vector3.Angle(playerDir, transform.forward);
            Quaternion rot = Quaternion.LookRotation(-new Vector3(playerDir.x, 0, playerDir.z));
            HealthBar.transform.rotation = rot;
            agent.SetDestination(playerPos);
            if ((Time.time - SavedTime) > attackRate && !isAttacking)
            {

                SavedTime = Time.time;
                if (playerInRange)
                {
                    isAttacking = true;

                    anim.SetTrigger("Melee");
                }

            }
            if (playerInRange)
            {
                faceTarget();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;

        StartCoroutine(flashDamage());

        if (HP <= 0)
        {
            Death();
        }
    }

    // Status Effect  Implementaion
    public void ApplyFireDamage(int fireDamage, float duration, GameObject fireEffect)
    {
        //Debug.Log("brute current Hp" + HP);
        GameObject FireEffect = Instantiate(fireEffect, transform.position, Quaternion.identity, transform);
        FireEffect.transform.SetParent(transform);
        StartCoroutine(FireDamageCoroutine(fireDamage, duration, FireEffect));
    }

    IEnumerator FireDamageCoroutine(int fireDamage, float duration, GameObject FireEffect)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            //Debug.Log("brute Hp" + HP);
            TakeDamage(fireDamage);
            elapsed += 1f;
            //Debug.Log(fireDamage + " FireDamage");
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
            //Debug.Log("brute Hp" + HP);

            TakeDamage(PosionDamage);
            elapsed += 1f;
            //Debug.Log(PosionDamage + " PoisonDamage");
            yield return new WaitForSeconds(1f);

        }
        Destroy(PoisonEffect);
    }

   

    public void ApplySlow(float slowAmount, float duration, GameObject slowEffect)
    {
        if (!isSlowed)
        {
            isSlowed = true;

            //Debug.Log(agent.speed + " normal speed");

            agent.speed *= slowAmount;
            attackRate *= slowAmount;
            //Debug.Log(agent.speed + " Slow Speed Slow Start ");

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

       // Debug.Log(agent.speed + " Slow end normal speed");

    }

    public void ApplyFreeze(float duration, GameObject freezeEffect)
    {
        if (!isFrozen)
        {
           // Debug.Log(gameObject.name + " Enemy Frozen");
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
       // Debug.Log(gameObject.name + " Enemy Unfrozen");
        Destroy(FreeezEffect);
        isFrozen = false;
    }

    //status effect end
    
    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTaregtSpeed);
    }

    public void Death()
    {
        Instantiate(splitSlime, transform.position + new Vector3(4,0,0), transform.rotation);
        Instantiate(splitSlime, transform.position + new Vector3(4, 0, 0), transform.rotation);
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Instantiate(timeDrop, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation);
        UIManager.instance.UpdateEnemyDisplay(-1);
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
            model[i].material.color = Color.green;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("in range");
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("not in range");
            playerInRange = false;
        }
    }
    public void Punch()
    {
        Debug.Log("attacked");
        Instantiate(attack, attackPos.position, transform.rotation);
        isAttacking = false;
    }
    
  
}
