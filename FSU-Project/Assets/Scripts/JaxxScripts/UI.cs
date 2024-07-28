using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
   public void Resume()
    {
        UIManager.instance.stateUnpause();
    }   
    
    public void Restart()
    {
        Debug.Log("Restart");
        StartCoroutine(UIManager.instance.StartingMenu());
        Debug.Log("Class Select");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.player.transform.position = new Vector3(0, 4, 0);
        //UIManager.instance.bossHealth.SetActive(false);
        UIManager.instance.stateUnpause();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_WEBGL
        Application.OpenURL("https://www.newgrounds.com/portal/view/940367");

#else
        Application.Quit();
#endif
    }

    public void ClassSetMele()
    {
        UIManager.instance.classMele = true;
        UIManager.instance.classGunner = false;
        Resume();
        Debug.Log("Melee Class");
    }

    public void ClassSetGunner()
    {
        UIManager.instance.classMele = false;
        UIManager.instance.classGunner = true;
        Resume();
        Debug.Log("Gunner Class");
    }

    public void TurnOnUI(GameObject ui)
    {
        ui.SetActive(true);
    }

    public void TurnOffUI(GameObject ui)
    {
        ui.SetActive(false);
    }
   
}
