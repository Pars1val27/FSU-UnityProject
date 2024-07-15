using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minotaur : MonoBehaviour
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


    Vector3 playerDir;
    Vector3 playerPos;

    float angleToPlayer;

    bool isCharging;
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
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);

        if (agent.remainingDistance == 0)
        {
            agent.SetDestination(playerPos);
        }
    }
    public void TakeDamage(int amount)
    {
        HP -= amount;
       
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

   
   
}
