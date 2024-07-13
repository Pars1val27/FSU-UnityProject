using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "PoisonEffect", menuName = "Abilities/Poison Effect")]
    public class PoisonEffect : Ability
    {
        public float damageOverTime;
        public float duration;

        public override void Activate(GameObject target)
        {
            var poisonDamage = target.GetComponent<IPoisonDamage>();
            if (poisonDamage != null)
            {
                poisonDamage.ApplyPoisonDamage(damageOverTime, duration);
            }
        }
    }
}
