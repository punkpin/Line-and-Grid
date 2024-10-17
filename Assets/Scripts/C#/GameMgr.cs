using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public struct Data{
        public int currentLevel;
        public Data(int level){
            currentLevel = level;
        }
}
public class GameMgr : MonoBehaviour
{
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
        for (int c = 1; c <= 3; c++)
        {
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath,$"gameLevelDataChapter{c}.json")))
            {
                SaveData(c,0);
            }
        }
        SaveData(1,1);
    }
}
