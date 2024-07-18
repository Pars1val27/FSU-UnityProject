using System.Collections;
using System.Collections.Generic;
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
   
   
    bool isDying;

    bool playerInRange;


    float SavedTime;

    float currInterval;
  

    int randAction;

    bool[] actions;
    // Update is called once per frame
    void Start()
    {
        UIManager.instance.UpdateEnemyDisplay(1);
        
       randAction = Random.Range(0,actions.Length);
        actions[randAction] = true;
        currInterval = 5;
        maxHP = HP;
    }
    void Update()
    {
        if(Time.time - SavedTime > currInterval)
        {
            if (actions[0] == true)
            {
                currInterval = FireRate;
                actions[0] = false;
                anim.SetTrigger("fire");
            }
            else if (actions[1] == true)
            {
                currInterval = FlyRate;
                actions[1] = false;
                anim.SetTrigger("fly");
            }
            else if (actions[2] == true && playerInRange)
            {
                currInterval = BiteRate;
                actions[2] = false;
                anim.SetTrigger("Bite");
            }
            else if (actions[3] == true && playerInRange)
            {
                currInterval = WingRate;
                actions[4] = false;
                anim.SetTrigger("Wing");
            }
            else
            {
                SetRand();
            }
            SavedTime = Time.time;


        }
        playerDir = EnemyManager.instance.player.transform.position - transform.position;
        
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
        }
           
    }
    public void Fly()
    {
      isFly = true;
    }

    public void Land()
    {
        isFly = false;
        SetRand();
    }
    public void Bite()
    {
        Instantiate(biteObject, BitePos.position,transform.rotation);
        SetRand();
    }
    public void Wing()
    {
        if (playerInRange)
        {
            CharacterController player = EnemyManager.instance.player.GetComponent<CharacterController>();
           
        }
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
