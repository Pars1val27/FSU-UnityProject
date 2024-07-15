using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilitySpawner : MonoBehaviour
    {
        public enum SpawnCondition
        {

            Store,
            ItemRoom,
            BossRoom
        }


        private HashSet<string> spawnedAbilities = new HashSet<string>();
        [SerializeField] public List<string> availableAbilities;

        [SerializeField] float pickupRadius;
        [SerializeField] bool spawnOnStart = false;
        [SerializeField] bool IsDebugAbility = false;
        [SerializeField] SpawnCondition spawnCondition;

        [SerializeField] Transform bossSpawnPoint;

        public Transform[] spawnPoints;

        private Collider spawnerCollider;



        private void Start()
        {
            spawnerCollider = GetComponent<Collider>();
            if (spawnerCollider == null)
            {
                Debug.LogError("Collider is missing on AbilitySpawner.");
            }

            if (spawnOnStart)
            {
                SpawnPickups();
            }
        }

        

        public void SpawnPickups()
        {
            switch (spawnCondition)
            {
                case SpawnCondition.Store:
                    SpawnStorePickups();
                    break;
                case SpawnCondition.ItemRoom:
                    SpawnItemRoomPickups();
                    break;
                case SpawnCondition.BossRoom:
                    SpawnBossRoomPickups();
                    break;
                default:
                    break;
            }
        }

        private void SpawnStorePickups()
        {

            SpawnAbilities(spawnPoints, true);
        }

        private void SpawnItemRoomPickups()
        {
            SpawnAbilities(spawnPoints, false);
        }

        private void SpawnBossRoomPickups()
        {
            // either use boss spawner or get boss position 
            SpawnAbilities(new Transform[] { bossSpawnPoint }, false);
          
            
        }


        private void SpawnAbilities(Transform[] points, bool isStore)
        {
            foreach (Transform spawnPoint in points)
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
                        pickupScript.isItemPickup = !isStore;  // Set the pickup type
                        pickupScript.IsDebugAbility = IsDebugAbility;

                        SphereCollider pickupCollider = pickup.AddComponent<SphereCollider>();
                        pickupCollider.isTrigger = true;
                        pickupCollider.radius = pickupRadius;

                        pickup.name = abilityName + "Pickup";
                        spawnedAbilities.Add(abilityName);
                        Debug.Log(abilityName + " spawned");
                    }
                }
            }
        }

        private string GetUniqueSpawnableAbility()
        {
            availableAbilities = new List<string>(AbilityManager.Instance.spawnableAbilities);
            availableAbilities.RemoveAll(name => spawnedAbilities.Contains(name));

            if (availableAbilities.Count > 0)
            {
                int randomIndex = Random.Range(0, availableAbilities.Count);
                return availableAbilities[randomIndex];
            }

            return null;
        }



        public void ClearSpawnedAbilities()
        {
            AbilityPickup[] allPickups = FindObjectsOfType<AbilityPickup>();
            foreach (AbilityPickup pickup in allPickups)
            {
                Destroy(pickup.gameObject);
            }
            spawnedAbilities.Clear();
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!spawnOnStart && other.CompareTag("Player"))
            {

                SpawnPickups();
                if (spawnerCollider != null)
                {
                    spawnerCollider.enabled = false;
                }
            }

        }

    }
}