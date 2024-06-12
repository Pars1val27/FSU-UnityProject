using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
//Shaley

public class Arena2Script : MonoBehaviour
{
    [SerializeField] GameObject[] QuadPresets;
    [SerializeField] EnemyManager enemyManager;

    int lastDir;
    int lastPreset;

    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        lastDir = 0;
        lastPreset = -1;
        RandArena();
        gameManager.instance.surface.BuildNavMesh();
        //EnemyManager.instance.enemies
        
    }

    void Update()
    {
        
    }

    void RandArena()
    {
        CreateRandQuad(0, 0);
        CreateRandQuad(62.5f, 0);
        CreateRandQuad(0, 62.5f);
        CreateRandQuad(62.5f, 62.5f);
    }

    void CreateRandQuad(float x, float z)
    {
        int dir = RandDir();
        GameObject Quad = Instantiate(RandPreset());
        Quad.transform.localPosition = new Vector3(x, 0, z);
        Quad.transform.localEulerAngles = new Vector3(0, dir, 0);
    }
    GameObject RandPreset()
    {
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
        return dir * 90;
    }


}
