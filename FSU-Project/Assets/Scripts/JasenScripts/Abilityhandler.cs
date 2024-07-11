using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilityhandler : MonoBehaviour
{
    //public GameObject player;
    public PlayerController playerController;
    public GunScript gunScript;
    public SwordScript swordScript;
    public Grenade grenade;
    public List<Ability> abilities = new List<Ability>();

    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        playerController = GetComponent<PlayerController>();
        
    }

    public void IncreaseMaxHP(int amount)
    {
        if (playerController != null)
        {
            playerController.origHP += amount;
            //playerController.playerHP = playerController.origHP;
            // Update UI or other logic to reflect new HP value
        }
    }


    public void AddAbility(Ability ability)
    {
        if (!abilities.Contains(ability))
        {
            Debug.Log(ability + " added tolist");
            abilities.Add(ability);
            
            ability.Activate(gameObject);
        }
    }
}
