using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public GameObject modelPrefab; // 3D model for the ability
    //public Sprite uiSprite; // UI sprite for the ability

    public abstract void Activate(GameObject target);
}