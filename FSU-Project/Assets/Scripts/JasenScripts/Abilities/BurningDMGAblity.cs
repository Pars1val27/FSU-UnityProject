using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "BurningEffect", menuName = "Abilities/Burning Effect")]
    public class BurningEffect : Ability
    {
        public float burnDamage;
        public float duration;

        public override void Activate(GameObject target)
        {
           
            
        }
    }
}
