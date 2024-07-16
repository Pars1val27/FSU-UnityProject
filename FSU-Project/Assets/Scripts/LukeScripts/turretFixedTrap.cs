using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class turretFixedTrap : MonoBehaviour
{
    //Luke

    [Header("----- AI -----")]
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] GameObject swivel;


    [Header("----- Attack -----")]
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] float shootRate;
    [SerializeField] float rotateSpeed;

    bool isshooting;

    private void Update()
    {
        swivel.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        if (!isshooting)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        isshooting = true;
        yield return new WaitForSeconds(shootRate);
        Instantiate(projectile, shootPos.position, new Quaternion(swivel.transform.rotation.x, swivel.transform.rotation.y, swivel.transform.rotation.z, swivel.transform.rotation.w));
        isshooting = false;
    }
}
