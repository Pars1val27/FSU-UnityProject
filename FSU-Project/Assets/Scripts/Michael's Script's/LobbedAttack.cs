using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Arc Calculation
//(player transform - transform) + new Vector3(0,arc,0) * speed (speed = 3, arc = 2)
public class LobbedAttack : MonoBehaviour
{
    //Michael
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform shootPos;

    [SerializeField] int shootRate;
    [SerializeField] GameObject projectileGameObject;
    [SerializeField] int initialVelocity;

    bool isShooting;

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
        
        /*Instantiate(projectileGameObject, shootPos.position, rotation);*/

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
