using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilitySpawner : MonoBehaviour
    {
        public Transform[] spawnPoints;
        private HashSet<string> spawnedAbilities = new HashSet<string>();
        [SerializeField] float pickipRadius;
        private void Start()
        {
            SpawnPickups();
        }

        public void SpawnPickups()
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                string abilityName = GetUniqueSpawnableAbility();
                if (!string.IsNullOrEmpty(abilityName))
                {
                    Ability ability = AbilityManager.Instance.GetAbility(abilityName);
                    if (ability != null && ability.modelPrefab != null)
                    {
                        GameObject pickup = Instantiate(ability.modelPrefab, spawnPoint.position, spawnPoint.rotation);
                        AbilityPickup pickupScript = pickup.AddComponent<AbilityPickup>();
                        pickupScript.ability = ability;

                        SphereCollider pickupCollider = pickup.AddComponent<SphereCollider>();
                        pickupCollider.isTrigger = true;
                        pickupCollider.radius = pickipRadius; // Adjust as needed

                        pickup.name = abilityName + "Pickup";
                        spawnedAbilities.Add(abilityName);
                        Debug.Log(abilityName + " spawned");
                    }
                    else
                    {
                        Debug.LogWarning("Ability or modelPrefab is null for ability: " + abilityName);
                    }
                }
                else
                {
                    Debug.LogWarning("No unique spawnable abilities available.");
                }
            }
        }

        private string GetUniqueSpawnableAbility()
        {
            List<string> availableAbilities = new List<string>(AbilityManager.Instance.spawnableAbilities);
            availableAbilities.RemoveAll(name => spawnedAbilities.Contains(name));

            if (availableAbilities.Count > 0)
            {
                int randomIndex = Random.Range(0, availableAbilities.Count);
                return availableAbilities[randomIndex];
            }

            return null;
        }

        public void RemoveAbilityFromPool(string abilityName)
        {
            AbilityManager.Instance.RemoveSpawnableAbility(abilityName);
        }

        public void ClearSpawnedAbilities()
        {
            AbilityPickup[] allPickups = FindObjectsOfType<AbilityPickup>();
            foreach (AbilityPickup pickup in allPickups)
            {
                Destroy(pickup.gameObject);
            }
            spawnedAbilities.Clear();
            Debug.Log("Cleared all spawned abilities.");
        }
    }
}