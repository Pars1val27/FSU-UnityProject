using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Arena2Script : MonoBehaviour
{
    [SerializeField] GameObject[] QuadPresets;

    int lastDir;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        lastDir = 0;
        RandArena();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandArena()
    {
        CreateRandQuad(0, 0);
        CreateRandQuad(250, 0);
        CreateRandQuad(0, 250);
        CreateRandQuad(250, 250);
    }

    void CreateRandQuad(int x, int z)
    {
        int dir = RandDir();
        GameObject Quad1 = Instantiate(RandPreset(), new Vector3(x, 0, z), new Quaternion(0, dir*90, 0, 0));
    }
    GameObject RandPreset()
    {
        System.Random randPreset = new System.Random();
        int numPresets = QuadPresets.Length;
        int preset = randPreset.Next(0, numPresets);
        return QuadPresets[preset];
    }
    int RandDir()
    {
        int dir = UnityEngine.Random.Range(-1, 3);
        if(lastDir == dir)
        {
            dir = UnityEngine.Random.Range(-1, 3);
        }
        lastDir = dir;
        return dir;
    }


}
