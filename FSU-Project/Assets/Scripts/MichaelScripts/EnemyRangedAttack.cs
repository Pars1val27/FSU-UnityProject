using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    //Michael
    [Header("----- Attack -----")]
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] int shootAngle;
    [SerializeField] float shootRate;

    [Header("----- Animation -----")]
     [SerializeField] Animator anim;

    float angleToPlayer;
    float SavedTime = 0;

    Vector3 playerDir;
    void Update()
    {
        playerDir = new Vector3 (playerDir.x, playerDir.y +1, playerDir.z) - transform.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);
        if ((Time.time - SavedTime) > shootRate && angleToPlayer < shootAngle)
        {
            
            SavedTime = Time.time;
            anim.SetTrigger("Shoot");
        }
    }

    
    public void shoot()
    {
        Instantiate(projectile, shootPos.position, transform.rotation);
    }
}
