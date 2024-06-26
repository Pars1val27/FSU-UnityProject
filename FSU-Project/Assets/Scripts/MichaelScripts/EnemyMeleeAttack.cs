using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    //Michael
    [Header("----- Attack -----")]
    [SerializeField] float AttackRate;
    [SerializeField] GameObject MeleeAttack;
    [SerializeField] Transform ImpactPoint;

    [Header("----- Animation -----")]
    [SerializeField] Animator anim;


    float SavedTime = 0;

    bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && (Time.time - SavedTime) > AttackRate)
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
