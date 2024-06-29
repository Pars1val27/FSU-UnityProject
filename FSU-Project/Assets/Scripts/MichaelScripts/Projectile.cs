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
    float angle;

    Vector3 playerDir;
    // Start is called before the first frame update
    void Start()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;
        angle = Vector3.Angle(playerDir, transform.forward);
        rb.velocity = (EnemyManager.instance.player.transform.position - transform.position) + playerDir.normalized * speed;
       
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
