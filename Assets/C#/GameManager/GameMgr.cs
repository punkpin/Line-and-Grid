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
    [Serializable]
    class passAndRouteRecord
    {
        [SerializeField] public MyList<bool>[] passRecord;

        [SerializeField] public MyList<int>[] routeRecordList;
        public List<List<int>> GetIntList()
        {
            List<List<int>> list = new List<List<int>>();
            for (int i = 0; i < routeRecordList.Length; i++)
            {
                list.Add(routeRecordList[i].data);
            }
            return list;
        }
        public List<List<bool>> GetBoolList()
        {
            List<List<bool>> list = new List<List<bool>>();
            for (int i = 0; i < passRecord.Length; i++)
            {
                list.Add(passRecord[i].data);
            }
            return list;
        }
    }
    [Serializable]
    public class MyList<T> 
    {
        public List<T> data;

        public MyList(List<T> data)
        {
            this.data = data;
        }

        public MyList()
        {
            this.data = new List<T>();
        }
    }
    
    public static void SaveRecord()
    {
        passAndRouteRecord pr=new passAndRouteRecord();
        pr.passRecord =new MyList<bool>[CustomsPass.passRecord.Count];
        for (int i = 0; i < CustomsPass.passRecord.Count; i++)
        {
            pr.passRecord[i]=new MyList<bool>(CustomsPass.passRecord[i]);
        }
        pr.routeRecordList =new MyList<int>[CustomsPass.routeRecord.Count];
        for (int i = 0; i < CustomsPass.routeRecord.Count; i++)
        {
            pr.routeRecordList[i]=new MyList<int>(CustomsPass.routeRecord[i]);
        }
        
        string dataJson=JsonUtility.ToJson(pr);
        string path=Path.Combine(Application.persistentDataPath,"passAndRouteRecord.json");
        using(StreamWriter sw=new StreamWriter(path)){
            sw.Write(dataJson);
        }
    }
    public static List<List<bool>> LoadPassRecord()
    {
        string dataJson;
        using(StreamReader sr=new StreamReader(Path.Combine(Application.persistentDataPath,$"passAndRouteRecord.json"))){
            dataJson=sr.ReadToEnd();
        }

        return JsonUtility.FromJson<passAndRouteRecord>(dataJson).GetBoolList();
    }

    public static List<List<int>> LoadRouteRecord()
    {
        string dataJson;
        using(StreamReader sr=new StreamReader(Path.Combine(Application.persistentDataPath,$"passAndRouteRecord.json"))){
            dataJson=sr.ReadToEnd();
        }
        Debug.Log(JsonUtility.FromJson<passAndRouteRecord>(dataJson).GetIntList().Count);
        return JsonUtility.FromJson<passAndRouteRecord>(dataJson).GetIntList();
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        for (int c = 2; c <= 4; c++)
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath,$"gameLevelDataChapter{c}.json")))
            {
                SaveData(c,-1);
            }
        }
        if(!File.Exists(Path.Combine(Application.persistentDataPath,"gameLevelDataChapter1.json")))
            SaveData(1,0);

        if (!File.Exists(Path.Combine(Application.persistentDataPath, "passAndRouteRecord.json")))
        {
            passAndRouteRecord pr=new passAndRouteRecord();
            pr.passRecord =new MyList<bool>[]
            {
                new MyList<bool>(new List<bool>(){false,false,false,false,false,false,false,false,false,false }),
                new MyList<bool>(new List<bool>(){false,false,false,false,false,false,false,false,false,false }),
                new MyList<bool>(new List<bool>(){false,false,false,false,false,false,false,false,false,false }),
                new MyList<bool>(new List<bool>(){false,false,false,false,false,false,false,false,false,false })
            };
            pr.routeRecordList = new MyList<int>[40];
            Debug.Log(pr);
            string dataJson=JsonUtility.ToJson(pr);
            Debug.Log(dataJson);
            string path=Path.Combine(Application.persistentDataPath,"passAndRouteRecord.json");
            using(StreamWriter sw=new StreamWriter(path)){
                sw.Write(dataJson);
            }
        }
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
