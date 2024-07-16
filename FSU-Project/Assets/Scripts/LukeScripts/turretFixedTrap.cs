using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class turretFixedTrap : MonoBehaviour
{
    //Luke

    [Header("----- AI -----")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] GameObject swivel;


    [Header("----- Attack -----")]
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] float shootRate;
    [SerializeField] float rotateAngle;
    bool isshooting;

    private void Update()
    {
        swivel.transform.Rotate(transform.position, rotateAngle * Time.deltaTime);
        if (!isshooting)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        isshooting = true;
        yield return new WaitForSeconds(shootRate);
        Instantiate(projectile, shootPos.position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        isshooting = false;
    }
}
