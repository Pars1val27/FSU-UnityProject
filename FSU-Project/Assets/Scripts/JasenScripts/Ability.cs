using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] public string abilityName;
        [SerializeField] public string ablitiyDisctriptions;
        [SerializeField] public GameObject modelPrefab;
        [SerializeField] public Image uiIcon;

        public abstract void Activate(GameObject target);
    }
}