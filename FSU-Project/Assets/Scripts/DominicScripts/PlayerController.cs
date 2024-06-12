using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{

    [SerializeField] CharacterController controller;

    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int dashMod;
    [SerializeField] int jumpMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravtiy;
    [SerializeField] int PlayerHP;
    [SerializeField] int bulletdmg;

    [SerializeField] public float dashCD;

    int jumpCount;
    int origHP;

    public bool isDashing;

    public float dashDuration = 0.2f;

    Vector3 moveDirection;
    Vector3 playerVelocity;

    
    // Start is called before the first frame update
    void Start()
    {
        origHP = PlayerHP;
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

    public void TakeDamage(int dmg)
    {
        PlayerHP -= dmg;
        UpdatePlayerUI();

        if(PlayerHP <= 0)
        {
            UIManager.instance.onLose();
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

    void UpdatePlayerUI()
    {
        UIManager.instance.playerHPBar.fillAmount = (float)PlayerHP / origHP;
    }

}
