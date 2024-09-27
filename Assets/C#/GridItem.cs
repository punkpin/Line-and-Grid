using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class GridItemInfo
{
    public int value;          //格子数字
    internal int index_i;        // i坐标
    internal int index_j;        // j坐标
    public bool isDigit;       //是否为数字
    public bool isSquare;      //是否为方形
    public bool isCirle;       //是否为圆形
}

public class GridItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GridItemInfo itemInfo = new GridItemInfo();
    public Image iconDigit;          //获取图标
    public Image iconShape;          //获取图标
    public Image selected;      //是否被选


    public void OnPointerEnter(PointerEventData eventData)//鼠标进入该UI时
    {
        selected.transform.gameObject.SetActive(true);
        if (itemInfo.isDigit) //如果该节点是数字
        {
            int[] dx = {0,0,1,-1,1,1,-1,-1};    //8个方向
            int[] dy = {1,-1,0,0,-1,1,1,-1};
            for(int k = 0; k < 8; k++)
            {
                int nx = itemInfo.index_i + dx[k];
                int ny = itemInfo.index_j + dy[k];
                if(nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;//超出边界
                GameObject aroundItem = GridGame.Instance.gridItemPrefab.transform.GetChild(nx * 6 + ny).gameObject;//获取周围的Gameobject
                GridItem aroundGridItem = aroundItem.GetComponent<GridItem>();//获取组件
                aroundGridItem.selected.transform.gameObject.SetActive(true);//将被选定改为true
            }
        }
        
        
    }

    public void OnPointerExit(PointerEventData eventData)//鼠标离开该UI时
    {
        selected.transform.gameObject.SetActive(false);
        if (itemInfo.isDigit)
        {
            int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };
            int[] dy = { 1, -1, 0, 0, -1, 1, 1, -1 };
            for (int k = 0; k < 8; k++)
            {
                int nx = itemInfo.index_i + dx[k];
                int ny = itemInfo.index_j + dy[k];
                if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;
                GameObject aroundItem = GridGame.Instance.gridItemPrefab.transform.GetChild(nx * 6 + ny).gameObject;
                GridItem aroundGridItem = aroundItem.GetComponent<GridItem>();
                aroundGridItem.selected.transform.gameObject.SetActive(false);//将被选定改为false
            }
        }
    }

    public void Start()
    {
        if (itemInfo.isDigit)  //如果是数字
        {
            iconDigit.sprite = Resources.Load<Sprite>("Sprite/Num/" + itemInfo.value);
            iconDigit.transform.gameObject.SetActive(true);
        }
        else if (itemInfo.isSquare) //如果是方形
        {
            iconShape.sprite = Resources.Load<Sprite>("Sprite/Green Square");
            iconShape.transform.gameObject.SetActive(true);
        }
        else if (itemInfo.isCirle) //如果是圆形
        {
            iconShape.sprite = Resources.Load<Sprite>("Sprite/Red Circle");
            iconShape.transform.gameObject.SetActive(true);
        }

    }

}
