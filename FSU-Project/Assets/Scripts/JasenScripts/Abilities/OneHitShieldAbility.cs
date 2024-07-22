using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "OneHitShieldAbility", menuName = "Abilities/OneHitShield")]
    public class OneHitShieldAbility : Ability
    {
        //AbilityHandler handler;
        [Range(1,25)][SerializeField] public int rechargeTime;

        public override void Activate(GameObject target)
        {
            var handler = target.GetComponent<AbilityHandler>();
            if (handler != null)
            {

                handler.ActivateOneHitShield(this);
            }
        }
    }
}
