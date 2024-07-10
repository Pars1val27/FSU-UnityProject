using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    public Ability ability;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var abilityHandler = other.GetComponent<Abilityhandler>();
            if (abilityHandler != null && ability != null)
            {
                ability.Activate(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
