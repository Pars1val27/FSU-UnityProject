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

        void Start()
        {
            abilitiesUI = FindObjectOfType<AbilitiesUI>();
        }

        void OnTriggerEnter(Collider other)
        {
            
                if (other.CompareTag("Player"))
                    if (IsDebugAbility)
                    {
                        ConfirmPickup(other.gameObject);
                    }
                    else
                    {
                        ShowPickupUI(other.gameObject);
                    }
            
        }
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
                HidePickupUI();
            }
        }
        private void ShowPickupUI(GameObject player)
        {
            
                abilitiesUI.ShowAbilityItem(ability, isItemPickup);
            
        }

        private void HidePickupUI()
        {
            
        }

        void ConfirmPickup(GameObject player)
        {
            var abilityHandler = player.GetComponent<AbilityHandler>();
            if (abilityHandler != null && ability != null)
            {
                if (!abilityHandler.HasAbility(ability.abilityName))
                {
                    if (isItemPickup)
                    {
                        if (HasEnoughTime(player))
                        {
                            RemoveTime(player);
                            abilityHandler.AddAbility(ability);
                            AbilityManager.Instance.ActivateAbility(ability.abilityName, player);
                            Debug.Log("Ability " + ability.abilityName + " activated.");
                            Destroy(gameObject);
                            //Debug.Log("Ability pickup destroyed.");
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
                        //Debug.Log("Ability pickup destroyed.");
                    }
                }
                else
                {
                    Debug.Log("Player already has ability " + ability.abilityName);
                    Destroy(gameObject);
                }
            }
        }

        //make time remaining public and change edit time to a float

        private bool HasEnoughTime(GameObject player)
        {
            var timer = player.GetComponent<Timer>();
            return timer != null && timer.remainingTime >= ability.abilityCost; 
        }

        private void RemoveTime(GameObject player)
        {
            var timer = player.GetComponent<Timer>();
            if (timer != null)
            {
                //timer.EditTime(-ability.abilityCost);
            }
        }

        
    }
}