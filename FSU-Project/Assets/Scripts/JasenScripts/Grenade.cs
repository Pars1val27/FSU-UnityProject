using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    
        [SerializeField] private float delay = 3f;
        [SerializeField] private float explosionRadius = 5f;
        [SerializeField] private float explosionForce = 700f;
        [SerializeField] private int damage = 5;
        [SerializeField] private ParticleSystem explosionEffect;

        private bool hasExploded = false;
        private float countdown;

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
          
            explosionEffect.transform.parent = null; 
            explosionEffect.transform.position = transform.position;
            explosionEffect.Play();

           
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

            
            Destroy(gameObject);
        }

        void OnDrawGizmosSelected()
        {
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
   }

