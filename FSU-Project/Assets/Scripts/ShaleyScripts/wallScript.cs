using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    [SerializeField] GameObject door;
    public GameObject[] walls;
    int doorCount;
    int lastChance;
    // Start is called before the first frame update
    void Start()
    {
        GetDoorCount();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (doorCount == 0)
            {
                AddDoor();
                Destroy(other.gameObject);
            }
            else
            {
                int chance = RandChance();
                if (chance == 0)
                {
                    AddDoor();
                }
                else
                {
                    //do nothing
                }
                Destroy(other.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    int RandChance()
    {
        int chance = UnityEngine.Random.Range(0, 1);
        if (lastChance == chance)
        {
            chance = UnityEngine.Random.Range(0, 1);
        }
        lastChance = chance;
        return chance;
    }

    void AddDoor()
    {
        door = Instantiate(door);
        door.transform.position = this.transform.position;
        door.transform.rotation = this.transform.rotation;
        //type other destroy after
        Destroy(this.gameObject);

    }

    void GetDoorCount()
    {
        for (int wallIndex = 0; wallIndex < 4; wallIndex++)
        {
            if (walls[wallIndex].CompareTag("Wall"))
            {
                //do nothing
            }
            else
            {
                //is door
                doorCount++;
            }
        }
    }
}
