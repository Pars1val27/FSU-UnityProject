using AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{
    static public AbilitiesUI abilitiesUI;
    [SerializeField] Button purchaseButton;
    [SerializeField] GameObject shownPrice;
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] GameObject[] abilityIcons; 
    public Image icon;
    public TMP_Text Name;
    public TMP_Text description;
    public TMP_Text price;

    public Image[] ownedAbil;

    public AbilityPickup currentPickup;

    bool invActive;


    // Start is called before the first frame update
    void Start()
    {
        abilitiesUI = this;
        purchaseButton.onClick.AddListener(OnButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Debug.Log("Input accepted");
            UIManager.instance.ShowAbilityInventory();
            Debug.Log("Ability Menu Open");
        }
    }

        public void ShowAbilityItem(Ability abil, bool isItemRoom)
    {
        
        UIManager.instance.AbilityMenuOn();
        if(abil.uiIcon != null)
        { icon.sprite = abil.uiIcon; }
        Name.text = abil.abilityName;
        description.text = abil.ablitiyDisctriptions;
        price.text = abil.abilityCost.ToString();
        purchaseButton.gameObject.SetActive(true);
        purchaseButton.GetComponentInChildren<TMP_Text>().text = "Purchase";
        shownPrice.SetActive(true);
        if (isItemRoom)
        {
            //purchaseButton.gameObject.SetActive(true);
            purchaseButton.GetComponentInChildren<TMP_Text>().text = "Pick Up";
            shownPrice.SetActive(false);
        }
        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(OnButtonPressed);

    }

    public void ShowAbilityInventory()
    {
            int index = 0;
            foreach (var ability in AbilityHandler.handlerInstance.abilities)
            {
                abilityIcons[index].SetActive(true);
                Debug.Log("Icons aactivated");
                ownedAbil[index].sprite = ability.uiIcon;
                Debug.Log("Icon Set");
                ++index;
            }
    }
    public void OnButtonPressed()
    {
        if (currentPickup != null)
        {
            currentPickup.ConfirmPickup(GameObject.FindGameObjectWithTag("Player"));
            UIManager.instance.AbilityMenuOff();
        }
    }
}
