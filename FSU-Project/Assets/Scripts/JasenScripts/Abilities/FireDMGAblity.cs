using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "FireEffect", menuName = "Abilities/FireEffect")]
    public class FireEffect : Ability
    {
        public int fireDamage;
        public float duration;

        public override void Activate(GameObject target)
        {
            if (debugAbility)
            {
                Debug.Log("FireEffect Triggered in ability");

            }
            AbilityHandler.handlerInstance.ApplyFireDamage(target, this);

        }

    }
}
