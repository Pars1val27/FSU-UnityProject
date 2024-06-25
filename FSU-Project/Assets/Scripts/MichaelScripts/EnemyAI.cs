using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamage
{
    //Michael
    [SerializeField] int HP;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int faceTaregtSpeed;
    [SerializeField] Animator anim;
    [SerializeField] int animTranSpeed;

    int currHP;
    bool playerInRange;

    Vector3 playerDir;
    // Start is called before the first frame update
    void Start()
    {
        
        UIManager.instance.UpdateEnemyDisplay(1);
    }

    // Update is called once per frame
    void Update()
    {
        float agentSpeed = agent.velocity.normalized.magnitude;
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agentSpeed, Time.deltaTime * animTranSpeed));
        playerDir = EnemyManager.instance.player.transform.position - transform.position;

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            faceTarget();
        }
        agent.SetDestination(EnemyManager.instance.player.transform.position);
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;

        if(HP <= 0)
        {
            anim.StopPlayback();
            anim.SetTrigger("Death");
        }
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x,0,playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTaregtSpeed);
    }

    public void Death()
    {
        Destroy(gameObject);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
}
