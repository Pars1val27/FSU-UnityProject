using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityHandler : MonoBehaviour
    {
        
        public PlayerController playerController;
        public GunScript gunScript;
        public SwordScript swordScript;
        
        public List<Ability> abilities = new List<Ability>();

        void Start()
        {
            
            playerController = GetComponent<PlayerController>();
            gunScript = playerController.GetComponentInChildren<GunScript>();

        }
        void Update()
        {    
            if (gunScript == null && playerController.gunScript != null)
            {
                gunScript = playerController.gunScript;
            }

            
            if (swordScript == null && playerController.swordScript != null)
            {
                swordScript = playerController.swordScript;
            }
        }
        public bool HasAbility(string abilityName)
        {
            foreach (var ability in abilities)
            {
                if (ability.abilityName == abilityName)
                {
                    return true;
                }
            }
            return false;
        }
        public Ability GetAbility(string abilityName)
        {
            foreach (var ability in abilities)
            {
                if (ability.abilityName == abilityName)
                {
                    return ability;
                }
            }
            return null;
        }

        public void IncreaseMaxHP(int amount)
        {
             playerController.origHP += amount;
             playerController.playerHP = playerController.origHP; 
             playerController.UpdatePlayerUI();

        }

        public void IncreaseSpeed(int amount)
        {
            playerController.speed += amount;    
        }



        public void IncreaseStamina(int amount)
        {

        }

        public void IncreaseDamage(int amount)
        {
            playerController.damage += amount;
            
        }

        public void IncreaseAttackSpeed(float amount)
        {
            playerController.attackSpeed -= amount; 
        }

        public void IncreaseMaxAmmo(int amount)
        {
            
                Debug.Log("gunscript not null");
                gunScript.maxAmmo += amount;
                gunScript.currAmmo = gunScript.maxAmmo;
                gunScript.UpdateAmmoCount();
            

        }


        public void AddAbility(Ability ability)
        {
            if (!abilities.Contains(ability))
            {
                Debug.Log(ability + " added to Abilities");
                abilities.Add(ability);

                //ability.Activate(gameObject);
            }
        }
    }
}