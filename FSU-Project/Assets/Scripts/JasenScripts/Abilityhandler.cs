using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace AbilitySystem
{
    public class AbilityHandler : MonoBehaviour
    {
        public static AbilityHandler handlerInstance;
        public PlayerController playerController;
        public GunScript gunScript;
        public SwordScript swordScript;

        public List<Ability> abilities = new List<Ability>();
        public bool isHPRecoveryEnabled = false;
        private Coroutine hpRecoveryCoroutine;
        private int recoveryAmount;
        private float recoveryRate;

        public bool hasFireEffect = false;
        public bool hasPoisonEffect = false;
        public bool hasSlowEffect = false;
        public bool hasFreezeEffect = false;

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


        public void AddAbility(Ability ability)
        {
            if (!abilities.Contains(ability))
            {
                Debug.Log(ability.abilityName + " added to Abilities");
                
                abilities.Add(ability);

                ActivateAbilityFlags(ability);
            }
        }
        private void ActivateAbilityFlags(Ability ability)
        {
            if (ability is FireEffect)
                hasFireEffect = true;
            else if (ability is PoisonEffect)
                hasPoisonEffect = true;
            else if (ability is SlowedEffect)
                hasSlowEffect = true;
            else if (ability is FreezeEffect)
                hasFreezeEffect = true;
        }

        public void ApplyFireDamage(GameObject target, FireEffect fireEffect)
        {
            var flammable = target.GetComponent<IFireDamage>();
            if (flammable != null)
            {
                flammable.ApplyFireDamage(fireEffect.fireDamage, fireEffect.duration);
            }
            else
            {
                Debug.Log(target.name + " Can not Burned");
            }
        }

        public void ApplyPoisonDamage(GameObject target, PoisonEffect poisonEffect)
        {
            Debug.Log("ApplyPoisonDamage has been called" );
            var poisonable = target.GetComponent<IPoisonDamage>();
            if (poisonable != null)
            {
                poisonable.ApplyPoisonDamage(poisonEffect.poisonDamage, poisonEffect.duration);
            }
            else 
            {
                Debug.Log(target.name + " Can not be Posioned");
            }

        }

        public void ApplySlow(GameObject target, SlowedEffect slowEffect)
        {
            var slowable = target.GetComponent<ISlow>();
            if (slowable != null)
            {
                slowable.ApplySlow(slowEffect.slowAmount, slowEffect.duration);
            }
            else
            {
                Debug.Log(target.name + " Can not be Slowed");
            }
        }

        public void ApplyFreeze(GameObject target, FreezeEffect freezeEffect)
        {
            var freezable = target.GetComponent<IFreeze>();
            if (freezable != null)
            {
                freezable.ApplyFreeze(freezeEffect.duration);
            }
            else
            {
                Debug.Log(target.name + " Can not be Frozen");
            }
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

        
        
    }
}