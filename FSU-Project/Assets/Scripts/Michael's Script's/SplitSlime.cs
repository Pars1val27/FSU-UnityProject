using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SplitSlime : MonoBehaviour ,IDamage
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
    [SerializeField] Transform attackPos;
    [SerializeField] GameObject attack;
    [SerializeField] float attackRate;

    Vector3 playerDir;
    Vector3 playerPos;

    float angleToPlayer;
    float SavedTime = 0;

    bool playerInRange;
    bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SphereCollider>().radius = agent.stoppingDistance;
        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.green;
        }
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
    }
    public void TakeDamage(int amount)
    {
        HP -= amount;
        Debug.Log("got hit");
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
    public void Punch()
    {
        Instantiate(attack, attackPos.position, transform.rotation);
        isAttacking = false;
    }
}
