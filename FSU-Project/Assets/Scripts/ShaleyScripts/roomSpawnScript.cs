using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomSpawnScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Clock.timeInstance.StartTimer();
        }
    }
}
