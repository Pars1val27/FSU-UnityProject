using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "OneHitShieldAbility", menuName = "Abilities/OneHitShield")]
    public class OneHitShieldAbility : Ability
    {
        [Range(1,25)][SerializeField] public int rechargeTime;

        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                abilityHandler.shieldRecharge = rechargeTime;
                abilityHandler.ActivateOneHitShield();
            }
        }
    }
}
