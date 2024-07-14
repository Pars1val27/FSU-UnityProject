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
    [SerializeField] GameObject inventoryMenu;
    public Image icon;
    public TMP_Text Name;
    public TMP_Text description;
    public TMP_Text price;
    public TMP_Text itemComfirm;

    public Image[] ownedAbil;
    
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

    public void ShowAbilityItem(Ability abil, bool isItemRoom)
    {
        
        UIManager.instance.AbilityMenuOn();
        if(abil.uiIcon != null)
        { icon = abil.uiIcon; }
        Name.text = abil.abilityName;
        description.text = abil.ablitiyDisctriptions;
        price.text = abil.abilityCost.ToString();
        purchaseButton.SetActive(true);
        shownPrice.SetActive(true);
        if (isItemRoom)
        {
            itemComfirm.text = "Confirm";
            shownPrice.SetActive(false);
        }
    }

    public void ShowAbilityInventory()
    {
        UIManager.instance.SetMenu(inventoryMenu);
    }
}
