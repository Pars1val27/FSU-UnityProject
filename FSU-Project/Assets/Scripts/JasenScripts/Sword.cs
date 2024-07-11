using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    //[SerializeField] public GameObject sword;
    [SerializeField] Collider SwordCollider;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] attackSound;
    [SerializeField] float attackSoundVol;
    [SerializeField] ParticleSystem attackEffect;

    private Animator anim;

    public bool isAttacking;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //if (Time.time >= nextAttackTime)
        //{
            if (Input.GetButtonDown("Fire1") && !isAttacking)
            {
                StartCoroutine(Attack());
                //nextAttackTime = Time.time + 1f / PlayerController.playerInstance.attackSpeed;
            }
        //}
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        //aud.PlayOneShot(attackSound[Random.Range(0, attackSound.Length)], attackSoundVol);

        if (PlayerController.playerInstance.attackSpeed < 1)
        {
            anim.speed = (1 + (1 - PlayerController.playerInstance.attackSpeed));
        }

        anim.SetTrigger("Attack");

        if (attackEffect != null)
        {
            attackEffect.Play();
        }

        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            IDamage dmg = hit.collider.GetComponent<IDamage>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage);
            }
        }*/

        yield return new WaitForSeconds(PlayerController.playerInstance.attackSpeed);

        isAttacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player" && !other.isTrigger)
        {
            other.GetComponent<IDamage>().TakeDamage(PlayerController.playerInstance.damage);
        }
    }

    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }*/
}
