using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] Transform shootPos;

    [SerializeField] int shootRate;
    [SerializeField] GameObject projectile;

    bool isShooting;

    void Start()
    {
        
    }

    
    void Update()
    {
        Debug.Log("testUpdate");
        if (isShooting == false)
        {
            Debug.Log("Test IsShooting");
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        Debug.Log("Test Shoot");
        isShooting = true;
        Instantiate(projectile, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
