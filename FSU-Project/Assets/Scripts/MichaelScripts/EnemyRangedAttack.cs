using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    //Michael
    [SerializeField] Transform shootPos;
    [SerializeField] Animator anim;
    [SerializeField] GameObject projectile;

    // the name of animation for another attack of the enemy(if it doesnt have an alt attack leave blank)
    [SerializeField] string altAttack;

    [SerializeField] float shootRate;
 
    float SavedTime = 0;
    
    void Update()
    {
        if ((Time.time - SavedTime) > shootRate && !anim.GetCurrentAnimatorStateInfo(0).IsName(altAttack))
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
