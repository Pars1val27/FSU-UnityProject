using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbedAttack : MonoBehaviour
{
    //Michael
    [SerializeField] Transform shootPos;

    [SerializeField] int shootRate;
    [SerializeField] GameObject projectileGameObject;
    [SerializeField] int initialVelocity;

    bool isShooting;
    GameObject target;

    void Start()
    {
        target = EnemyManager.instance.player;
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
        float position = Vector3.Distance(transform.position, target.transform.position); ;
        float angle = 0.5f * Mathf.Asin((6.8f * position) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f,angle);
        Instantiate(projectileGameObject, shootPos.position, rotation);

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
