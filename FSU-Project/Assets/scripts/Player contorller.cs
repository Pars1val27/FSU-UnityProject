using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class Playercontorller : MonoBehaviour, IDamage
{
    // Start is called before the first frame update
    [SerializeField] CharacterController controller;

    [SerializeField] int HP;
    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpMax;
    [SerializeField] int jumpsSpeed;
    [SerializeField] int gravity;
   



    int HPOrig;
    int jumpCount;

    Vector3 moveDir;
    Vector3 playerVel;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        HPOrig = HP;
        UpdateUIElements();
    }

    void Update()
    {

        Movement();
        Sprint();
        

    }
    void Movement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
            playerVel = Vector3.zero;
        }

        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDir * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            playerVel.y = jumpsSpeed;
        }


        playerVel.y -= gravity * Time.deltaTime;
        controller.Move(playerVel * Time.deltaTime);
    }
    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            speed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
        }
    }

   
    public void TakeDamage(int ammount)
    {
        HP -= ammount;
        UpdateUIElements();
        if (HP <= 0)
        {
            
        }
    }

    void UpdateUIElements()
    {
        
    }


}

