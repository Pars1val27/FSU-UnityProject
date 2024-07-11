using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public Ability ability;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(" pick up ");
            var abilityHandler = other.GetComponent<Abilityhandler>();
            if (abilityHandler != null && ability != null)
            {
                abilityHandler.AddAbility(ability);
                //Debug.Log(" activate " + ability.abilityName);
                //Debug.Log(ability.abilityName + " removed for spawnable");
                AbilityManager.Instance.ActivateAbility(ability.abilityName, other.gameObject);

                Destroy(gameObject);
                //Debug.Log("ability destroyed");
            }
        }
    }
}
