using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOErock : MonoBehaviour
{
    //Michael
    [SerializeField] Rigidbody rb;

    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int destroyTime;
    [SerializeField] float angle;

    [SerializeField] GameObject rock;
    [SerializeField] Transform[] shootPos;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = (EnemyManager.instance.player.transform.position - transform.position) + new Vector3(0,angle,0) * speed;
        Destroy(gameObject, destroyTime);
        for(int i = 0; i < shootPos.Length; i++)
        {
            Instantiate(rock, shootPos[i].position, shootPos[i].rotation);
        }
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
