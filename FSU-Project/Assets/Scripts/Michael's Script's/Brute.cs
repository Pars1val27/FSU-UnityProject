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
    bool isSlowed = false;
    bool isFrozen = false;

    //Slow And Freeze logic
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
    public void ApplyFireDamage(int fireDamage, float duration)
    {
        Debug.Log("brute current Hp" + HP);
        StartCoroutine(FireDamageCoroutine(fireDamage, duration));
    }

    IEnumerator FireDamageCoroutine(int fireDamage, float duration)
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
    }
    public void ApplyPoisonDamage(int PosionDamage, float duration)
    {
        StartCoroutine(PoisonDamageCoroutine(PosionDamage, duration));
    }


    private IEnumerator PoisonDamageCoroutine(int PosionDamage, float duration)
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
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        
        StartCoroutine(flashDamage(Color.red));

        if (HP <= 0)
        {
            Death();
        }
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        if (!isSlowed)
        {
            isSlowed = true;
            
            Debug.Log(agent.speed + " normal speed");

            agent.speed /= slowAmount;
            attackRate /= slowAmount;
            Debug.Log(agent.speed + " Slow Speed Slow Start ");

            StartCoroutine(SlowCoroutine(duration));
        }
    }

    public void RemoveSlow()
    {
        isSlowed = false;

        agent.speed = originalSpeed;
        attackRate = origAttackRate;
        Debug.Log(agent.speed + " Slow end normal speed");

    }
    private IEnumerator SlowCoroutine(float duration)
    {

        yield return new WaitForSeconds(duration);
        RemoveSlow();
    }

    

    public void ApplyFreeze(float duration)
    {
        if (!isFrozen)
        {
            Debug.Log(gameObject.name + " Enemy Frozen");
            isFrozen = true;
            for (int i = 0; i < model.Length; i++)
            {
                model[i].material.color = Color.blue;
            }
            StartCoroutine(FreezeCoroutine(duration));
        }
    }

    private IEnumerator FreezeCoroutine(float duration)
    {

        agent.speed = 0f;
        anim.enabled = false;
        
            yield return new WaitForSeconds(duration);
        anim.enabled = true;
        agent.speed = originalSpeed;
        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.white;
        }
        Debug.Log(gameObject.name + " Enemy Unfrozen");
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
        Instantiate(deathEffect, new Vector3(transform.position.x,transform.position.y +5,transform.position.z), transform.rotation);
        Destroy(gameObject);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
    IEnumerator flashDamage(Color input)
    {

        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = input;
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
