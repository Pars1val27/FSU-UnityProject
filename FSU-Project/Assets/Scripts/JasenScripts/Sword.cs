using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    //[SerializeField] public GameObject sword;
    [SerializeField] Collider SwordCollider;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private float attackSoundVol;
    [SerializeField] private ParticleSystem attackEffect;

    private float nextAttackTime = 0f;
    private AudioSource audioSource;
    int damage;
    float attackRate;
    float range;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        damage = PlayerController.playerInstance.damage;
        attackRate = PlayerController.playerInstance.attackSpeed;
        range = PlayerController.playerInstance.shootDist;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    IEnumerator Attack()
    {
        if (attackEffect != null)
        {
            attackEffect.Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            IDamage dmg = hit.collider.GetComponent<IDamage>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage);
            }
        }

        yield return new WaitForSeconds(0.1f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
