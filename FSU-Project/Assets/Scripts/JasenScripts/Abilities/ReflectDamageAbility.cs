using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{

    [CreateAssetMenu(fileName = "ReflectDamageAbility", menuName = "Abilities/ReflectDamage")]
    public class ReflectDamageAbility : Ability
    {
        [Range(0,1)] [SerializeField] public float reflectDamagePercentage;
        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                //abilityHandler.reflectDamagePercentage = reflectDamagePercentage;
            }
        }
    }
}