using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject menuMain;
    [SerializeField] GameObject menuBossWin;
    [SerializeField] GameObject inerface;
    [SerializeField] public GameObject bossHealth;
    public PlayerStatUpgrade playerStatUp;

    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] public TMP_Text ammoMax;
    [SerializeField] public TMP_Text ammoCur;

    public Image playerHPBar;
    public Image DashCoolDownFill;
    public Image bossHealthBar;
   



    public bool gamePause;
    public float DashCDRemaining;
    public float dashingTime;

    int enemyCount;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        //StartMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStatUp = null)
            playerStatUp = FindObjectOfType<PlayerStatUpgrade>();
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
            
            statePause();
            playerStatUp.GenerateRandomUpgrades();
            menuActive = menuWin;
            menuActive.SetActive(gamePause);
            playerStatUp.GenerateRandomUpgrades();
            
        }

    }

    public void onLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(true);
    }
    public void StartMenu()
    {
        menuActive = menuMain;
        stateUnpause();   
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
            DashCoolDownFill.fillAmount = DashCDRemaining / PlayerController.playerInstance.playerClass.dashCD;
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
}
