using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "PoisonEffect", menuName = "Abilities/Poison Effect")]
    public class PoisonEffect : Ability
    {
        [Range(1, 5)]
        [SerializeField] public int poisonDamage;
        [Range(0, 10)]
        [SerializeField] public float duration;
        [SerializeField] public GameObject PoisonEffectPrefab;
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