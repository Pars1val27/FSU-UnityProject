using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        moveBlockRand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void moveBlockRand()
    {
        System.Random rand = new System.Random();
        int preset = rand.Next(1, 4);
        if (preset == 1)
        {
            this.gameObject.transform.position += new Vector3(0, 25, 0);
        }
        else if (preset == 2)
        {
            this.gameObject.transform.position += new Vector3(0, 50, 0);
        }
        else { }
    }
}
