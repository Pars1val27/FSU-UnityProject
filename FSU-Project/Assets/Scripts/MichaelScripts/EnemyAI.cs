using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamage
{
    //Michael

    [Header("----- Health -----")]
    [SerializeField] int HP;

    [Header("----- AI -----")]
    [SerializeField] int faceTaregtSpeed; 
    [SerializeField] NavMeshAgent agent;
    
    [Header("----- Animation's -----")] 
    [SerializeField] Renderer[] model;
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

        StartCoroutine(flashDamage());

        if (HP <= 0)
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
    IEnumerator flashDamage()
    {
        for(int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.red;
        }
      
        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.white;
        }
       
    }
}
