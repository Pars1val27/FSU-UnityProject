using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class gameManager : MonoBehaviour
{
    public PlayerClass Gunner;

    public static gameManager instance;

    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Gunner.SaveDefault();
        LoadPlayerClass();
        
    }

    void OnApplicationQuit()
    {
        ResetPlayerClass();
    }

    public void SavePlayerClass()
    {
        Gunner.Save();
        Debug.Log("Player class saved");
    }

    public void LoadPlayerClass()
    {
        Gunner.Load();
        Debug.Log("Player class loaded");
    }

    public void ResetPlayerClass()
    {
        Gunner.ResetToDefault();
        Debug.Log("Player class reset to default values");
    }


}
