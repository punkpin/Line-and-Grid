using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridGame : MonoBehaviour
{
    private GridGame() { }
    private static GridGame instance;
    public static GridGame Instance //单例
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GridGame>();
            }
            return instance;
        }
    }

    public List<GridItemInfo> gridItems = new List<GridItemInfo>(); //地图里面的元素
    public GridItemInfo[,] gridItemInfos = new GridItemInfo[6,6];   //地图
    public GameObject gridItemPrefab;                               //地图背景


    private void Start() 
    {
        for (int i = 0; i < 6; i++)//将地图元素放到地图中去
        {
            for(int j = 0; j < 6; j++)
            {   
                gridItemInfos[i, j] = gridItems[i * 6 + j];
                gridItemInfos[i,j].index_i = i;
                gridItemInfos[i,j].index_j = j;
                
            }
        }
        LoadGrid();
    }

    public void LoadGrid() 
    {
        for (int i = 0; i < 6; i++)//将地图元素放到地图中去
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject Item = Resources.Load<GameObject>("Prefab/地图网格元素");  // 获取地图背景的子物体
                GameObject gameObject = Instantiate(Item, transform.position, transform.rotation, transform);  //实例化
                gameObject.name = $"({i},{j})";
                GridItem Info = gameObject.GetComponent<GridItem>();
                Info.itemInfo = gridItemInfos[i,j];
                gameObject.transform.SetParent(gridItemPrefab.transform, false); //将实例化的物体作为gridItemPrefab的子物体

            }
        }  
    }
}
