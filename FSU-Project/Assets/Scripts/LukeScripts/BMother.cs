using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BMother : MonoBehaviour, IDamage
{
    //Luke

    [Header("----- Health -----")]
    [SerializeField] int HP;

    [Header("----- AI -----")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] Renderer[] model;


    [Header("----- Attack -----")]
    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject spawn;
    [SerializeField] float spawnRate;

    Vector3 playerDir;
    Vector3 playerPos;
    Vector3 dest;

    bool isshooting;
    bool canSeePlayer;
    bool playerInRange;

    void Start()
    {
        transform.GetComponent<SphereCollider>().radius = agent.stoppingDistance;
        UIManager.instance.UpdateEnemyDisplay(1);
    }

    private void Update()
    {
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        dest = transform.position + playerDir;
        agent.SetDestination(dest);
        if (!isshooting)
        {
            StartCoroutine(Spawn());
        }
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }

    IEnumerator Spawn()
    {
        isshooting = true;
        yield return new WaitForSeconds(spawnRate);
        faceTarget();
        Instantiate(spawn, spawnPos.position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
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
        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < model.Length; i++)
        {
            model[i].material.color = Color.white;
        }

    }

    public void Death()
    {
        Destroy(gameObject);
        UIManager.instance.UpdateEnemyDisplay(-1);
    }
}
