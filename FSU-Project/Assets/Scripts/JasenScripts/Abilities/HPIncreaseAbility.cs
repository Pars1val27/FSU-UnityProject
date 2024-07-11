using UnityEngine;

[CreateAssetMenu(fileName = "HPIncreaseAbility", menuName = "Abilities/HPIncrease")]
public class HPIncreaseAbility : Ability
{
    [SerializeField]public int hpIncreaseAmount;

    public override void Activate(GameObject target)
    {
        var abilityhandler = target.GetComponent<Abilityhandler>();
        if (abilityhandler != null)
        {
            abilityhandler.IncreaseMaxHP(hpIncreaseAmount);
        }

    }
}