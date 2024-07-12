using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityInitializer : MonoBehaviour
    {
        public static AbilityInitializer Instance;

        public Ability[] abilities;

        void Start()
        {
            Instance = this;

        }

        public void Initialize()
        {
            foreach (var ability in abilities)
            {
                if (ability != null)
                {
                    AbilityManager.Instance.RegisterAbility(ability.abilityName, ability);
                }
            }
            AbilityManager.Instance.ResetSpawnableAbilities();
        }
    }
}