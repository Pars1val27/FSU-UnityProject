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
   
}
