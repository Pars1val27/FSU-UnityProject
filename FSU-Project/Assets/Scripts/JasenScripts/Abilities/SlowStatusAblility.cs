using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "SlowedEffect", menuName = "Abilities/Slowed Effect")]
    public class SlowedEffect : Ability
    {
        public float slowAmount;
        public float duration;
        public override void Activate(GameObject target)
        {
            if (debugAbility == true)
            {
                Debug.Log("slowedEffect Triggered in ability");

            }
            AbilityHandler.handlerInstance.ApplySlow(target, this);
        }
    }
}
