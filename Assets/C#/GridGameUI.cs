using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGameUI : MonoBehaviour
{
    private static GridGameUI instance;
    public static GridGameUI Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GridGameUI>();
            }
            return instance;
        }
    }


    public GameObject gridItemUIPrefab;
    public Text nowPassText;
    public Text tips;

    public void LoadGridUI()
    {
        if (GridGame.Instance.nowPass == 1) tips.gameObject.SetActive(true);
        else tips.gameObject.SetActive(false);

        for (int i = gridItemUIPrefab.transform.childCount - 1; i >= 0; i--)//删除地图子物体
        {
            GameObject.Destroy(gridItemUIPrefab.transform.GetChild(i).gameObject);
        }

        GridItemInfo[,] gridItemInfos = GridGame.Instance.gridItemInfos;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject ItemUI = Resources.Load<GameObject>("Prefab/GridItemUI");
                GameObject gameObjectUI = Instantiate(ItemUI, transform.position, transform.rotation, transform);
                gameObjectUI.name = $"({i},{j})";
                GridItemUI InfoUI = gameObjectUI.GetComponent<GridItemUI>();
                if (gridItemInfos[i, j].isDigit)
                {
                    InfoUI.digit = gridItemInfos[i, j].value;
                    InfoUI.digitalType = gridItemInfos[i, j].digitalType;
                    InfoUI.isDigit = true;
                }
                else InfoUI.isDigit = false;
                gameObjectUI.transform.SetParent(gridItemUIPrefab.transform, false);

            }
        } 

        if(GridGame.Instance.nowStage == 1)
        {
            nowPassText.text = $"P-1-{GridGame.Instance.nowPass}";
        }
        else if(GridGame.Instance.nowStage == 2)
        {
            nowPassText.text = $"N-1-{GridGame.Instance.nowPass}";
        }
        else if (GridGame.Instance.nowStage == 3)
        {
            nowPassText.text = $"P-2-{GridGame.Instance.nowPass}";
        }
        else
        {
            nowPassText.text = $"N-2-{GridGame.Instance.nowPass}";
        }
    }
}
