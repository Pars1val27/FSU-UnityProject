using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        int preset = rand.Next(1, 4);
        if (preset == 1)
        {
            this.gameObject.transform.position += new Vector3(0, 10, 0);
        }
        else if (preset == 2)
        {
            this.gameObject.transform.position += new Vector3(0, 20, 0);
        }
        else { }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
