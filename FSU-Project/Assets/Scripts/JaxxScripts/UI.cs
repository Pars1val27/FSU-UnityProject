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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //UIManager.instance.bossHealth.SetActive(false);
        UIManager.instance.stateUnpause();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ClassSetMele()
    {
        Resume();
    }

    public void ClassSetGunner()
    {
        Resume();
    }

    public void StartTime()
    {

    }

    public void StopTime()
    {

    }

    public void EditTime(float time)
    {

    }
   
}
