
using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] public string abilityName;
        [SerializeField] public string ablitiyDisctriptions;
        [SerializeField] public int abilityCost;
        [SerializeField] public Image uiIcon;
        [SerializeField] public GameObject modelPrefab;
        

        public abstract void Activate(GameObject target);
    }
}