using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] int sens;
    [SerializeField] int lockVertMin, lockVertMax;
    [SerializeField] bool invertY;

    float rotX;

    // Start is called before the first frame update
    void Start()
    {
        //Lock cursor to play window
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Get mouse X and Y movements
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;

        //invert up and down controls
        if (invertY)
        {
            rotX += mouseY;
        }
        else
        {
            rotX -= mouseY;
        }

        //clamp Y rotation 
        rotX = Mathf.Clamp(rotX, lockVertMin, lockVertMax);
        //rotate cam on X axis
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        //rotate player on Y axis
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
