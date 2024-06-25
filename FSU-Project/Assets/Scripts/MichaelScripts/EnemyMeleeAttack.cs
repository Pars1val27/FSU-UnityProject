using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    //Michael
    [SerializeField] float AttackRate;
    [SerializeField] Animator anim;
    [SerializeField] GameObject MeleeAttack;
    [SerializeField] Transform ImpactPoint;

    // the name of animation for another attack of the enemy(if it doesnt have an alt attack leave blank)
    [SerializeField] string altAttack;


    float SavedTime = 0;

    bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        if(altAttack == null)
        {
            altAttack = "Empty";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !anim.GetCurrentAnimatorStateInfo(0).IsName(altAttack) && (Time.time - SavedTime) > AttackRate)
        {
            Debug.Log("Melee Hit");
            SavedTime = Time.time;
            anim.SetTrigger("Melee");
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if(other.CompareTag("Player"))
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

    public void punch()
    {
        Instantiate(MeleeAttack, ImpactPoint.position, transform.rotation);
    }

}
