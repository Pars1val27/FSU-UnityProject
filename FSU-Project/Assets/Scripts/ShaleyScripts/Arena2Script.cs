using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEditor.AI;

public class Arena2Script : MonoBehaviour
{
    [SerializeField] GameObject[] QuadPresets;

    int lastDir;
    int lastPreset;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        lastDir = 0;
        lastPreset = -1;
        RandArena();
        gameManager.instance.surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandArena()
    {
        CreateRandQuad(0, 0);
        CreateRandQuad(125, 0);
        CreateRandQuad(0, 125);
        CreateRandQuad(125, 125);
    }

    void CreateRandQuad(int x, int z)
    {
        int dir = RandDir() * 90;
        GameObject Quad = Instantiate(RandPreset(), new Vector3(x, 0, z), new Quaternion(0, 1, 0, 0));
        //Quad.transform.
    }
    GameObject RandPreset()
    {
        //System.Random randPreset = new System.Random();
        //int numPresets = QuadPresets.Length;
        //int preset = randPreset.Next(0, numPresets);
        //return QuadPresets[preset];
        int numPresets = QuadPresets.Length;
        int preset = UnityEngine.Random.Range(0, numPresets);
        if(lastPreset == preset)
        {
            preset = UnityEngine.Random.Range(0, numPresets);
        }
        lastPreset = preset;
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
