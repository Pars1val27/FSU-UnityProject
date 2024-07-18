using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemy : MonoBehaviour , IDamage
{
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
    public void TakeDamage(int amount)
    {
        HP -= amount;
        Debug.Log("got hit");
        StartCoroutine(flashDamage());
        HealthBar.transform.localScale = new Vector3(HP / StartHP * 2, HealthBar.transform.localScale.y, transform.transform.localScale.z);
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
        Instantiate(splitSlime, transform.position + new Vector3(4,0,0), transform.rotation);
        Instantiate(splitSlime, transform.position + new Vector3(4, 0, 0), transform.rotation);
        Instantiate(deathEffect, transform.position, transform.rotation);
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
