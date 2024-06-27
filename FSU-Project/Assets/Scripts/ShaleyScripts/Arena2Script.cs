using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
//Shaley

public class Arena2Script : MonoBehaviour
{
    [SerializeField] GameObject[] QuadPresets;
    //make direction same as portal direction

    int lastDir;
    int lastPreset;
    public static bool isPlayerSpawned;
    
    public void StartArena()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        lastDir = 0;
        lastPreset = -1;
        RandArena();
        gameManager.instance.surface.BuildNavMesh();
    }

    void RandArena()
    {
        CreateRandQuad(0, 0); //top left
        CreateRandQuad(125, 0); //top right
        CreateRandQuad(62.5f, 0); //top middle
        CreateRandQuad(0, 62.5f); //middle left
        CreateRandQuad(62.5f, 62.5f); //middle middle
        CreateRandQuad(125, 62.5f); //middle right
        CreateRandQuad(0, 125); //bottom left
        CreateRandQuad(125, 125); //bottom right
        CreateRandQuad(62.5f, 125); //bottom middle

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
