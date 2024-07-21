using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomShopItemScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Clock.timeInstance.StopTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Clock.timeInstance.StartTimer();
        }
    }
}
