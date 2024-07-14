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

        public bool isStorePickup;
        public bool IsDebugAbility = false;

        void Start()
        {
            abilitiesUI = FindObjectOfType<AbilitiesUI>();
        }

        void OnTriggerEnter(Collider other)
        {
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
        }
        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player left ability pickup radius.");
                HidePickupUI();
            }
        }
        private void ShowPickupUI(GameObject player)
        {
            if (isStorePickup)
            {
                
            }
            else
            {

            }
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
                    if (isStorePickup)
                    {
                        if (HasEnoughTime(player))
                        {
                            RemoveTime(player);
                            abilityHandler.AddAbility(ability);
                            AbilityManager.Instance.ActivateAbility(ability.abilityName, player);
                            Debug.Log("Ability " + ability.abilityName + " activated.");
                            Destroy(gameObject);
                            Debug.Log("Ability pickup destroyed.");
                        }
                        else
                        {
                            Debug.Log("Not enough resources to pick up " + ability.abilityName);
                        }
                    }
                    else
                    {
                        abilityHandler.AddAbility(ability);
                        AbilityManager.Instance.ActivateAbility(ability.abilityName, player);
                        Debug.Log("Ability " + ability.abilityName + " activated.");
                        Destroy(gameObject);
                        Debug.Log("Ability pickup destroyed.");
                    }
                }
                else
                {
                    Debug.Log("Player already has ability " + ability.abilityName);
                    Destroy(gameObject);
                }
            }
        }



        private bool HasEnoughTime(GameObject player)
        {
            return true;
        }

        private void RemoveTime(GameObject player)
        {
           
        }


    }
}