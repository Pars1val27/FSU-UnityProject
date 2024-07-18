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
    [Header("Attack")]
    [SerializeField] float FlyRate;
    [SerializeField] float FireRate;
    [SerializeField] float BiteRate;
    [SerializeField] float WingRate;

    [SerializeField] GameObject fireAttackAir;
    [SerializeField] GameObject fireAttackGround;
    [SerializeField] GameObject biteObject;
    [SerializeField] Transform BitePos;


    Vector3 playerDir;

    float maxHP;

    bool isFly;
    bool isFire;
   
    bool isDying;

    bool playerInRange;


    float SavedTime;

    float currInterval;
  

    int randAction;

    bool[] actions;
    // Update is called once per frame
    void Start()
    {
        actions = new bool[4];
        UIManager.instance.UpdateEnemyDisplay(1);
        for (int i = 0; i < actions.Length; i++) { actions[i] = false; }
        SetRand();
        actions[randAction] = true;
        currInterval = 10;
        maxHP = HP;
    }
    void Update()
    { 
        playerDir = EnemyManager.instance.player.transform.position - transform.position;
        if((Time.time - SavedTime) > FireRate && !isDying)
        {
            Debug.Log("in list");
            if (actions[0] == true)
            {
                Debug.Log("in fire");
              
               
                anim.SetTrigger("Fire");
            }
            else if (actions[1] == true)
            {
                Debug.Log("in fly");
               
               
                anim.SetTrigger("Fly");
            }
            else if (actions[2] == true && playerInRange)
            {
                Debug.Log("in bite");
                
               
                anim.SetTrigger("Bite");
            }
            else if (actions[3] == true && playerInRange)
            {
                Debug.Log("in wing");
               
                actions[3] = false;
                anim.SetTrigger("Wing");
            }
            else
            {
                for (int i = 0; i < actions.Length; i++) { actions[i] = false; }
                Debug.Log("in else");
                SetRand();
            }
            SavedTime = Time.time;


        }
       

        if(!isFire)
            faceTarget();
        
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
            fireAttackAir.SetActive(true);
        else
            fireAttackGround.SetActive(true);
    } 
    public void FireDone()
    {
      
        if(isFly)
            fireAttackAir.SetActive(false);
        else
        {
            fireAttackGround.SetActive(false);
            SetRand();
            actions[0] = false;
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
        SetRand();
        actions[1] = false;
       
    }
    public void Bite()
    {
        Instantiate(biteObject, BitePos.position,transform.rotation);
        SetRand();
        actions[2] = false;
        
    }
    public void Wing()
    {
        if (playerInRange)
        {
            CharacterController player = EnemyManager.instance.player.GetComponent<CharacterController>();
            player.SimpleMove(playerDir * 10);
        }
        actions[3] = false;
        SetRand();
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
    void SetRand()
    {
        randAction = Random.Range(0,actions.Length);
        actions[randAction] = true;
    }
}
