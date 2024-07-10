using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilityhandler : MonoBehaviour
{
    private PlayerController playerController;
    private GunScript gunScript;
    private SwordScript swordScript;
    private Grenade grenade;
    private List<Ability> abilities = new List<Ability>();

    void Start()
    {
        playerController = GetComponent<PlayerController>();

    }

    public void IncreaseMaxHP(int amount)
    {
        if (playerController != null)
        {
            //playerController.maxHP += amount;
            //playerController.currHP = playerController.maxHP;
            // Update UI or other logic to reflect new HP value
        }
    }


    public void AddAbility(Ability ability)
    {
        if (!abilities.Contains(ability))
        {
            abilities.Add(ability);
            ability.Activate(gameObject);
        }
    }
}
