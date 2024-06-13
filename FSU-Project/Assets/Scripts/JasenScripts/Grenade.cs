using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] int delay;
    [SerializeField] float explosionRadius = 5;
    [SerializeField] float explosionForce = 50f;
    [SerializeField] int damage = 5;
    [SerializeField] ParticleSystem explosionEffect;

    bool hasExploded = false;
    float countdown;

    void Start()
    {
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
