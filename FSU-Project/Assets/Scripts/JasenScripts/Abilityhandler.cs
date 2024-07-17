using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityHandler : MonoBehaviour
    {
        static public AbilityHandler handlerInstance;
        public PlayerController playerController;
        public GunScript gunScript;
        public SwordScript swordScript;

        public List<Ability> abilities = new List<Ability>();
        //public Ability[] abilities;
        public bool isHPRecoveryEnabled = false;
        private Coroutine hpRecoveryCoroutine;
        private int recoveryAmount;
        private float recoveryRate;

        void Start()
        {

            handlerInstance = this;
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
        public void EnableHPRecovery(int amount, float rate)
        {
            recoveryAmount = amount;
            recoveryRate = rate;

            isHPRecoveryEnabled = true;

            if (hpRecoveryCoroutine == null)
            {
                hpRecoveryCoroutine = StartCoroutine(HPRecovery());
            }
        }

        private IEnumerator HPRecovery()
        {
            while (isHPRecoveryEnabled)
            {
                yield return new WaitForSeconds(recoveryRate);
                if (playerController.playerHP < playerController.origHP)
                {
                    playerController.playerHP += recoveryAmount;
                    
                    playerController.UpdatePlayerUI();
                }
                else
                {
                    isHPRecoveryEnabled = false;
                }
            }
            hpRecoveryCoroutine = null;
        }

        public void DisableHPRecovery()
        {
            isHPRecoveryEnabled = false;
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