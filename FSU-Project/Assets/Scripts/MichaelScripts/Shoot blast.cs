using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootblast : MonoBehaviour
{
    // Michael
    [SerializeField] Transform shootPos1;
    [SerializeField] Transform shootPos2;
    [SerializeField] Transform shootPos3;
    [SerializeField] Transform shootPos4;

    [SerializeField] float shootRate;
    [SerializeField] GameObject projectile;
    [SerializeField] Animator anim;

    // the name of animation for another attack of the enemy(if it doesnt have an alt attack leave blank)
    [SerializeField] string altAttack;

    bool isShooting;
    float SavedTime = 0;

    // Update is called once per frame
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
        Instantiate(projectile, shootPos1.position, shootPos1.rotation);
        Instantiate(projectile, shootPos2.position, shootPos2.rotation);
        Instantiate(projectile, shootPos3.position, shootPos3.rotation);
        Instantiate(projectile, shootPos4.position, shootPos4.rotation);
    }


}
