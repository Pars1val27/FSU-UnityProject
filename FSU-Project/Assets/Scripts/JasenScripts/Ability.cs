using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public GameObject modelPrefab;
    //public Sprite uiSprite; 

    public abstract void Activate(GameObject target);
}