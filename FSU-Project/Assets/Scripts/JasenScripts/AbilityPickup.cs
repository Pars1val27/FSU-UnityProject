using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityPickup : MonoBehaviour
    {
        public Ability ability;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player triggered ability pickup.");
                var abilityHandler = other.GetComponent<AbilityHandler>();
                if (abilityHandler != null && ability != null)
                {
                   
                    if (!abilityHandler.HasAbility(ability.abilityName))
                    {
                        abilityHandler.AddAbility(ability);
                        //Debug.Log("Ability " + ability.abilityName + " added to handler.");
                        AbilityManager.Instance.ActivateAbility(ability.abilityName, other.gameObject);
                        Debug.Log("Ability " + ability.abilityName + " activated.");
                        Destroy(gameObject);
                        Debug.Log("Ability pickup destroyed.");
                    }
                    else
                    {
                        Debug.Log("Player already has ability " + ability.abilityName + ".");
                        Destroy(gameObject);
                    }
                }
                
            }
        }
       
    }
}