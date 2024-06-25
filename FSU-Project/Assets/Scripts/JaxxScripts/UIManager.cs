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

    [SerializeField] TMP_Text enemyCountText;

    public Image playerHPBar;
    public Image DashCoolDownFill;
    static PlayerController playerInstance;


    public bool gamePause;
    public bool crosshairActive;
    public float DashCDRemaining;

    int enemyCount;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
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
        }/*
        if(playerInstance.isDashing)
        {
            DashCDRemaining = playerInstance.dashCD;
            while(DashCDRemaining > 0)
            {
                DashCDRemaining -= DashCDRemaining;
                DashCoolDownFill.fillAmount = DashCDRemaining/ playerInstance.dashCD;
            }
        }*/
    }

    public void statePause()
    {
        gamePause = !gamePause;
        crosshairActive = !crosshairActive; 
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void stateUnpause()
    {
        gamePause = !gamePause;
        crosshairActive = !crosshairActive;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(gamePause);
        menuActive = null;
    }

    public void UpdateEnemyDisplay(int amount)
    {
  
        enemyCount += amount;
        enemyCountText.text = enemyCount.ToString("f0");

        if(enemyCount <= 0)
        {
            statePause();
            menuActive = menuWin;
            menuActive.SetActive(gamePause);
        }
        
    }

    public void onLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(true);
    }
   
}
