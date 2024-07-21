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

    [SerializeField] float blockDuration;
    [SerializeField] float blockStamCost;

    private Animator anim;

    public bool isAttacking;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetInteger("AttackNum", Random.Range(0, 6));

        if (Input.GetButtonDown("Fire1") && !isAttacking && !UIManager.instance.abilityMenuOpen)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetButtonDown("Fire2") && !PlayerController.playerInstance.isBlocking && !UIManager.instance.abilityMenuOpen && PlayerController.playerInstance.stamina >= blockStamCost)
        {
            PlayerController.playerInstance.isBlocking = true;
            PlayerController.playerInstance.stamina -= blockStamCost;
            anim.SetBool("Block", true);
        }

        if(Input.GetButtonUp("Fire2") || PlayerController.playerInstance.stamina <= 0)
        {
            PlayerController.playerInstance.isBlocking = false;
            anim.SetBool("Block", false);
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        aud.PlayOneShot(attackSound[Random.Range(0, attackSound.Length)], attackSoundVol);

        if (PlayerController.playerInstance.attackSpeed < 1)
        {
            anim.speed = (1 + (1 - PlayerController.playerInstance.attackSpeed));
        }

        anim.SetTrigger("Attack");

        if (attackEffect != null)
        {
            attackEffect.Play();
        }

        yield return new WaitForSeconds(PlayerController.playerInstance.attackSpeed);

        isAttacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Player" && !other.isTrigger)
        {
            IDamage dmg = other.GetComponent<IDamage>();

            if (dmg != null)
            {
                dmg.TakeDamage(PlayerController.playerInstance.damage);
            }
        }
    }
}
