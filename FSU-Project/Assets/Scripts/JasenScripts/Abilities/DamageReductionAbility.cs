using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    
    [CreateAssetMenu(fileName = "DamageReductionAbility", menuName = "Abilities/Damage Reduction")]
   
    public class DamageReductionAbility : Ability
    {
        AbilityHandler handler;
        [Range(1,100)] [SerializeField] public float reductionPercentage;

        public override void Activate(GameObject target)
        {
            handler = AbilityHandler.handlerInstance;
            if (handler != null)
            {
                handler.ReducedDamagePercentage = reductionPercentage;
            }
        }
    }
}