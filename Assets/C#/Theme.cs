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
    public float themeAlpha; //��ǰ͸����
    public float downAlpha;  //ÿ֡�½���͸����
    public GameObject ThemePrefab; //����Ľ���
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
            ThemePrefab.SetActive(true); //������չʾ
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime + waitingTime > Time.time) return;
        if (themeAlpha > 0f)
        {
            themeAlpha -= downAlpha;  //�𽥱�͸��
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
            ThemePrefab.SetActive(false); //�ر�����չʾ
            GameMgr.SaveData(GridGame.Instance.nowStage, 1);
            themeAlpha = 0f;
        }
    }

    public void newAlpha() //�ı�Alpha����ʾ����
    {
        themeAlpha = 1; 
    }
}
