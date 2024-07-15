using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRock : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int destroyTime;
    [SerializeField] float angle;

    // Start is called before the first frame update
    void Start()
    {
        int Randx = Random.Range(-20, 20);
        int Randy = Random.Range(-20, 20);
        int Randz = Random.Range(-20, 20);
        rb.velocity = (EnemyManager.instance.player.transform.position - transform.position) +  new Vector3(Randx,Randy,Randz) + new Vector3(0, angle, 0) * speed;
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

       

        /*Destroy(gameObject);*/

    }
}
