using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoisonDamage 
{
    void ApplyPoisonDamage(int poisonDamage, float duration , GameObject poisonEffect);
}

/*
 public void ApplyPoisonDamage(float damageOverTime, float duration)
    {
        StartCoroutine(PoisonDamageCoroutine(damageOverTime, duration));
    }

    
  private IEnumerator PoisonDamageCoroutine(float damageOverTime, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            TakeDamage(damageOverTime);
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
    }
 */