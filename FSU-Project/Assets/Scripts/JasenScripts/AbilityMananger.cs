using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance;
    public Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
    [SerializeField] public List<string> spawnableAbilities = new List<string>();
    void Start()
    {
        Instance = this;
        InitializeAbilities();
    }

    public void InitializeAbilities()
    {
       
        AbilityInitializer.Instance.Initialize();
    }

    public void RegisterAbility(string abilityName, Ability ability)
    {
        if (!abilities.ContainsKey(abilityName))
        {
            abilities[abilityName] = ability;
            spawnableAbilities.Add(abilityName);
        }
    }

    public void ActivateAbility(string abilityName, GameObject target)
    {
        if (abilities.ContainsKey(abilityName))
        {
            //abilities[abilityName].Activate(target);
            RemoveSpawnableAbility(abilityName);
        }
    }

    public void RemoveSpawnableAbility(string abilityName)
    {
        if (spawnableAbilities.Contains(abilityName))
        {
            spawnableAbilities.Remove(abilityName);
        }
    }

    public string GetRandomSpawnableAbility()
    {
        if (spawnableAbilities.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnableAbilities.Count);
            return spawnableAbilities[randomIndex];
        }
        return null;
    }

    public Ability GetAbility(string abilityName)
    {
        if (abilities.ContainsKey(abilityName))
        {
            return abilities[abilityName];
        }
        return null;
    }

    public void ResetSpawnableAbilities()
    {
        spawnableAbilities = new List<string>(abilities.Keys);
    }
}
