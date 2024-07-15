using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int destroyTime;
    [SerializeField] float angle;

   
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && other.tag == "Player")
        {
            dmg.TakeDamage(damage);
        }
        else
        {
            Destroy(gameObject);
        }

      

        Destroy(gameObject);

    }
}
