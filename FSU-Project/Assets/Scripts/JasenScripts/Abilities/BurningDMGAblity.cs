using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "FireEffect", menuName = "Abilities/FireEffect")]
    public class FireEffect : Ability
    {
        public float fireDamage;
        public float duration;

        public override void Activate(GameObject target)
        {
            var flammable = target.GetComponent<IFireDamage>();
            if (flammable != null)
            {
                flammable.ApplyFireDamage(fireDamage, duration);
            }
        }
    }
}
