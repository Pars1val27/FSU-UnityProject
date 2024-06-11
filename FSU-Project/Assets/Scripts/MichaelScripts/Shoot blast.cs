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

    bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        Instantiate(projectile, shootPos1.position, shootPos1.rotation);
        Instantiate(projectile, shootPos2.position, shootPos2.rotation);
        Instantiate(projectile, shootPos3.position, shootPos3.rotation);
        Instantiate(projectile, shootPos4.position, shootPos4.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
