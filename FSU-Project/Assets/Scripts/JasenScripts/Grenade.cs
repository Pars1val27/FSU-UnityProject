using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using AbilitySystem;

public class Grenade : MonoBehaviour
{
    private float delay;
    private float explosionRadius;
    private float explosionForce;
    private int damage;
    AbilityHandler handler;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] AudioClip[] explosionSound;
    [SerializeField] float explosionSoundVol;

    private AudioSource grenadeAudio;
    bool hasExploded = false;
    float countdown;

    public AbilityHandler abilityHandler;

    public void Initialize(float delay, float explosionRadius, float explosionForce, int damage)
    {
        this.delay = delay;
        this.explosionRadius = explosionRadius;
        this.explosionForce = explosionForce;
        this.damage = damage;
        countdown = delay;
    }

    void Start()
    {
        handler = AbilityHandler.handlerInstance;
        grenadeAudio = GetComponent<AudioSource>();
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        ParticleSystem effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        effect.Play();

        if (grenadeAudio != null && explosionSound != null)
        {
            grenadeAudio.PlayOneShot(explosionSound[Random.Range(0, explosionSound.Length)], explosionSoundVol);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            IDamage dmg = nearbyObject.GetComponent<IDamage>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage);
                //ApplyFreezeEffect(nearbyObject.gameObject);
            }
        }

        Destroy(effect.gameObject, effect.main.duration);
        Destroy(gameObject);
    }

    //void ApplyFreezeEffect(GameObject target)
    //{

    //    foreach (var ability in abilityHandler.abilities)
    //    {
    //        if (ability is FreezeEffect freezeEffect && abilityHandler.hasFreezeEffect)
    //        {
    //            Debug.Log("Activating FreezeEffect on target: " + target.name);
    //            abilityHandler.ApplyFreeze(target, freezeEffect);
    //        }
    //    }
    //}



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}