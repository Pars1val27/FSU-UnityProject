using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

public class SwordScript : MonoBehaviour
{
    public AbilityHandler abilityHandler;
    //[SerializeField] public GameObject sword;
    [SerializeField] Collider SwordCollider;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] attackSound;
    [SerializeField] float attackSoundVol;
    [SerializeField] ParticleSystem attackEffect;

    [SerializeField] float blockCooldown;
    [SerializeField] float blockDuration;

    private Animator anim;

    public bool isAttacking;
    public bool isBlockReady;

    void Start()
    {
        anim = GetComponent<Animator>();
        isBlockReady = true;
    }

    void Update()
    {
        //if (Time.time >= nextAttackTime)
        //{
            if (Input.GetButtonDown("Fire1") && !isAttacking && !UIManager.instance.abilityMenuOpen)
            {
                StartCoroutine(Attack());
                //nextAttackTime = Time.time + 1f / PlayerController.playerInstance.attackSpeed;
            }
        //}

        if (Input.GetButtonDown("Fire2") && !PlayerController.playerInstance.isBlocking && isBlockReady && UIManager.instance.abilityMenuOpen)
        {
            PlayerController.playerInstance.isBlocking = true;
            anim.SetBool("Block", true);
            StartCoroutine(BlockDuration());
            StartCoroutine(BlockCooldown());
        }
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

    IEnumerator BlockDuration()
    {
        /*if (Input.GetButtonUp("Fire2")) {
            yield return new WaitForSeconds(0);
            PlayerController.playerInstance.isBlocking = false;
            anim.SetBool("Block", false);
        }*/
        yield return new WaitForSeconds(blockDuration);
        PlayerController.playerInstance.isBlocking = false;
        anim.SetBool("Block", false);

    }

    IEnumerator BlockCooldown()
    {
        isBlockReady = false;
        yield return new WaitForSeconds(blockCooldown);
        isBlockReady = true;
    }

    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }*/
}
