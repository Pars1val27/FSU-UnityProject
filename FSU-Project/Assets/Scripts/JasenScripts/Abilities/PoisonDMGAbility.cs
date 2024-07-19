using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "PoisonEffect", menuName = "Abilities/Poison Effect")]
    public class PoisonEffect : Ability
    {
        public int poisonDamage;
        public float duration;

        public override void Activate(GameObject target)
        {
            if (debugAbility)
            {
                Debug.Log("PoisonEffect Triggered in ability");
            }

            AbilityHandler.handlerInstance.ApplyPoisonDamage(target, this);
        }
    }
}
