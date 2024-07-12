using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    //[CreateAssetMenu(fileName = "StaminaAbility", menuName = "Abilities/StaminaIncrease")]
    public class StaminaAbility : Ability
    {
        [SerializeField] public int staminaIncreaseAmount;
        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                abilityHandler.IncreaseStamina(staminaIncreaseAmount);
            }
        }
    }
}
