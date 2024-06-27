using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArena1 : MonoBehaviour
{
    [SerializeField] GameObject[] QuadPresets;
    //make direction same as portal direction

    int lastDir;
    int lastPreset;
    //public static bool isPlayerSpawned;

    public void StartArena()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        lastDir = -1;
        lastPreset = -1;
        RandArena();
        gameManager.instance.surface.BuildNavMesh();
    }

    void RandArena()
    {
        CreateRandQuad(62, 62); //only one for now
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
        if (lastPreset == preset)
        {
            preset = UnityEngine.Random.Range(0, numPresets);
        }
        lastPreset = preset;
        return QuadPresets[preset];
    }
    int RandDir()
    {
        int dir = UnityEngine.Random.Range(-1, 3);
        if (lastDir == dir)
        {
            dir = UnityEngine.Random.Range(-1, 3);
        }
        lastDir = dir;
        return dir * 90;
    }
}
