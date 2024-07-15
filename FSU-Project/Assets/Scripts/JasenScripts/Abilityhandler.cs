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

        private bool isHPRecoveryEnabled = false;

         Coroutine hpRecoveryCoroutine;


        void Start()
        {
            
            playerController = GetComponent<PlayerController>();

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

            gunScript.maxAmmo += amount;
            gunScript.currAmmo = gunScript.maxAmmo;
            gunScript.UpdateAmmoCount();

        }
        public void EnableHPRecovery(int amount, float interval)
        {
            StartCoroutine(HPRecoveryCoroutine(amount, interval));
      
        }

        private IEnumerator HPRecoveryCoroutine(int amount, float interval)
        {
            while (playerController.playerHP < playerController.origHP)
            {
                playerController.playerHP += amount;
                if (playerController.playerHP > playerController.origHP)
                {
                    playerController.playerHP = playerController.origHP;
                }
                playerController.UpdatePlayerUI();
                yield return new WaitForSeconds(interval);
            }
            hpRecoveryCoroutine = null;
        }


        public void AddAbility(Ability ability)
        {
            if (!abilities.Contains(ability))
            {
                Debug.Log(ability + " added to Abilities");
                abilities.Add(ability);

                ability.Activate(gameObject);
            }
        }
    }
}