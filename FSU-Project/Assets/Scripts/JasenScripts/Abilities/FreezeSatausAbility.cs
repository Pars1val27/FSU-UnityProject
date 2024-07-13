using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "FreezeEffect", menuName = "Abilities/Freeze Effect")]
    public class FreezeEffect : Ability
    {
        public float duration;

        public override void Activate(GameObject target)
        {
            //var statusEffectable = target.GetComponent<StatusEffectable>();
            //if (statusEffectable != null)
            //{
            //    statusEffectable.ApplyStatusEffect(StatusEffectType.Freeze, 0, duration);
            //}
        }
    }
}