using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    //Michael
    [SerializeField] int Dmg;
    [SerializeField] float AttackRate;

    float SavedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    { 
        if ((Time.time - SavedTime) > AttackRate && other.tag == "Player")
        {
            SavedTime = Time.time;

            IDamage dmg = other.GetComponent<IDamage>();
           
            if (dmg != null)
            {

                dmg.TakeDamage(Dmg);
            }
        }
    }

}
