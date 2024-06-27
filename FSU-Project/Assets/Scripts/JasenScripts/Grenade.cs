using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private float delay;
    private float explosionRadius;
    private float explosionForce;
    private int damage;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] AudioClip explosionSound;

    private AudioSource audioSource;
    bool hasExploded = false;
    float countdown;

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
        countdown = delay;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
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

        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound); 
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
            }
        }

        Destroy(effect.gameObject, effect.main.duration);

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
