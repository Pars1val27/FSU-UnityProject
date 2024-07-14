using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    [SerializeField] int damage;

    float savedTime;
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(Time.time - savedTime > 0.1f)
        {
            savedTime = Time.time;
            if (other.tag == "Player")
            {

                IDamage dmg = other.GetComponent<IDamage>();

                if (dmg != null)
                {
                    dmg.TakeDamage(damage);
                }
            }
        }
    }
}
