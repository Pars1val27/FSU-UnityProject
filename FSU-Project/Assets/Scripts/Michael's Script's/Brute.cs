using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brute : MonoBehaviour , IDamage, IFireDamage, IPoisonDamage, ISlow, IFreeze
{

    [Header("----- Health -----")]
    [SerializeField] int HP;

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
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] int shootAngle;
    
    [SerializeField] Transform attackPos;
    [SerializeField] GameObject attack;
    [SerializeField] float attackRate;

    Vector3 playerDir;
    Vector3 playerPos;
    Vector3 prevNavPos;



    float angleToPlayer;
    float SavedTime = 0;

    bool isshooting;
    bool canSeePlayer;
    bool playerInRange;
    bool isAttacking;

    //Slow And Freeze logic
    bool isSlowed = false;
    bool isFrozen = false;

    float originalSpeed;
    float origAttackRate;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(SpawnEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
        transform.GetComponent<SphereCollider>().radius = agent.stoppingDistance;
        UIManager.instance.UpdateEnemyDisplay(1);

        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.white;
        }
        
        originalSpeed = agent.speed;
        origAttackRate = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            float agentSpeed = agent.velocity.normalized.magnitude;
            float distance = Vector3.Distance(transform.position, playerPos);
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agentSpeed, Time.deltaTime * animTranSpeed));
            playerPos = EnemyManager.instance.player.transform.position;
            playerDir = playerPos - transform.position;
            angleToPlayer = Vector3.Angle(playerDir, transform.forward);



            RaycastHit hit;


            if ((Time.time - SavedTime) > attackRate)
            {
                SavedTime = Time.time;

                if (distance < 10 && !isAttacking)
                {
                    isAttacking = false;
                    anim.SetTrigger("Melee");
                }
                else if (!isAttacking && canSeePlayer)
                {
                    isAttacking = true;
                    anim.SetTrigger("Shoot");
                }
            }

            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), playerDir);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), playerDir, out hit))
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("Player") && playerInRange)
                {

                    canSeePlayer = true;
                    agent.SetDestination(transform.position);
                    faceTarget();
                }
                else
                {
                    canSeePlayer = false;
                    agent.SetDestination(playerPos);
                }


            }
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
            Debug.Log( gameObject.name +" Hp " + HP);

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

           // Debug.Log(agent.speed + " normal speed");

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

        //Debug.Log(agent.speed + " Slow end normal speed");

    }




    public void ApplyFreeze(float duration, GameObject freezeEffect)
    {
        if (!isFrozen)
        {
            //Debug.Log(gameObject.name + " Enemy Frozen");
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
        //Debug.Log(gameObject.name + " Enemy Unfrozen");
        Destroy(FreeezEffect);
        isFrozen = false;
    }

    //status effect end

    public void TakeDamage(int amount)
    {
        HP -= amount;

        StartCoroutine(flashDamage());

        if (HP <= 0)
        {
            Death();
        }
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTaregtSpeed);
    }

    public void Death()
    {
        Instantiate(deathEffect, new Vector3(transform.position.x,transform.position.y +5,transform.position.z), transform.rotation);
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
            model[i].material.color = Color.white;
        }

    }
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
    public void shoot()
    {
        Instantiate(projectile, shootPos.position, transform.rotation);
        isAttacking = false;
       
     }

    public void punch()
    {
        
        Instantiate(attack, attackPos.position, transform.rotation);
        isAttacking = false;
        

    }
}
