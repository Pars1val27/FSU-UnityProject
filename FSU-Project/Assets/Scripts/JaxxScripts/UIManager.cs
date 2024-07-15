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
    [SerializeField] public GameObject menuPrev;
    [SerializeField] public GameObject menuMain;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuTimeLose;
    [SerializeField] GameObject menuHPLose;
    [SerializeField] GameObject menuSelect;
    [SerializeField] GameObject menuBossWin;
    [SerializeField] GameObject inerface;
    [SerializeField] GameObject menuSettings;
    [SerializeField] GameObject menuAbilities;
    [SerializeField] GameObject menuControls;
    [SerializeField] GameObject menuInventory;
    [SerializeField] GameObject itemMenu;
    [SerializeField] GameObject lowHealthIndi;
    [SerializeField] public GameObject bossHealth;
    [SerializeField] public GameObject loadingScreen;

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
    public bool abilityMenuOpen;

    int enemyCount;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(MainMenu());
        Debug.Log("MainMenu Up");
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
        if (playerHPBar.fillAmount <= playerHPBar.fillAmount * .1) 
        {
            SetMenu(lowHealthIndi);
        }
    }

    public IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(.1f);
        SetMenu(menuMain);
        Debug.Log("Set Menu");
    }
    public void statePause()
    {
        gamePause = !gamePause; 
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Menu Open");
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
        Debug.Log("Menu Close");
    }

    public void UpdateEnemyDisplay(int amount)
    {
        enemyCount += amount;
        enemyCountText.text = enemyCount.ToString("f0");
        
        

    }

    public void onTimeLose()
    {
        SetMenu(menuTimeLose);
    }

    public void onLose()
    {
        SetMenu(menuHPLose);
    }

    public void onWin()
    {
        statePause();
        menuActive = menuBossWin;
        menuActive.SetActive(gamePause);
    }
    public void StartMenu()
    {
        menuActive = menuSelect;
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

    public IEnumerator StartingMenu()
    {
        yield return new WaitForSeconds(0.2f);
        StartMenu();
    }

    public void SetMenu(GameObject menu)
    {
        if (menuActive != null)
        {
            menuActive.SetActive(false);
        }
            menuPrev = menuActive;
            menuActive = menu;
            statePause();
            menuActive.SetActive(true);
    }

    public void SetPrevMenu()
    {
        SetMenu(menuPrev);
    }

    public void MenuOff() 
    {
        if(menuActive != null)
        {
            stateUnpause();
        }
    }

    public void AbilityMenuOn()
    {
        abilityMenuOpen = true;
        menuActive = itemMenu;
        menuActive.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void AbilityMenuOff() 
    {
        abilityMenuOpen = false;
        menuActive.SetActive(false);
        menuActive = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShowAbilityInventory()
    {
        if(menuActive != menuInventory && !gamePause)
        { 
            SetMenu(menuInventory);
        }
        else if(menuActive == menuInventory)
        {
            stateUnpause();
        }
    }

    
}