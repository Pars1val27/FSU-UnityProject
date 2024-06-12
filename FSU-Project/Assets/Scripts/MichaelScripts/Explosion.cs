using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

         IDamage dmg = other.GetComponent<IDamage>();

                if (dmg != null)
                {
                    dmg.TakeDamage(damage);
                }
        }

        Invoke("explode", 1);
    }

    void explode()
    {
        Destroy(gameObject);
    }
}
