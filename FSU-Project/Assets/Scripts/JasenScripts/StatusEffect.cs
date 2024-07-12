using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

public enum StatusEffectType
{
    Slow,
    Freeze,
    Burn,
    Poison
}
/*public class StatusEffectable : MonoBehaviour
{
    private Dictionary<StatusEffectType, bool> activeEffects = new Dictionary<StatusEffectType, bool>();
    private Dictionary<StatusEffectType, Coroutine> effectCoroutines = new Dictionary<StatusEffectType, Coroutine>();

    void Start()
    {
        foreach (StatusEffectType effectType in System.Enum.GetValues(typeof(StatusEffectType)))
        {
            activeEffects[effectType] = false;
        }
    }

    public void ApplyStatusEffect(StatusEffectType effectType, float amount, float duration)
    {
        if (!activeEffects[effectType])
        {
            activeEffects[effectType] = true;
            if (effectCoroutines.ContainsKey(effectType) && effectCoroutines[effectType] != null)
            {
                StopCoroutine(effectCoroutines[effectType]);
            }
            effectCoroutines[effectType] = StartCoroutine(StatusEffectCoroutine(effectType, amount, duration));
        }
    }

    private IEnumerator StatusEffectCoroutine(StatusEffectType effectType, float amount, float duration)
    {
        switch (effectType)
        {
            case StatusEffectType.Slow:
                ApplySlow(amount);
                break;
            case StatusEffectType.Freeze:
                ApplyFreeze();
                break;
            case StatusEffectType.Burn:
                yield return BurnEffect(amount, duration);
                break;
            case StatusEffectType.Poison:
                yield return PoisonEffect(amount, duration);
                break;
            
        }

        yield return new WaitForSeconds(duration);

        switch (effectType)
        {
            case StatusEffectType.Slow:
            case StatusEffectType.Freeze:
                ResetSpeed();
                break;
        }

        activeEffects[effectType] = false;
    }

    private void ApplySlow(float slowAmount)
    {
       // var enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.speed *= slowAmount;
        }
    }

    private IEnumerator BurnEffect(float burnDamage, float duration)
    {
        var enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                enemy.TakeDamage(burnDamage);
                elapsed += 1f;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private IEnumerator PoisonEffect(float poisonDamage, float duration)
    {
        var enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                enemy.TakeDamage(poisonDamage);
                elapsed += 1f;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void ApplyFreeze()
    {
        var enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.speed = 0f;
        }
    }

    private void ResetSpeed()
    {
        var enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ResetSpeed();
        }
    }
}*/