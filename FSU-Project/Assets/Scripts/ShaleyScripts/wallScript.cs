using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    public GameObject wall;
    public bool hasCollisionWall;
    public bool hasCollisionDoor;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall")){
            hasCollisionWall = true;
            Destroy(other.gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
            hasCollisionDoor = true;
        }
    }
}
