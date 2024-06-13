using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveShot : MonoBehaviour
{
    //Michael
    [SerializeField] Rigidbody rb;

    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int destroyTime;

    [SerializeField] GameObject explosion;
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

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && other.tag == "Player")
        {
            dmg.TakeDamage(damage);
        }

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);

    }
}
