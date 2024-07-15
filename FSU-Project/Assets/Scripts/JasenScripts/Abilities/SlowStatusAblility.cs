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
            var slow = target.GetComponent<ISlow>();
            if (slow != null)
            {
                slow.ApplySlow(slowAmount, duration);
            }
        }
    }
}
