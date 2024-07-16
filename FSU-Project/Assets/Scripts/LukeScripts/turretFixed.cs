using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class turretFixed : MonoBehaviour, IDamage
{
    //Luke

    [Header("----- Health -----")]
    [SerializeField] int HP;

    [Header("----- AI -----")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] Renderer model;
    [SerializeField] GameObject swivel;
    

    [Header("----- Attack -----")]
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject projectile;
    [SerializeField] float shootRate;
    [SerializeField] float rotateSpeed;
    bool isshooting;

    void Start()
    {
        UIManager.instance.UpdateEnemyDisplay(1);
    }

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

    public void TakeDamage(int amount)
    {
        HP -= amount;

        StartCoroutine(flashDamage());

        if (HP <= 0)
        {
            Death();
        }
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }

    public void Death()
    {
        Destroy(gameObject);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
}
