using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityHandler : MonoBehaviour
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
            gunScript = playerController.GetComponentInChildren<GunScript>();

        }

        public void IncreaseMaxHP(int amount)
        {
            if (playerController != null)
            {
                //Debug.Log(Equals(playerController.playerHP));
                playerController.origHP += amount;
                //playerController.playerHP = playerController.origHP;
                // Update UI or other logic to reflect new HP value
                //playerController.UpdatePlayerUI();
                //Debug.Log(playerController.origHP);
                //Debug.Log(Equals(playerController.playerHP));
            }
        }

        public void IncreaseSpeed(int amount)
        {
            playerController.speed += amount;    
        }

        public void IncreaseRateOfFire(float amount)
        {
            playerController.attackSpeed -= amount; 
        }

        public void IncreaseStamina(int amount)
        {
            
        }

        public void IncreaseDamage(int amount)
        {
            playerController.damage += amount;
            // Update UI 
        }

        public void IncreaseAttackSpeed(float amount)
        {
            playerController.attackSpeed -= amount; 
        }

        public void IncreaseMaxAmmo(int amount)
        {
            
            if (gunScript != null)
            {
                gunScript.maxAmmo += amount;
                //gunScript.currAmmo = gunScript.maxAmmo;
                // Update UI 
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
}