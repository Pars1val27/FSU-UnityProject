using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    [SerializeField] LineRenderer lineRend;
    [SerializeField] int dmg;
    bool canDmg;
    // Start is called before the first frame update
    void Start()
    {
        canDmg = true;
        Debug.Log("Can damage: " + canDmg);
        lineRend = this.GetComponent<LineRenderer>();
        Vector3[] points = new Vector3[2];
        points[0] = start.transform.position;
        points[1] = end.transform.position;
        lineRend.SetPositions(points);
    }

    void Update()
    {
        if (canDmg)
        {
            RaycastHit hit;
            Vector3 dir = start.transform.position - end.transform.position;
            if (Physics.Linecast(start.transform.position, end.transform.position, out hit))
            {
                Debug.DrawRay(start.transform.position, dir);
                if (hit.collider.CompareTag("Player"))
                {
                    PlayerController.playerInstance.TakeDamage(dmg);
                    canDmg = false;
                    StartCoroutine(WaitForDmg());
                }
            }
        }
    }

    IEnumerator WaitForDmg()
    {
        yield return new WaitForSeconds(1);
        canDmg = true;
    }
}
