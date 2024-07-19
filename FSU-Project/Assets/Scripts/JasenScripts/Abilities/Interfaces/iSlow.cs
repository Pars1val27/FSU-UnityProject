using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlow
{
    void ApplySlow(float slowAmount, float duration);
    void RemoveSlow();
}

/*
    float originalSpeed;
    float origAttackRate

    float attackRate;
    float slowMultiplier;

//declare this in start after navMesh Agent
    originalSpeed = agent.speed;
    origAttackRate = attackRate;

    public void ApplySlow(float slowAmount, float duration)
        {
            if (!isSlowed)
            {
                isSlowed = true;
                slowMultiplier = slowAmount;
                agent.speed *= slowAmount;
                attackRate *= slowAmount;
                StartCoroutine(SlowCoroutine(duration));
            }
        }

        public void RemoveSlow()
         {
            isSlowed = false;
            agent.speed = originalSpeed;
            attackRate = origAttackRate;
        
        }
     private IEnumerator SlowCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            RemoveSlow();
        }
*/