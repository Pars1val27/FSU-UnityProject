using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireDamage 
{
    void ApplyFireDamage(int fireDamage, float duration);
}
/*
 public void ApplyFireDamage(float damageOverTime, float duration)
    {
        StartCoroutine(FireDamageCoroutine(damageOverTime, duration));
    }

 private IEnumerator FireDamageCoroutine(float damageOverTime, float duration)
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