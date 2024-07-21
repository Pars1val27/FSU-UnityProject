using System.Collections;
using System.Collections.Generic;
using AbilitySystem;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    // all the the applyed edits and commented out code are handled in playerClass.cs 
    // this shouldn't change any of the prestablished funcionality of the code only changing the veriable location

    public static PlayerController playerInstance;
    
    public CharacterController controller;
    public AbilityHandler abilityHandler;


    [SerializeField] AudioSource aud;
    [SerializeField] Transform gunPos;
    [SerializeField] Transform swordPos;
    [SerializeField] Transform climbPos;

    [SerializeField] GameObject deathCam;

    [Header("Class Weapons")]
    [SerializeField] public GameObject gun;
    [SerializeField] public GameObject sword;

    [Header("Attributes")]
    [Range(1, 150)]
    [SerializeField] public int origHP;
    public int playerHP;
    [Range(1, 50)]
    [SerializeField] public int damage;
    [Range(0, 2)]
    [SerializeField] public float attackSpeed;
    [Range(1f, 1000f)]
    [SerializeField] public float shootDist;
    [Range(1, 20)]
    [SerializeField] public int speed;
    public int baseSpeed;
    [Range(1, 5)]
    [SerializeField] public int sprintMod;
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

    [Header("Stamina")]
    public float stamina;
    [Range(50,150)]
    [SerializeField] public float maxStamina;
    [SerializeField] public float jumpCost;
    public bool staminaFull;
    [Range(0,20)]
    [SerializeField] public float staminaDrain;
    [Range(0, 20)]
    [SerializeField] public float staminaRegen;

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
    float origFOV;
    public float currFOV;

    int jumpCount;

    bool isPlayingSteps;
    bool isPlayingHurt;

    public bool isSprinting;
    public bool isClimbing;
    public bool isDashing;
    public bool isCoolDown;
    public bool isBlocking;
    public bool isCrouching;

    public float dashDuration = 0.2f;
    [SerializeField] public float interactDist;

    Vector3 moveDirection;
    Vector3 playerVelocity;


    public GameObject classWeaponInstance;
   
    public GunScript gunScript;
    public SwordScript swordScript;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = this;
        origFOV = FOV;
        playerHP = origHP;
        isCoolDown = false;
        deathCam.SetActive(false);
        abilityHandler = GetComponent<AbilityHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerUI();   
        Movement();
        Sprint();
        Crouch();
        wallClimb();
        EquipClassWeapon();

        if (Input.GetButtonDown("Dash") && !isDashing)
        {
            StartCoroutine(Dash());
        }

        if (isSprinting && !UIManager.instance.gamePause)
        {
            staminaFull = false;
            stamina -= staminaDrain * Time.deltaTime;
        }

        if (isBlocking)
        {
            staminaFull = false;
            stamina -= staminaDrain * Time.deltaTime;
        }

        if (!isSprinting && staminaFull == false && !UIManager.instance.gamePause && !isBlocking)
        {
            if(stamina <= maxStamina - 0.01)
            {
                stamina += staminaRegen + Time.deltaTime;

                if(stamina >= maxStamina)
                {
                    staminaFull = true;
                }
            }
        }

        RaycastHit interactHit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out interactHit, interactDist))
        {

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

        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax && (stamina >= (maxStamina * jumpCost / maxStamina)))
        {
            if (jumpCount == 0)
            {
                stamina -= jumpCost;
                staminaFull = false;
                jumpCount++;
                playerVelocity.y = jumpSpeed;
                aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
            }
            //jump modifier to make player go higher on second jump
            else if (jumpCount == 1)
            {
                stamina -= jumpCost * 1.5f;
                staminaFull = false;
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

        if (Input.GetButtonDown("Sprint") && stamina >= 5)
        {
            isSprinting = true;
            speed = speed * sprintMod;
            Camera.main.fieldOfView = Mathf.Lerp(FOVSprintMod, origFOV, 0.25f);
        }
        if (Input.GetButtonUp("Sprint") && isSprinting)
        {
            speed = baseSpeed;
            isSprinting = false;
            Camera.main.fieldOfView = Mathf.Lerp(origFOV, FOVSprintMod, 0.25f);
        }
        if(stamina <= 0 && isSprinting)
        {
            speed = baseSpeed;
            isSprinting = false;
            Camera.main.fieldOfView = Mathf.Lerp(origFOV, FOVSprintMod, 0.25f);
            
        }
    }
    void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            speed /= 2;
            playerInstance.transform.localScale = playerInstance.transform.localScale + new Vector3(-1, -1, -1);


        }
        else if (Input.GetButtonUp("Crouch"))
        {
            speed *= 2;
            isCrouching = false;
            playerInstance.transform.localScale = playerInstance.transform.localScale + new Vector3(1, 1, 1);
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
    
    public void TakeDamage(int dmg)
    {
        if (!isBlocking)
        {
            playerHP -= dmg;
            UpdatePlayerUI();
            StartCoroutine(UIManager.instance.FlashDamage());
            if (abilityHandler != null && abilityHandler.HasAbility("HPRecoveryAbility"))
            {                  
                abilityHandler.EnableHPRecovery(1, 5f);
            }

            if (!isPlayingHurt)
            {
                StartCoroutine(isHurtAud());
            }

            if (playerHP <= 0)
            {
                deathCam.SetActive(true);
                StartCoroutine(UIManager.instance.onLose());
            }
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
        UIManager.instance.DashCDRemaining = dashCD;
        speed *= dashMod;
        Camera.main.fieldOfView = Mathf.Lerp(FOVDashMod, currFOV, 0.2f);
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
        Camera.main.fieldOfView = Mathf.Lerp(currFOV, FOVDashMod, 0.2f); ;          

    }
    
    public void UpdatePlayerUI()
    {
        UIManager.instance.playerHPBar.fillAmount = (float)playerHP / origHP;
        UIManager.instance.maxPlayerHP.text = origHP.ToString();
        UIManager.instance.currPlayerMP.text = playerHP.ToString();
        UIManager.instance.staminaBar.fillAmount = stamina / maxStamina;
        if(playerHP <= 0)
        {
            UIManager.instance.onLose();
            Debug.Log("Lose Menu Called");
        }
    }

    void wallClimb()
    {
        isClimbing = false;

        RaycastHit hit;
        if (Physics.Raycast(climbPos.position + new Vector3(0, 0, 0), Camera.main.transform.forward, out hit, 2) && stamina >= 5)
        {
            if (hit.collider.CompareTag("Climbable"))
            {
                isClimbing = true;
                stamina -= staminaDrain * Time.deltaTime;
                staminaFull = false;
                playerVelocity.y = climbSpeed;

            }
        }
    }

/*    public void getGun(GameObject gun)
    {
        if (UIManager.instance.classGunner == true)
        {
            weapons.Add(gun);
        }
    }

    public void getSword(GameObject sword)
    {
        if (UIManager.instance.classMele == true)
        {
            weapons.Add(sword);
        }
    }*/

    void EquipClassWeapon()
    {
        if (UIManager.instance.classGunner == true && classWeaponInstance == null)
        {
            origHP = 20;
            playerHP = origHP; 
            speed = 14;
            baseSpeed = speed;
            attackSpeed = 0.5f;
            classWeaponInstance = Instantiate(gun, gunPos.position, gunPos.rotation, gunPos);
            gunScript = classWeaponInstance.GetComponent<GunScript>();
            NotifyAbilityHandler();
        }

        if (UIManager.instance.classMele == true && classWeaponInstance == null)
        {
            origHP = 30;
            playerHP = origHP;
            speed = 20;
            baseSpeed = speed;
            attackSpeed = .75f;
            classWeaponInstance = Instantiate(sword, swordPos.position, swordPos.rotation, swordPos);
            swordScript = classWeaponInstance.GetComponent<SwordScript>();
            AbilityManager.Instance.RemoveSpawnableAbility("increaseMaxAmmo");
            NotifyAbilityHandler();
        }
    }
    private void NotifyAbilityHandler()
    {
       // AbilityHandler abilityHandler = GetComponent<AbilityHandler>();
        if (abilityHandler != null)
        {
            if (gunScript != null)
            {
                abilityHandler.gunScript = gunScript;
            }
            if (swordScript != null)
            {
                abilityHandler.swordScript = swordScript;
                
            }
        }
    }
}
