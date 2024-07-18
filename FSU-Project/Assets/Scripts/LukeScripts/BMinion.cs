using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BMinion : MonoBehaviour, IDamage
{
    //Luke

    [Header("----- AI -----")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] Animator anim;
    [SerializeField] int animTranSpeed;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] ParticleSystem deathEffect;


    [Header("----- Attack -----")]
    [SerializeField] int damage;
    [SerializeField] float attackRate;
    [SerializeField] GameObject sting;
    IDamage dmg;

    bool isAttacking;
    bool playerInRange;
    float SavedTime = 0;
    Vector3 playerDir;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
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
        faceTarget();
        agent.SetDestination(playerPos);
        if ((Time.time - SavedTime) > attackRate && !isAttacking)
        {

            SavedTime = Time.time;
            if (playerInRange)
            {
                isAttacking = true;
                SavedTime = Time.time;
                anim.SetTrigger("Attack");
            }

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

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }

    public void TakeDamage(int amount)
    {
        Death();
    }

    public void Death()
    {
        Instantiate(deathEffect, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), transform.rotation);
        Destroy(gameObject);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }

    public void Sting()
    {
        sting.SetActive(true);
        StartCoroutine(wait());
        isAttacking = false;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        sting.SetActive(false);
    }
}
