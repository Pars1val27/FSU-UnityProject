using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
//Shaley

public class Arena2Script : MonoBehaviour
{
    [SerializeField] GameObject[] QuadPresets;
    [SerializeField] GameObject[] SpawnPos;

    int lastDir;
    int lastPreset;
    int lastEnemy;

    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        lastDir = 0;
        lastPreset = -1;
        RandArena();
        gameManager.instance.surface.BuildNavMesh();
        RandEnemies();
    }

    void Update()
    {
        
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

    void RandEnemies()
    {

        Instantiate(EnemyManager.instance.enemies[0], SpawnPos[0].transform);
        for (int i = 1; i < SpawnPos.Length; ++i)
        {
            Instantiate(RandEnemy(), SpawnPos[i].transform);
        }
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

    GameObject RandEnemy()
    {
        int enemy = UnityEngine.Random.Range(1, EnemyManager.instance.enemies.Length);
        if(lastEnemy == enemy)
        {
            enemy = UnityEngine.Random.Range(1, EnemyManager.instance.enemies.Length);
        }
        lastEnemy = enemy;
        return EnemyManager.instance.enemies[enemy];
    }
}
