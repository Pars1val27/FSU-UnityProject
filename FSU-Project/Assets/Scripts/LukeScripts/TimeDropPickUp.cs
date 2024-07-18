using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeDropPickUp : MonoBehaviour
{
    [Range(1, 10)] public int seconds;
    [SerializeField] TextMeshPro text;

    Vector3 playerDir;
    Vector3 playerPos;

    void Start()
    {
        text.text = seconds.ToString();
    }

    private void Update()
    {
        playerPos = EnemyManager.instance.player.transform.position;
        playerDir = playerPos - transform.position;
        Quaternion rot = Quaternion.LookRotation(-new Vector3(playerDir.x, 0, playerDir.z));
        text.transform.rotation = rot;
    }

    private void OnTriggerEnter(Collider other)
    {
        Clock.timeInstance.EditTIme(seconds);
        Destroy(gameObject);
    }
}
