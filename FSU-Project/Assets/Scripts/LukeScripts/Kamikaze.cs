using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kamikaze : MonoBehaviour, IDamage
{
    //Luke
    [Header("----- Health -----")]
    [SerializeField] int HP;

    [Header("----- AI -----")]
    [SerializeField] int faceTargetSpeed;
    [SerializeField] NavMeshAgent agent;

    [Header("----- Animation's -----")]
    [SerializeField] Renderer model;


    [Header("----- Attack -----")]
    [SerializeField] int damage;
    [SerializeField] int attackRate;

    bool isAttacking;
    bool playerInRange;
    Vector3 playerDir;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.UpdateEnemyDisplay(1);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;

        agent.SetDestination(playerPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            IDamage dmg = other.GetComponent<IDamage>();

            if (dmg != null)
            {
                dmg.TakeDamage(damage);
                Death();
            }
        }
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
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
