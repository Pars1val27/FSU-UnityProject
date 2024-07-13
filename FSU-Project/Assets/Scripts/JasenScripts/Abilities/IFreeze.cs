using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFreeze 
{
    void ApplyFreeze(float duration);
}
/*
create bool 
bool isFrozen;

public void ApplyFreeze(float duration)
{
    if (!isFrozen)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }
}

private IEnumerator FreezeCoroutine(float duration)
{
    isFrozen = true;
    agent.isStopped = true;
    yield return new WaitForSeconds(duration);
    agent.isStopped = false;
    isFrozen = false;
}*/

