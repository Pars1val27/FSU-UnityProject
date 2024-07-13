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
            var freeze = target.GetComponent<IFreeze>();
            if (freeze != null)
            {
                freeze.ApplyFreeze(duration);
            }
        }
    }
}