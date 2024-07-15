using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour

    
{ 
    //Michael
    public static EnemyManager instance;

    public GameObject player;

    public List<GameObject[]> bosses;
    [SerializeField] public GameObject[] floor1Bosses;
    [SerializeField] public GameObject[] floor2Bosses;

    public List<GameObject[]> enemies;
    [SerializeField] public GameObject[] floor1Enemies;
    [SerializeField] public GameObject[] floor2Enemies;

    public List<GameObject[]> enemiesHard;
    [SerializeField] public GameObject[] floor1EnemiesHard;
    [SerializeField] public GameObject[] floor2EnemiesHard;

    public List<GameObject[]> enemiesStationary;
    [SerializeField] public GameObject[] floor1StationaryEnemies;
    [SerializeField] public GameObject[] floor2StationaryEnemies;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        FindPlayer();
        bosses = new List<GameObject[]>();
        enemies = new List<GameObject[]>();
        enemiesHard = new List<GameObject[]>();
        enemiesStationary = new List<GameObject[]>();
        bosses.Add(floor1Bosses);
        bosses.Add(floor2Bosses);
        enemies.Add(floor1Enemies);
        enemies.Add(floor2Enemies);
        enemiesHard.Add(floor1EnemiesHard);
        enemiesHard.Add(floor2EnemiesHard);
        enemiesStationary.Add(floor1StationaryEnemies);
        enemiesStationary.Add(floor2StationaryEnemies);
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}