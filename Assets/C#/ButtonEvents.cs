using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameMgr.LoadScene(sceneName);
    }

    public void FullScreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else
        {
            Screen.fullScreen = true;
        }
    }
    public static void LoadSceneAndBackup(string name)
    {
        GameMgr.LoadSceneAndBackup(name);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        GameMgr.LoadSceneAndBackup("Start");
    }
}
