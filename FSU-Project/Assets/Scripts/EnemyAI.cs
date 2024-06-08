using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamage
{
    [SerializeField] int HP;
    [SerializeField] NavMeshAgent agent;

    int currHP;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(EnemyManager.instance.player.transform.position);
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
    }
}
