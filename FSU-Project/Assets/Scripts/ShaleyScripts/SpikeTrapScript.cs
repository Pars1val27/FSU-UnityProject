using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Collider collider;
    [SerializeField] int dmg;
    bool readyForOpen;
    bool isPlaying;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying == true) {
            return;
        }
        if (readyForOpen)
        {
            isPlaying = true;
            StartCoroutine(WaitForTrapOpen());
        }
        else
        {
            isPlaying = true;
            StartCoroutine(WaitForTrapClose());
        }
    }
    IEnumerator WaitForTrapClose()
    {
        yield return new WaitForSeconds(1);
        anim.Play("Close Trap");
        collider.enabled = false;
        isPlaying = false;
        readyForOpen = true;
    }
    IEnumerator WaitForTrapOpen()
    {
        yield return new WaitForSeconds(3);
        anim.Play("Open Trap");
        collider.enabled = true;
        isPlaying = false;
        readyForOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.playerInstance.TakeDamage(dmg);
        }
    }
}
