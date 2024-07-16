using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brute : MonoBehaviour , IDamage
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
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SphereCollider>().radius = agent.stoppingDistance;
        UIManager.instance.UpdateEnemyDisplay(1);

        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float agentSpeed = agent.velocity.normalized.magnitude;
        float distance = Vector3.Distance(transform.position, playerPos);
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agentSpeed, Time.deltaTime * animTranSpeed));
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);



        RaycastHit hit;


       if((Time.time-SavedTime) > attackRate)
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
            Debug.Log(hit.collider.tag);
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
