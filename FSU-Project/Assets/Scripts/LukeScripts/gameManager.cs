using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class gameManager : MonoBehaviour
{
    public PlayerClass playerClass;

    private string saveFilePath;


    public static gameManager instance;

    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

 

    // Update is called once per frame
    void Update()
    {
        SavePlayerClass();
    }

    public void SavePlayerClass()
    {
        playerClass.Save();
        Debug.Log("Player class saved");
    }

    public void LoadPlayerClass()
    {
        playerClass.Load();
        Debug.Log("Player class loaded");
    }

    public void ResetToDefault()
    {
        PlayerPrefs.DeleteAll();
        playerClass.Load();
        Debug.Log("Player class reset to default values");
    }
}
