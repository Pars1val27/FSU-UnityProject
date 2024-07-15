using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrutePunch : MonoBehaviour
{
    [SerializeField] int damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.1F);
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
    }
}
