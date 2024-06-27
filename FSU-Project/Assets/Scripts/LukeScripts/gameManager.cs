using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class gameManager : MonoBehaviour
{
    //public PlayerClass Gunner;
    [SerializeField] public GameObject player;

    [SerializeField] GameObject spawnPos;

    public static gameManager instance;

    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void SpawnPlayer()
    {
        Instantiate(gameManager.instance.player, new UnityEngine.Vector3(
            spawnPos.transform.position.x, spawnPos.transform.position.y, spawnPos.transform.position.z),
            new UnityEngine.Quaternion(spawnPos.transform.rotation.x, spawnPos.transform.rotation.y,
            spawnPos.transform.rotation.z, spawnPos.transform.rotation.w));
    }

}
