using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityPickup : MonoBehaviour
    {
        public Ability ability;
        public AbilitiesUI abilitiesUI;

        public bool isItemPickup;
        public bool IsDebugAbility = false;

        private Clock timer;

        private static bool isPickupActive = false;

        void Start()
        {
            abilitiesUI = FindObjectOfType<AbilitiesUI>();
            timer = UIManager.instance.GetComponent<Clock>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isPickupActive)
            {
                isPickupActive = true;
                if (IsDebugAbility)
                    { 
                    ConfirmPickup(other.gameObject);
                    }
                else
                    {
                        ShowPickupUI(other.gameObject);
                    }
            }
        }
        void OnTriggerExit(Collider other)
        {      
            if (other.CompareTag("Player"))
            {
                HidePickupUI();
                isPickupActive = false;
            }
        }
        private void ShowPickupUI(GameObject player)
        {
            if (abilitiesUI != null)
            {
                abilitiesUI.currentPickup = this;
                abilitiesUI.ShowAbilityItem(ability, isItemPickup);
            }
        }

        private void HidePickupUI()
        {
            if (UIManager.instance != null)
            {
                UIManager.instance.AbilityMenuOff();
            }
            else
            {
                Debug.LogError("UIManager instance is null");
            }

            if (abilitiesUI != null)
            {
                abilitiesUI.currentPickup = null;
            }
            isPickupActive = false;
        }

        public void ConfirmPickup(GameObject player)
        {
            if (ability == null || player == null) return; 

            var abilityHandler = player.GetComponent<AbilityHandler>();
            if (abilityHandler != null && !abilityHandler.HasAbility(ability.abilityName))
            {
                if (isItemPickup)
                {
                    if (HasEnoughTime())
                    {
                        RemoveTime();
                        abilityHandler.AddAbility(ability);
                        AbilityManager.Instance.ActivateAbility(ability.abilityName, player);
                        Debug.Log("Ability " + ability.abilityName + " activated.");
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Not enough time to buy " + ability.abilityName);
                    }
                }
                else
                {
                    abilityHandler.AddAbility(ability);
                    AbilityManager.Instance.ActivateAbility(ability.abilityName, player);
                    Debug.Log("Ability " + ability.abilityName + " activated.");
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.Log("Player already has ability " + ability.abilityName);
                Destroy(gameObject);
            }

            isPickupActive = false;
        }

        //make time remaining public and change edit time to a float


        private bool HasEnoughTime()
        {
            if (timer != null)
            {
                Debug.Log("HasEnoughTime called. Current remaining time: " + timer.GetRemainingTime());
                return timer.GetRemainingTime() >= ability.abilityCost;
            }
            //Debug.Log("Clock component not found.");
            return false;
        }

        private void RemoveTime()
        {
            if (timer != null)
            {
                Debug.Log("Removed time: " + ability.abilityCost);
                timer.EditTIme(-ability.abilityCost);   
            }
            
        }


    }
}