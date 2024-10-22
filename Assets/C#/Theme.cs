using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Theme : MonoBehaviour
{
    private static Theme instance;
    public static Theme Instance
    {
        get
        {
            if(instance == null) instance = FindObjectOfType<Theme>();
            return instance;
        }
    }

    private float startTime;
    public float waitingTime;
    public float themeAlpha; //当前透明度
    public float downAlpha;  //每帧下降的透明度
    public GameObject ThemePrefab; //主题的界面
    public List<Image> images;
    public List<Text> texts;
    void Start()
    {
        if (GameMgr.LoadData(GridGame.Instance.nowStage).currentLevel != 0)
        {
            ThemePrefab.SetActive(false);
            this.enabled = false;
            return;
        }
        startTime = Time.time;
        if (themeAlpha > 0f)
        {
            ThemePrefab.SetActive(true); //打开主题展示
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime + waitingTime > Time.time) return;
        if (themeAlpha > 0f)
        {
            themeAlpha -= downAlpha;  //逐渐变透明
            for(int i = 0; i < images.Count; i++)
            {
                Color newcolor = images[i].color;
                newcolor.a = themeAlpha;
                images[i].color = newcolor;
            }

            for (int i = 0; i < texts.Count; i++)
            {
                Color newcolor = texts[i].color;
                newcolor.a = themeAlpha;
                texts[i].color = newcolor;
            }
        }
        else
        {
            ThemePrefab.SetActive(false); //关闭主题展示
            GameMgr.SaveData(GridGame.Instance.nowStage, 1);
            themeAlpha = 0f;
        }
    }

    public void newAlpha() //改变Alpha来显示主题
    {
        themeAlpha = 1; 
    }
}
