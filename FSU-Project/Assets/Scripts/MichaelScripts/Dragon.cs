using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [Header("Attack")]
    [SerializeField] float flyIntervalMin;
    [SerializeField] float flyIntervalMax;

    [SerializeField] float FireIntervalMin;
    [SerializeField] float FireIntervalMax;

    [SerializeField] float BiteRate;

    [SerializeField] GameObject fireAttackAir;
    [SerializeField] GameObject fireAttackGround;


    Vector3 playerDir;

    bool isFly;
    bool isFire;
    bool isBite;
    bool isDying;
    bool playerInRange;


    float SavedTimeFire;
    float fireInterval;
   
    float SavedTimeFly;
    float flyInterval;

    float SavedTimeBite;
    // Update is called once per frame
    void Start()
    {
        UIManager.instance.UpdateEnemyDisplay(1);
        SavedTimeFire = Random.Range(FireIntervalMin, FireIntervalMax);
        fireInterval = SavedTimeFire;
        SavedTimeFly = Random.Range(flyIntervalMin, flyIntervalMax);
        flyInterval = SavedTimeFly;
    }
    void Update()
    {
        if ((Time.time - SavedTimeFly) > flyInterval && !isFire && !isFly && !isBite && !isDying)
        {
            isFly = true;
            SavedTimeFly += flyInterval;
            flyInterval = Random.Range(flyIntervalMin, flyIntervalMax);
            anim.SetTrigger("Fly");
        }
        if((Time.time - SavedTimeFire) > fireInterval && !isFire && !isFly && !isBite && !isDying)
        {
            isFire = true;
            SavedTimeFire += fireInterval;
            fireInterval = Random.Range(FireIntervalMin, FireIntervalMax);
            anim.SetTrigger("Fire");
        }
       
        if ((Time.time - SavedTimeBite) > BiteRate && !isFire && !isFly && !isBite && playerInRange && !isDying)
        {
            isBite = true;
            SavedTimeBite += BiteRate;
            anim.SetTrigger("Bite");
        }
        playerDir = EnemyManager.instance.player.transform.position - transform.position;
        
        faceTarget();
        
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0)
        {
            isDying = true;
            anim.StopPlayback();
            anim.SetTrigger("Death");
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
        isFire = false;
        if(isFly)
            fireAttackAir.SetActive(false);
        else 
            fireAttackGround.SetActive(false);
    }
    public void Fly()
    {
        BoxCollider boxPos = gameObject.GetComponent<BoxCollider>();
        boxPos.transform.position = new Vector3(boxPos.transform.position.x, boxPos.transform.position.y + 5, boxPos.transform.position.z);
      isFly = true;
    }

    public void Land()
    {
        BoxCollider boxPos = gameObject.GetComponent<BoxCollider>();
        boxPos.transform.position = new Vector3(boxPos.transform.position.x, boxPos.transform.position.y - 5, boxPos.transform.position.z);
        isFly = false;
    }
    public void Bite()
    {
        
        isBite = false;
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
}
