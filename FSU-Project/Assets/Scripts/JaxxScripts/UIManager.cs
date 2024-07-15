using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("----Menus----")]
    [SerializeField] public GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject menuMain;
    [SerializeField] GameObject menuBossWin;
    [SerializeField] GameObject inerface;
    [SerializeField] GameObject menuSettings;
    [SerializeField] GameObject menuAbilities;
    [SerializeField] GameObject menuControls;
    [SerializeField] GameObject itemMenu;
    [SerializeField] GameObject lowHealthIndi;
    [SerializeField] public GameObject bossHealth;

    [Header("----Text----")]
    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] public TMP_Text ammoMax;
    [SerializeField] public TMP_Text ammoCur;
    [SerializeField] public TMP_Text maxPlayerHP;
    [SerializeField] public TMP_Text currPlayerMP;

    [Header("----Image----")]
    public Image playerHPBar;
    public Image DashCoolDownFill;
    public Image bossHealthBar;


    [Header("----CoolDowns")]
    public float DashCDRemaining;
    public float dashingTime;

    [Header("----Bools----")]
    public bool gamePause;
    public bool classMele;
    public bool classGunner;

    int enemyCount;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        StartCoroutine(StartingMenu());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(gamePause);
            }
            else if(menuActive == menuPause)
            {
                stateUnpause();
            }
        }

        if (PlayerController.playerInstance.isCoolDown)
        {
            DashCD();
        }
        if (playerHPBar.fillAmount == .1f ) 
        {
            SetMenu(lowHealthIndi);
        }
    }

    public void statePause()
    {
        gamePause = !gamePause; 
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void stateUnpause()
    {
        gamePause = !gamePause;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(gamePause);
        inerface.SetActive(true);
        menuActive = null;  
    }

    public void UpdateEnemyDisplay(int amount)
    {
        enemyCount += amount;
        enemyCountText.text = enemyCount.ToString("f0");
        
        if(enemyCount <= 0)
        {
            onWin();
            //PlayerController.playerInstance.playerStatUp.GenerateRandomUpgrades();
            //PlayerController.playerInstance.playerStatUp.GenerateRandomUpgrades();
        }

    }

    public void onLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(true);
    }

    public void onWin()
    {
        statePause();
        menuActive = menuBossWin;
        menuActive.SetActive(gamePause);
    }
    public void StartMenu()
    {
        menuActive = menuMain;
        statePause();   
        menuActive.SetActive(gamePause);
    }

    public void DashCD()
    {
        DashCDRemaining -= Time.deltaTime;
        if(DashCDRemaining <= 0) 
        {
            DashCoolDownFill.fillAmount = 1;
        }
        else
        {
            DashCoolDownFill.fillAmount = DashCDRemaining / PlayerController.playerInstance.dashCD;
        }
    }

    public void StartBoss()
    {
       bossHealth.SetActive(true);
    }
    public void BossWin()
    {
        menuActive = menuBossWin;
        statePause();
        menuActive.SetActive(gamePause);
    }

    IEnumerator StartingMenu()
    {
        yield return new WaitForSeconds(0.2f);
        StartMenu();
    }

    public void SetMenu(GameObject menu)
    {
        menuActive.SetActive(false);
        menuActive = menu;
        menuActive.SetActive(gamePause);
    }

    public void AbilityMenuOn()
    {
        menuActive = itemMenu;
        menuActive.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void AbilityMenuOff() 
    {
        menuActive.SetActive(false);
        menuActive = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}