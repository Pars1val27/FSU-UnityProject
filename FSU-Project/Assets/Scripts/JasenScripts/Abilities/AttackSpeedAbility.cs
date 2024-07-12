using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "AttackSpeedAbility ", menuName = "Abilities/AttackSpeedIncrease")]
    public class AttackSpeedAbility : Ability
    {
        [SerializeField] public float attackSpeedIncreaseAmount;
        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                abilityHandler.IncreaseAttackSpeed(attackSpeedIncreaseAmount);
            }
        }
    }
}