using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamage
{
    //Michael
    [SerializeField] int HP;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int faceTaregtSpeed;

    int currHP;
    bool playerInRange;

    Vector3 playerDir;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.UpdateEnemyDisplay(3);
    }

    // Update is called once per frame
    void Update()
    {
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
            UIManager.instance.UpdateEnemyDisplay(-1);
            Destroy(gameObject);
        }
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTaregtSpeed);
    }
}
