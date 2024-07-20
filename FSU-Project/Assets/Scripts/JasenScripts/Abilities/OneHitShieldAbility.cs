using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "OneHitShieldAbility", menuName = "Abilities/OneHitShield")]
    public class OneHitShieldAbility : Ability
    {
        public float rechargeTime;

        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                abilityHandler.ActivateOneHitShield(this);
               
            }
        }
    }
}
