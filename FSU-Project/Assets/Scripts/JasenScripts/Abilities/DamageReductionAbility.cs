using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "DamageReductionAbility", menuName = "Abilities/Damage Reduction")]
    public class DamageReductionAbility : Ability
    {
        [Range(1,100)] [SerializeField] public float reductionPercentage;

        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                abilityHandler.ReducedDamagePercentage = reductionPercentage;
            }
        }
    }
}