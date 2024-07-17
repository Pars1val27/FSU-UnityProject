using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "SpeedAbility", menuName = "Abilities/SpeedIncrease")]
    public class SpeedAbility : Ability
    {
        [SerializeField] public int speedIncreaseAmount;
        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                abilityHandler.IncreaseSpeed(speedIncreaseAmount);
            }
        }
    }
}