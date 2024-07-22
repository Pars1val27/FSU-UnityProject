using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        //HpRecovery
        public bool hasHPRecovery = false;
        public bool isHPRecoveryEnabled = false;
        public int recoveryAmount;
        public float recoveryRate;



        //Status Effects 
        public bool hasFireEffect = false;
        public bool hasPoisonEffect = false;
        public bool hasSlowEffect = false;
        public bool hasFreezeEffect = false;
        public bool hasDamageReduction = false;

        //Reflect damage
        public bool hasReflectDamage = false;
        public int reflectDamagePercentage;

        //reduced damage
        public bool HasReduseddamage = false;
        public float ReducedDamagePercentage;

        //One hit shield
        public bool hasOneHitShield = false;
        public bool isOneHitShieldActive;
        public int shieldRecharge;

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

        public void AddAbility(Ability ability)
        {
            if (!abilities.Contains(ability))
            {
                Debug.Log(ability.abilityName + " added to Abilities");

                abilities.Add(ability);

                //ActivateAbilityFlags(ability);
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
            else if (ability is ReflectDamageAbility)
                hasReflectDamage = true;
            else if (ability is DamageReductionAbility)
            {

                hasDamageReduction = true;
            }
            else if (ability is OneHitShieldAbility)
            {
                hasOneHitShield = true; 
                Debug.Log("onehitshield = true");

                
            }



        }


        public void ApplyFireDamage(GameObject target, FireEffect fireEffect)
        {
            var flammable = target.GetComponent<IFireDamage>();
            if (flammable != null)
            {
                flammable.ApplyFireDamage(fireEffect.fireDamage, fireEffect.duration, fireEffect.fireEffectPrefab);
            }
            else
            {
                Debug.Log(target.name + " Can not Burned");
            }
        }

        public void ApplyPoisonDamage(GameObject target, PoisonEffect poisonEffect)
        {
            Debug.Log("ApplyPoisonDamage has been called");
            var poisonable = target.GetComponent<IPoisonDamage>();
            if (poisonable != null)
            {
                poisonable.ApplyPoisonDamage(poisonEffect.poisonDamage, poisonEffect.duration, poisonEffect.PoisonEffectPrefab);
            }
            else
            {
                Debug.Log(target.name + " Can not be Posioned");
            }

        }

        public void ApplySlow(GameObject target, SlowedEffect slowEffect )
        {
            var slowable = target.GetComponent<ISlow>();
            if (slowable != null)
            {
                slowable.ApplySlow(slowEffect.slowAmount, slowEffect.duration, slowEffect.slowEffectPrefab);
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
                freezable.ApplyFreeze(freezeEffect.duration, freezeEffect.freezeEffectPrefab);
            }
            else
            {
                Debug.Log(target.name + " Can not be Frozen");
            }
        }
        public void ApplyReflectDamage(GameObject damageSource, int damageAmount)
        {
            //Removed

            //if (hasReflectDamage)
            //{
            //    var damageable = damageSource.GetComponent<IDamage>();
            //    if (damageable != null)
            //    {

            //        int reflectedDamage = Mathf.CeilToInt(damageAmount * reflectDamagePercentage / 100);
            //        //damageable.TakeDamage(reflectedDamage, gameObject); 
            //        Debug.Log("Reflect Damage applied to: " + damageSource.name + " for amount: " + reflectedDamage);
            //    }
            //}
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
            playerController.maxStamina += amount;
            playerController.UpdatePlayerUI();
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

        public int CalculateReducedDamage(int damage)
        {
            if (hasDamageReduction)
            {
                int reducedDamage = Mathf.CeilToInt(damage * (1 - ReducedDamagePercentage / 100f));
                Debug.Log("Damage reduced by " + ReducedDamagePercentage + "%, new damage: " + reducedDamage);
                return reducedDamage;
            }
            return damage;
        }

        public void EnableHPRecovery(int amount, float rate)
        {
            isHPRecoveryEnabled = true;
            StartCoroutine(HPRecovery());
        }

        private IEnumerator HPRecovery()
        {
            
            while (playerController.playerHP < playerController.origHP)
            {
                yield return new WaitForSeconds(recoveryRate);
                playerController.playerHP += recoveryAmount;
                if (playerController.playerHP >= playerController.origHP)
                {
                    playerController.playerHP = playerController.origHP;
                    playerController.UpdatePlayerUI();
                    isHPRecoveryEnabled = false;
                    yield break;
                }
                playerController.UpdatePlayerUI();
            }
            isHPRecoveryEnabled = false;
        }
        public void ActivateOneHitShield(OneHitShieldAbility RechargeTime)
        {
            shieldRecharge = RechargeTime.rechargeTime;
            Debug.Log("one hit shild active");
            isOneHitShieldActive = true;
        }

        public void DeactivateOneHitShield()
        {
            if (isOneHitShieldActive)
            {
                Debug.Log("one hit shild used");
                isOneHitShieldActive = false;
                StartCoroutine(RechargeShield());
            }
        }

        private IEnumerator RechargeShield()
        {
            yield return new WaitForSeconds(shieldRecharge);
            isOneHitShieldActive = true;
            Debug.Log("one hit shild Recharged");

        }
    }
}