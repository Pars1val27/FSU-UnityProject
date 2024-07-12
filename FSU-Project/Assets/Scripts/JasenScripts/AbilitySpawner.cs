using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilitySpawner : MonoBehaviour
    {
        public static AbilitySpawner Instance;
        public Transform[] spawnPoints;


        void Start()
        {
            Instance = this;
            SpawnPickups();
        }

        void SpawnPickups()
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                string abilityName = AbilityManager.Instance.GetRandomSpawnableAbility();
                if (!string.IsNullOrEmpty(abilityName))
                {
                    Ability ability = AbilityManager.Instance.GetAbility(abilityName);
                    if (ability != null && ability.modelPrefab != null)
                    {
                        GameObject pickup = Instantiate(ability.modelPrefab, spawnPoint.position, spawnPoint.rotation);
                        AbilityPickup pickupScript = pickup.AddComponent<AbilityPickup>();
                        pickupScript.ability = ability;
                        Collider pickupCollider = pickup.AddComponent<SphereCollider>();
                        pickupCollider.isTrigger = true;
                        pickup.name = abilityName + "Pickup";
                    }
                    else
                    {
                        Debug.Log("Ability or modelPrefab is null for ability: " + abilityName);
                    }
                }
                else { Debug.Log("no Spawnable Abilitys"); }
            }
        }
    }
}