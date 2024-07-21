
using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] public string abilityName;
        [SerializeField] public string ablitiyDisctriptions;
        [SerializeField] public float abilityCost;
        [SerializeField] public Sprite uiIcon;
        [SerializeField] public GameObject modelPrefab;
        [SerializeField] public bool debugAbility;


        public abstract void Activate(GameObject target);
    }
}