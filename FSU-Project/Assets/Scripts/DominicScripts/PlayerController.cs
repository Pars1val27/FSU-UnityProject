using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    // all the the applyed edits and commented out code are handled in playerClass.cs 
    // this shouldn't change any of the prestablished funcionality of the code only changing the veriable location

    public static PlayerController playerInstance;
    
    public CharacterController controller;
    public PlayerStatUpgrade playerStatUp;
    [SerializeField] public GameObject classWeapon;

    [SerializeField] AudioSource aud;
    [SerializeField] Transform weaponPos;
    [SerializeField] Transform climbPos;

    [Header("Attributes")]
    [Range(1, 100)]
    [SerializeField] public int origHP;
    public int playerHP;
    [Range(1, 50)]
    [SerializeField] public int damage;
    [Range(1, 10)]
    [SerializeField] public float attackSpeed;
    [Range(1f, 1000f)]
    [SerializeField] public float shootDist;
    [Range(1, 20)]
    [SerializeField] int speed;
    [Range(1, 5)]
    [SerializeField] int sprintMod;
    [Range(1, 3)]
    [SerializeField] public int jumpMax;
    [Range(1, 20)]
    [SerializeField] public int jumpSpeed;
    [Range(1, 10)]
    [SerializeField] public float dashCD;
    [Range(1, 10)]
    [SerializeField] public int dashMod;
    [Range(1,20)]
    [SerializeField] int climbSpeed;
    [Range(1,50)]
    [SerializeField] int gravity;

    [Header("Audio")]
    [SerializeField] AudioClip[] audSteps;
    [SerializeField] float audStepsVol;
    [SerializeField] AudioClip[] audJump;
    [SerializeField] float audJumpVol;
    [SerializeField] AudioClip[] audHurt;
    [SerializeField] float audHurtVol;
    [SerializeField] AudioClip[] audJumpBoost;
    [SerializeField] float audJumpBoostVol;
    [SerializeField] AudioClip[] audDash;
    [SerializeField] float audDashVol;

    [Header("Field of View")]
    [SerializeField] float FOV;
    [SerializeField] float FOVSprintMod;
    [SerializeField] float FOVDashMod;

    int jumpCount;

    bool isPlayingSteps;
    bool isPlayingHurt;

    public bool isSprinting;
    public bool isClimbing;
    public bool isDashing;
    public bool isCoolDown;

    float origFOV;
    public float currFOV;

    public float dashDuration = 0.2f;

    Vector3 moveDirection;
    Vector3 playerVelocity;


    GameObject classWeaponInstance;
    GunScript gunScript;
    SwordScript swordScript;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = this;
        origFOV = FOV;
        playerHP = origHP;
        EquipClassWeapon();
        playerStatUp = FindObjectOfType<PlayerStatUpgrade>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sprint();
        wallClimb();
        
        //handled in gun.cs
        /*if (Input.GetButtonDown("Fire1"))
        {
            //StartCoroutine(shoot());
            
            
        }
        */
        if (Input.GetButton("Dash") && !isDashing)
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
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            if (jumpCount == 0)
            {
                jumpCount++;
                playerVelocity.y = jumpSpeed;
                aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
            }
            //jump modifier to make player go higher on second jump
            else if (jumpCount == 1)
            {
                jumpCount++;
                playerVelocity.y = (jumpSpeed * 1.5f);
                aud.PlayOneShot(audJumpBoost[Random.Range(0, audJumpBoost.Length)], audJumpBoostVol);
            }
        }
        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (controller.isGrounded && moveDirection.magnitude > .03f && !isPlayingSteps)
            StartCoroutine(playSteps());

    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            speed *= sprintMod;
            Camera.main.fieldOfView = Mathf.Lerp(FOVSprintMod, origFOV, 0.5f * Time.deltaTime);
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
            isSprinting = false;
            Camera.main.fieldOfView = Mathf.Lerp(origFOV, FOVSprintMod, 0.5f * Time.deltaTime);
        }
    }

    IEnumerator playSteps()
    {
        isPlayingSteps = true;
        aud.PlayOneShot(audSteps[Random.Range(0, audSteps.Length)], audStepsVol);
        if (!isSprinting)
            yield return new WaitForSeconds(0.3f);
        else
            yield return new WaitForSeconds(0.2f);
        isPlayingSteps = false;
    }

    //moveed to Gun.cs
   /* IEnumerator shoot()
    {
        isShooting = true;
        StartCoroutine(flashMuzzle());
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position + new Vector3(0,0,0), Camera.main.transform.forward, out hit, shootDist))
        {
            Debug.Log(hit);

            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if(hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(shootDmg);
            }
        }
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }*/

    /*IEnumerator flashMuzzle()
    {
        muzzleFlash.SetActive(true);
        aud.PlayOneShot(audGun[Random.Range(0, audGun.Length)], audGunVol);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }*/
   

    public void TakeDamage(int dmg)
    {
        playerHP -= dmg;
        UpdatePlayerUI();

        if (!isPlayingHurt)
        {
            StartCoroutine(isHurtAud());
        }

        if (playerHP <= 0)
        {
            UIManager.instance.onLose();
        }
    }

    IEnumerator isHurtAud()
    {
        isPlayingHurt = true;
        aud.PlayOneShot(audHurt[Random.Range(0,audHurt.Length)], audHurtVol);
        yield return new WaitForSeconds(0.15f);
        isPlayingHurt = false;
    }

    IEnumerator Dash()
    {
        currFOV = Camera.main.fieldOfView;
        aud.PlayOneShot(audDash[Random.Range(0, audDash.Length)], audDashVol);
        isDashing = true;
        UIManager.instance.DashCoolDownFill.fillAmount = 0;
        UIManager.instance.DashCDRemaining =dashCD;
        speed *= dashMod;
        Camera.main.fieldOfView = Mathf.Lerp(FOVDashMod, currFOV, 0.05f * Time.deltaTime);

        StartCoroutine(DashDuration());
        isCoolDown = true;
        yield return new WaitForSeconds(dashCD);
        isCoolDown = false;
        isDashing = false;
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(dashDuration);

        speed /= dashMod;
        Camera.main.fieldOfView = Mathf.Lerp(currFOV, FOVDashMod, 0.05f * Time.deltaTime); ;          

    }

    void UpdatePlayerUI()
    {
        UIManager.instance.playerHPBar.fillAmount = (float)playerHP / origHP;
    }

    void wallClimb()
    {
        isClimbing = false;

        Debug.DrawRay(climbPos.position + new Vector3(0, 0, 0), Camera.main.transform.forward, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(climbPos.position + new Vector3(0, 0, 0), Camera.main.transform.forward, out hit, 2))
        {
            if (hit.collider.CompareTag("Climbable"))
            {
                Debug.Log(hit.collider.name);

                isClimbing = true;
                playerVelocity.y = climbSpeed;

            }
        }
    }

    void EquipClassWeapon()
    {
        if (UIManager.instance.classGunner == true)
        {
            classWeaponInstance = Instantiate(classWeapon, weaponPos.position, weaponPos.rotation, weaponPos);
            gunScript = classWeaponInstance.GetComponent<GunScript>();
            playerHP = 20;
            speed = 14;
            attackSpeed = 0.25f;
        }

        if (UIManager.instance.classMele == true)
        {
            classWeaponInstance = Instantiate(classWeapon, weaponPos.position, weaponPos.rotation, weaponPos);
            swordScript = classWeaponInstance.GetComponent<SwordScript>();
            playerHP = 30;
            speed = 20;
            attackSpeed = 1;
            shootDist = 2;
        }
    }

} 
