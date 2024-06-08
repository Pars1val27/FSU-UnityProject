using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaController : MonoBehaviour
{

    [SerializeField] int sens;
    [SerializeField] int lockVermin, lockVermax;
    [SerializeField] bool invertY;

    float rotX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;

        if (invertY)
          rotX += mouseY; 
         else 
            rotX -= mouseY;
        

       

        //Clamp the rotX on the X axis
        rotX = Mathf.Clamp(rotX, lockVermin, lockVermax);

        //rotate the camera on the X axis
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        //rotate the player 
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
