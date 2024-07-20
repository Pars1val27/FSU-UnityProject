using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "FireEffect", menuName = "Abilities/FireEffect")]
    public class FireEffect : Ability
    {
        [Range(1, 10)]
        [SerializeField] public int fireDamage;
        [Range(0, 5)]
        [SerializeField] public float duration;

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
