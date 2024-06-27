using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour

    
{ 
    //Michael
    public static EnemyManager instance;

    public GameObject player;

    public GameObject[] bosses;

    public GameObject[] enemies;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        FindPlayer();
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
