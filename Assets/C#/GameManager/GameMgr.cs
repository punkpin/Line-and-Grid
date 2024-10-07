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
    public static void SaveData(int x){
        string dataJosn=JsonUtility.ToJson(new Data(x));
        Debug.Log("当前关卡进度" + dataJosn);
        string path=Path.Combine(Application.dataPath,"Data/gameLevelData.json");
        using(StreamWriter sw=new StreamWriter(path)){
            sw.Write(dataJosn);
        }
        Debug.Log(path);
    }
    public static Data LoadData(){
        string dataJosn;
        using(StreamReader sr=new StreamReader(Path.Combine(Application.dataPath,"Data/gameLevelData.json"))){
            dataJosn=sr.ReadToEnd();
        }
        return JsonUtility.FromJson<Data>(dataJosn);
    }
}
