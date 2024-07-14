using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "IncreaseMaxAmmoAbility", menuName = "Abilities/Increase Max Ammo")]
    public class IncreaseMaxAmmoAbility : Ability
    {
        public int ammoIncreaseAmount;

        public override void Activate(GameObject target)
        {
            var abilityhandler = target.GetComponent<AbilityHandler>();
            if (abilityhandler != null)
            {
                abilityhandler.IncreaseMaxAmmo(ammoIncreaseAmount);
            }
        }
    }
}