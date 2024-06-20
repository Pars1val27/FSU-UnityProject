using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    

    [SerializeField] CharacterController controller;
    //[SerializeField] PlayerClass playerClass;
    [SerializeField] GameObject classWeapon;
    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int dashMod;
    [SerializeField] int jumpMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int climbSpeed;
    [SerializeField] int gravity;
    [SerializeField] int PlayerHP;
    [SerializeField] GameObject muzzleFlash;

    [SerializeField] int shootDmg;
    [SerializeField] int shootRate;
    [SerializeField] int shootDist;

    [SerializeField] Transform climbPos;

    [SerializeField] public float dashCD;

    int jumpCount;
    int origSpeed;
    int origHP;
    int origGravity;

    bool isShooting;
    bool isClimbing;

    public bool isSprinting;
    public bool isDashing;

    public float dashDuration = 0.2f;

    Vector3 moveDirection;
    Vector3 playerVelocity;

    
    // Start is called before the first frame update
    void Start()
    {
        origSpeed = speed;
        origHP = PlayerHP;
        origGravity = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprint();
        wallClimb();
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(shoot());
        }
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
        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            speed *= sprintMod;
        }else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
            isSprinting = false;
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;
        StartCoroutine(flashMuzzle());
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position + new Vector3(0,0,0), Camera.main.transform.forward, out hit, shootDist))
        {

            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if(hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(shootDmg);
            }
        }
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    IEnumerator flashMuzzle()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
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

    void wallClimb()
    {
        isClimbing = false;

        Debug.DrawRay(climbPos.position + new Vector3(0, 0, 0), Camera.main.transform.forward, Color.red);
        

        RaycastHit hit;
        if(Physics.Raycast(climbPos.position + new Vector3 (0,0,0), Camera.main.transform.forward, out hit , 2))
        {
            if(hit.collider.CompareTag("Climbable"))
            {
                Debug.Log(hit.collider.name);

                isClimbing = true;
                playerVelocity.y = climbSpeed;
                gravity = 0;

            }

            isClimbing = false;
            gravity = origGravity;

        }
    }

}
