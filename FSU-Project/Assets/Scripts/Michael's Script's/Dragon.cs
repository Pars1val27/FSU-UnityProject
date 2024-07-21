using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour , IDamage
{
    [Header("Health")]
    [SerializeField] int HP;
    [Header("AI")]
    [SerializeField] float faceTargetSpeed;
    [Header("Animations")]
    [SerializeField] Animator anim;
    [SerializeField] int animTranSpeed;
    [SerializeField] Renderer[] model;
    [SerializeField] GameObject FireBreathAir;
    [SerializeField] GameObject FireBreathGround;
    [Header("Attack")]
    
    [SerializeField] float FireRate;
    [SerializeField] GameObject fireAttackAir;
    [SerializeField] GameObject fireAttackGround;
    [SerializeField] GameObject biteObject;
    [SerializeField] Transform BitePos;


    Vector3 playerDir;

    float maxHP;
    int RandNum;

    bool isFly;
    bool isFire;
    bool isLanding;
   
    bool isDying;
    bool isInAnim;

    bool playerInRange;


    float SavedTime;
    // Update is called once per frame
    void Start()
    {
        
        UIManager.instance.UpdateEnemyDisplay(1);
       
       
      
        
        maxHP = HP;
    }
    void Update()
    { 
        playerDir = EnemyManager.instance.player.transform.position - transform.position;
        if(((Time.time - SavedTime) > FireRate) && !isDying && !isInAnim)
        {
            isInAnim = true;
          RandNum = Random.Range(0, 2);
            if(!playerInRange)
                switch (RandNum)
                {
                    case 0:
                        
                        anim.SetTrigger("Fly");
                        break;
                    case 1:
                        anim.SetTrigger("Fire");
                        break;
                
                }
            if(playerInRange)
               // switch (RandNum)
                //{
                    //case 0:
                        anim.SetTrigger("Bite");
                       // break;
                   // case 1:
                       // anim.SetTrigger("Wing");
                      //  break;
               // }
            SavedTime = Time.time;


        }
       

        if(!isFire)
            faceTarget();
        if (isFly && !isLanding)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 30, transform.position.z), 0.5f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), 1.5f * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        UIManager.instance.bossHealthBar.fillAmount = (float)HP / maxHP;
        StartCoroutine(flashDamage());

        if (HP <= 0)
        {
            isDying = true;
            anim.StopPlayback();
            anim.SetTrigger("Death");
            UIManager.instance.BossWin();
        }
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
        
    }
    public void Death()
    {
        Destroy(gameObject);
        
        UIManager.instance.UpdateEnemyDisplay(-1);
    }

    public void Fire()
    {
        isFire = true;
        if (isFly)
        {
            FireBreathAir.SetActive(true);
            fireAttackAir.SetActive(true);
            
        }
        else
        {
            FireBreathGround.SetActive(true);
            fireAttackGround.SetActive(true);
        }
            
    } 
    public void FireDone()
    {

        if (isFly)
        {
            FireBreathAir.SetActive(false);
            fireAttackAir.SetActive(false);
            isLanding = true;
        }
            
        else
        {
            FireBreathGround.SetActive(false);
            fireAttackGround.SetActive(false);
           isInAnim = false;
        }
        isFire = false;
        
       
    }
    public void Fly()
    {
       isFly = true;
    }

    public void Land()
    {
       
        isFly = false;
        isLanding = false;

    }
    public void Bite()
    {
        Instantiate(biteObject, BitePos.position,transform.rotation);
        
        
    }
    /*public void Wing()
    {
        if (playerInRange)
        {
            EnemyManager.instance.player.GetComponent<Rigidbody>().AddForce(playerDir * 40, ForceMode.Impulse);
        }
        
      
    }*/
    public void LeaveAnim()
    {
        isInAnim = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
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
