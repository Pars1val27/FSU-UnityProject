using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Instance;
    private Dictionary<string, Ability> abilities = new Dictionary<string, Ability>();
    private List<string> spawnableAbilities = new List<string>();
    void Awake()
    {
        Instance = this;
        
    }

    public void RegisterAbility(string name, Ability ability)
    {
        if (!abilities.ContainsKey(name))
        {
            abilities[name] = ability;
            spawnableAbilities.Add(name);
        }
    }

    public void ActivateAbility(string name, GameObject target)
    {
        if (abilities.ContainsKey(name))
        {
            abilities[name].Activate(target);
            RemoveSpawnableAbility(name);
        }
    }

    public void RemoveSpawnableAbility(string name)
    {
        if (spawnableAbilities.Contains(name))
        {
            spawnableAbilities.Remove(name);
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

    public Ability GetAbility(string name)
    {
        if (abilities.ContainsKey(name))
        {
            return abilities[name];
        }
        return null;
    }

    public void ResetSpawnableAbilities()
    {
        spawnableAbilities = new List<string>(abilities.Keys);
    }
}
