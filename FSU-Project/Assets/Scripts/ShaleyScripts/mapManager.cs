using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    public static mapManager instance;

    //the array position is map level - 1
    [SerializeField] GameObject mapObj;
    [SerializeField] maps[] maps;
    public maps mapLevel;
    GameObject map;

    private void Awake()
    {
        instance = this;
        map = Instantiate(mapObj, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
    }

    private void Start()
    {
        mapLevel = maps[0];
    }


    public void DestroyMap()
    {
        Destroy(map);
    }
}
