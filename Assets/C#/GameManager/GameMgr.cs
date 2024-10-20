using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct Data{
        public int currentLevel;
        public Data(int level){
            currentLevel = level;
        }
}
public class GameMgr : MonoBehaviour
{
    public static int CurrentLevel = 0;
    public static void SaveData(int c,int x){
        string dataJson=JsonUtility.ToJson(new Data(x));
        string path=Path.Combine(Application.persistentDataPath,$"gameLevelDataChapter{c}.json");
        using(StreamWriter sw=new StreamWriter(path)){
            sw.Write(dataJson);
        }
    }
    public static Data LoadData(int c){
        string dataJson;
        using(StreamReader sr=new StreamReader(Path.Combine(Application.persistentDataPath,$"gameLevelDataChapter{c}.json"))){
            dataJson=sr.ReadToEnd();
        }
        return JsonUtility.FromJson<Data>(dataJson);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        for (int c = 2; c <= 4; c++)
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath,$"gameLevelDataChapter{c}.json")))
            {
                SaveData(c,0);
            }
        }
        if(!File.Exists(Path.Combine(Application.persistentDataPath,"gameLevelDataChapter1.json")))
            SaveData(1,1);
    }
    static string LastUnclearSceneName;

    public static void LoadScene(string name)
    {
        if (name.Equals(""))
        {
            AsyncOperation temp=SceneManager.LoadSceneAsync(LastUnclearSceneName);
            while (temp.isDone)
            {
                GridGame.Instance.nowPass = CurrentLevel;
            }
        }
        else
        {
            if (name.Equals("Select"))
            {
                CurrentLevel = 1;
            }
            SceneManager.LoadScene(name);
        }
    }

    public static void LoadSceneAndBackup(string name)
    {
        LastUnclearSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }
}
