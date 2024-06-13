using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    //Michael
    [SerializeField] Transform shootPos;

    [SerializeField] float shootRate;
    [SerializeField] GameObject projectile;

    bool isShooting;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (isShooting == false)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
       
        isShooting = true;
        Instantiate(projectile, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
