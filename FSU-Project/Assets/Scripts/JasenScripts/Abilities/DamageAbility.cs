using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "DamageAbility ", menuName = "Abilities/DamageIncrease")]
    public class DamageAbility : Ability
    {
        [SerializeField] public int DamageIncreaseAmount;
        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                abilityHandler.IncreaseDamage(DamageIncreaseAmount);
            }
        }
    }
}