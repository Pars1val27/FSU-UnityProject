using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "SlowedEffect", menuName = "Abilities/Slowed Effect")]
    public class SlowedEffect : Ability
    {
        [Range(0, 1)] 
        [SerializeField]public float slowAmount;

        [Range(0, 10)]
        [SerializeField] public float duration;

        [SerializeField ]public GameObject slowEffectPrefab;
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
