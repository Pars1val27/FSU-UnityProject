using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "HPRecoveryAbility", menuName = "Abilities/HPRecovery")]
    public class HPRecoveryAbility : Ability
    {
        [SerializeField] public int hpRecoveryAmount;
        [SerializeField] public float tickInterval;

        public override void Activate(GameObject target)
        {
            var abilityHandler = target.GetComponent<AbilityHandler>();
            if (abilityHandler != null)
            {
                abilityHandler.EnableHPRecovery(hpRecoveryAmount, tickInterval);
            }
        }
    }
}
