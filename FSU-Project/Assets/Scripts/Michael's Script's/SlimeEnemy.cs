using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemy : MonoBehaviour , IDamage
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

    [Header("----- Attack -----")]
    [SerializeField] Transform attackPos;
    [SerializeField] GameObject attack;
    [SerializeField] float attackRate;
    [SerializeField] GameObject splitSlime;

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

        if (HP <= 0)
        {

            anim.SetTrigger("Death");
        }
    }
    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTaregtSpeed);
    }

    public void Death()
    {
        Instantiate(splitSlime, transform.position + new Vector3(0,-4,0), transform.rotation);
        Instantiate(splitSlime, transform.position + new Vector3(0, 4, 0), transform.rotation);
        StartCoroutine(GetDestroyed());
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
    
    IEnumerator GetDestroyed()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
}
