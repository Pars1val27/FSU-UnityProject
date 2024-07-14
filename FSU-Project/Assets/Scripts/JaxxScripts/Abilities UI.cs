using AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{

    public Image icon;
    public TMP_Text Name;
    public TMP_Text description;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {

        }
    }

    public void ShowAbilityItem(Ability abil)
    {
        icon = abil.uiIcon;
        Name.text = abil.abilityName;
        description.text = abil.ablitiyDisctriptions;
    }

    public void ShowAbilityInventory()
    {
        
    }
}
