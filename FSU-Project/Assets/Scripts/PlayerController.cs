using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;

    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int dashMod;
    [SerializeField] int jumpMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravtiy;

    [SerializeField] float dashCD;

    int jumpCount;

    bool isDashing;

    float dashDuration = 0.2f;

    Vector3 moveDirection;
    Vector3 playerVelocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprint();
        if(Input.GetButton("Dash") && !isDashing)   
        {
            StartCoroutine(Dash());
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
            playerVelocity = Vector3.zero;
        }

        moveDirection = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDirection * speed *  Time.deltaTime);
        
        if(Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            if (jumpCount == 0)
            {
                jumpCount++;
                playerVelocity.y = jumpSpeed;
            }
            //jump modifier to make player go higher on second jump
            else if (jumpCount == 1)
            {
                jumpCount++;
                playerVelocity.y = (jumpSpeed * 1.5f);
            }
        }
        playerVelocity.y -= gravtiy * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            speed *= sprintMod;
        }else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;


        speed *= dashMod;
        StartCoroutine(DashDuration());

        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(dashDuration);
        speed /= dashMod;
    }

}
