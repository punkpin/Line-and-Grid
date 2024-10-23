using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    public float waitingTime; 
    public GameObject ThemePrefab; //����Ľ���
    void Start()
    {
        if (GameMgr.isThemePlayed[GridGame.Instance.nowStage-1])
        {
            ThemePrefab.SetActive(false);
            this.enabled = false;
            return;
        }
        else
        {
            ThemePrefab.SetActive(true); //������չʾ
        }
    }

    public void CloseTheme()
    {
        DOTween.To(() => ThemePrefab.GetComponent<CanvasGroup>().alpha,
            x => ThemePrefab.GetComponent<CanvasGroup>().alpha = x,
            0,
            waitingTime).onComplete += () =>
        {
            GameMgr.isThemePlayed[GridGame.Instance.nowStage-1] = true;
            ThemePrefab.SetActive(false);
        };
        
    }
}
