using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] public GameObject modelPrefab;
    [SerializeField] public Sprite uiSprite;
    [SerializeField] public string abilityName;
    [SerializeField] public string ablitiyDisctriptions;

    public abstract void Activate(GameObject target);
}