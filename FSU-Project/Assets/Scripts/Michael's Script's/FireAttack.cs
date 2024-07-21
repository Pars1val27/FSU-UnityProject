using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Transform mouthPOS;
    float savedTime;

    Vector3 playerDir;
    Vector3 playerPos;

    bool canSeePlayer;
    // Update is called once per frame
    private void Update()
    {
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {

                canSeePlayer = true;
               
            }
            else
            {
                canSeePlayer = false;
              

            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(Time.time - savedTime > 0.1f)
        {
            savedTime = Time.time;
           

                if (other.tag == "Player" && canSeePlayer)
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
