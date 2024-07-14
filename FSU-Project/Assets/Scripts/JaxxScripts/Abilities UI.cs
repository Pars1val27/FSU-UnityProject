using AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


        public void ShowAbilityItem(Ability abil,bool isItemRoom)
    {
        if(isItemRoom) 
        {
        purchaseButton.SetActive(false);
        shownPrice.SetActive(false);
        }else{
            purchaseButton.SetActive(true);
            shownPrice.SetActive(true);
            price.text = "Cost: " + abil.abilityCost.ToString();

        }

        UIManager.instance.statePause();
        if (abil.uiIcon != null)
        {
            icon.sprite = abil.uiIcon;
        }
        
        Name.text = abil.abilityName;
        description.text = abil.ablitiyDisctriptions;
        //price = abil.price
    }


    public void ShowAbilityInventory()
    {
        if(Input.GetButtonDown("B"))
        {

        }
    }
}
