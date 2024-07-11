using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
     

    void Start()
    {
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
                }
                else
                {
                    Debug.LogError("Ability or modelPrefab is null for ability: " + abilityName);
                }
            }
        }
    }
}
