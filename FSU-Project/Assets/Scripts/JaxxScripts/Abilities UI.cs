using AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{
    AbilitiesUI abilitiesUI; 
    [SerializeField] GameObject purchaseButton;
    [SerializeField] GameObject shownPrice;
    public Image icon;
    public TMP_Text Name;
    public TMP_Text description;
    public TMP_Text price;

    public Image[] ownedAbil;
    public bool isItemRoom;
    
    // Start is called before the first frame update
    void Start()
    {
        abilitiesUI = this;
    }  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ShowAbilityInventory();
        }
    }

    public void ShowAbilityItem(Ability abil)
    {
        if(isItemRoom) 
        {
        purchaseButton.SetActive(false);
        shownPrice.SetActive(false);
            UIManager.instance.statePause();
            icon = abil.uiIcon;
            Name.text = abil.abilityName;
            description.text = abil.ablitiyDisctriptions;
        }
        UIManager.instance.statePause();
        icon = abil.uiIcon;
        Name.text = abil.abilityName;
        description.text = abil.ablitiyDisctriptions;
        price.text = abil.ablitiyCost.ToString();
        purchaseButton.SetActive(true);
        shownPrice.SetActive(true);
    }

    public void ShowAbilityInventory()
    {
        
    }
}
