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
        
    }

    public void SavePlayerClass()
    {
    //should save all attributes listed in PlayerSaveData.CS will need updated after melees class is complete
    }

    public void LoadPlayerClass()
    {

    }

    public void ResetToDefault()
    {

    }
}
