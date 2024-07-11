using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInitializer : MonoBehaviour
{
    public static AbilityInitializer Instance;

    public Ability hpIncreaseAbility;

    void Start()
    {
        Instance = this;

    }

    public void Initialize()
    {
        AbilityManager.Instance.RegisterAbility(hpIncreaseAbility.abilityName, hpIncreaseAbility);
        AbilityManager.Instance.ResetSpawnableAbilities();
    }
}
