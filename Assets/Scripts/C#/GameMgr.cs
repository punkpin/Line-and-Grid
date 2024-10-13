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
        string path=Path.Combine(Application.dataPath,$"Data/gameLevelDataChapter{c}.json");
        using(StreamWriter sw=new StreamWriter(path)){
            sw.Write(dataJson);
        }
    }
    public static Data LoadData(){
        string dataJson;
        using(StreamReader sr=new StreamReader(Path.Combine(Application.dataPath,"Data/gameLevelData.json"))){
            dataJson=sr.ReadToEnd();
        }
        return JsonUtility.FromJson<Data>(dataJson);
    }
}
