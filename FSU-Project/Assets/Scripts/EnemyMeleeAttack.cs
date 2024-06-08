using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{

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

        Debug.Log("Hit");
        if ((Time.time - SavedTime) > AttackRate)
        {
            SavedTime = Time.time;

            IDamage dmg = other.GetComponent<IDamage>();
            Debug.Log("Hit");
            if (dmg != null)
            {

                dmg.TakeDamage(Dmg);
            }
        }
    }

}
