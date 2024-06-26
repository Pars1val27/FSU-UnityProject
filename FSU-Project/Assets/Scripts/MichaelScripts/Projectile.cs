using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Michael
    [SerializeField] Rigidbody rb;

    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        if(other.tag == "Player")
        {

        IDamage dmg = other.GetComponent<IDamage>();

                if (dmg != null)
                {
                    dmg.TakeDamage(damage);
                }
        }
        

        Destroy(gameObject);
    }
}
