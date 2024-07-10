using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlphaBrute : MonoBehaviour , IDamage
{
    [Header("----- Health -----")]
    [SerializeField] int HP;

    [Header("----- AI -----")]
    [SerializeField] int faceTaregtSpeed;
    

    [Header("----- Animation's -----")]
    [SerializeField] Renderer[] model;
    [SerializeField] Animator anim;
    [SerializeField] int animTranSpeed;

    [Header("----- Attack -----")]
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] int shootAngle;

    [SerializeField] Transform attackPos;
    [SerializeField] GameObject attack;
    [SerializeField] float attackRate;

    Vector3 playerDir;
    Vector3 playerPos;
    Vector3 prevNavPos;



    float angleToPlayer;
    float SavedTime = 0;
    float MaxHP;

    bool isshooting;
   
    bool playerInRange;
    bool isAttacking;
    bool isSecondPhase;
    bool hasStartedRocks;
    // Start is called before the first frame update
    void Start()
    {
       
        UIManager.instance.UpdateEnemyDisplay(1);
        MaxHP = HP;

    }

    // Update is called once per frame
    void Update()
    {
      
        float distance = Vector3.Distance(transform.position, playerPos);
       
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);


if(HP < MaxHP/2 && !isSecondPhase)
            {
                isSecondPhase = true;
            }
        


        if ((Time.time - SavedTime) > attackRate)
        {
            SavedTime = Time.time;
            
            if (distance < 10 && !isAttacking)
            {
                isAttacking = false;
                anim.SetTrigger("Melee");
            }
            if (isSecondPhase && !hasStartedRocks)
            {
                hasStartedRocks = false;
            }
            else if (!isAttacking)
            {
                isAttacking = true;
                anim.SetTrigger("Shoot");
            }
            
        }


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
    public void shoot()
    {
        Instantiate(projectile, shootPos.position, transform.rotation);

        isAttacking = false;
    }

    public void punch()
    {
        Instantiate(attack, attackPos.position, transform.rotation);
        isAttacking = false;
    }
}
