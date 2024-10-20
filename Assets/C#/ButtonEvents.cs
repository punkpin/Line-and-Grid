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
    public static void LoadSceneAndBackup(string name)
    {
        GameMgr.LoadSceneAndBackup(name);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
    public void LoadNewSceneAdditive(string name)
    {
        GameMgr.CurrentLevel = GridGame.Instance?GridGame.Instance.nowPass:1;
        GameMgr.LoadSceneAndBackup(name);
    }
    public void CancelQuit()
    {
        GameMgr.LoadSceneAndBackup("Start");
    }
}
