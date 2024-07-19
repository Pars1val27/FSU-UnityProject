using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour , IDamage
{
    //Michael

    [Header("----- Health -----")]
    [SerializeField] int HP;
    [SerializeField] GameObject HealthBar;

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
    [SerializeField] Transform[] shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] int shootAngle;
    [SerializeField] float shootRate;

    Vector3 playerDir;
    Vector3 playerPos;

   

    float angleToPlayer;
    float SavedTime = 0;
    float StartHP;

    bool isshooting;
    bool canSeePlayer;
    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        StartHP = HP;
        Instantiate(SpawnEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
        transform.GetComponent<SphereCollider>().radius = agent.stoppingDistance;
        UIManager.instance.UpdateEnemyDisplay(1);

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = Quaternion.LookRotation(-new Vector3(playerDir.x, 0, playerDir.z));
        HealthBar.transform.rotation = rot;
        float agentSpeed = agent.velocity.normalized.magnitude;
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agentSpeed, Time.deltaTime * animTranSpeed));
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);

     

        RaycastHit hit;


        if ((Time.time - SavedTime) > shootRate && angleToPlayer < shootAngle && !isshooting)
        {

            SavedTime = Time.time;
            if (playerInRange && canSeePlayer)
            {
               
                isshooting = true;
                anim.SetTrigger("Shoot");
            }

        }

        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y +1,transform.position.z), playerDir, out hit))
        {
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
        HealthBar.transform.localScale = new Vector3(HP / StartHP * 2, HealthBar.transform.localScale.y, transform.transform.localScale.z);
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
        Instantiate(deathEffect,new Vector3(transform.position.x,transform.position.y + 3,transform.position.z),transform.rotation);
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
        for (int i = 0; i < shootPos.Length; i++)
        {
            float randAngle = 0;
            if (i > 0)
            {
                randAngle = Random.Range(-1, 1);
            }
            Instantiate(projectile, shootPos[i].position, new Quaternion(transform.rotation.x + randAngle,transform.rotation.y + randAngle,transform.rotation.z,transform.rotation.w));
        }
        isshooting = false;
    }
}
