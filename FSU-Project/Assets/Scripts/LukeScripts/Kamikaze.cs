using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kamikaze : MonoBehaviour, IDamage
{
    //Luke
    [Header("----- Health -----")]
    [SerializeField] int HP;

    [Header("----- AI -----")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] Animator anim;
    [SerializeField] int animTranSpeed;
    [SerializeField] ParticleSystem spark;
    [SerializeField] ParticleSystem spawnEffect;
    [SerializeField] ParticleSystem explodeEffect;
    [SerializeField] Renderer model;


    [Header("----- Attack -----")]
    [SerializeField] int damage;
    [SerializeField] int attackRate;
    IDamage dmg;

    bool isAttacking;
    bool playerInRange;
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
        agent.SetDestination(playerPos);
    }
    private void OnTriggerEnter(Collider other)
    {
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

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;

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
        dmg.TakeDamage(damage);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
}
