using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class gameManager : MonoBehaviour
{
    //public PlayerClass Gunner;
    public GameObject player;
    public PlayerController playerScript;

    public static gameManager instance;

    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    public void SpawnPlayer(GameObject spawnPos)
    {
        gameManager.instance.playerScript.controller.enabled = false;
        gameManager.instance.player.transform.position = spawnPos.transform.position;
        gameManager.instance.playerScript.controller.enabled = true;

    }

}