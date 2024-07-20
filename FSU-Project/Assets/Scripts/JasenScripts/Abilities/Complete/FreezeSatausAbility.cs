using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "FreezeEffect", menuName = "Abilities/Freeze Effect")]
    public class FreezeEffect : Ability
    {
        [Range(0, 5)]
        [SerializeField] public float duration;

        public override void Activate(GameObject target)
        {
            if (debugAbility)
            {
                Debug.Log("FreezeEffect Triggered in ability");

            }
            AbilityHandler.handlerInstance.ApplyFreeze(target, this);
        }
    }
}