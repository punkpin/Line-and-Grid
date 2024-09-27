using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridGame : MonoBehaviour
{
    private GridGame() { }
    private static GridGame instance;
    public static GridGame Instance //����
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

    public List<GridItemInfo> gridItems = new List<GridItemInfo>(); //��ͼ�����Ԫ��
    public GridItemInfo[,] gridItemInfos = new GridItemInfo[6,6];   //��ͼ
    public GameObject gridItemPrefab;                               //��ͼ����

    public List<GridItemInfo> examItems = new List<GridItemInfo>(); //inspect GridItemInfo element


    private void Start() 
    {
        for (int i = 0; i < 6; i++)//����ͼԪ�طŵ���ͼ��ȥ
        {
            for(int j = 0; j < 6; j++)
            {   
                gridItemInfos[i, j] = gridItems[i * 6 + j];
                gridItemInfos[i,j].index_i = i;
                gridItemInfos[i,j].index_j = j;

                if (gridItems[i * 6 + j].isDigit || gridItems[i * 6 + j].isSquare || gridItems[i * 6 + j].isCirle)
                {
                    examItems.Add(gridItems[i * 6 + j]);
                }

            }
        }
        LoadGrid();
    }

    public void LoadGrid() 
    {
        for (int i = 0; i < 6; i++)//����ͼԪ�طŵ���ͼ��ȥ
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject Item = Resources.Load<GameObject>("Prefab/GridItem");  // ��ȡ��ͼ������������
                GameObject gameObject = Instantiate(Item, transform.position, transform.rotation, transform);  //ʵ����
                gameObject.name = $"({i},{j})";
                GridItem Info = gameObject.GetComponent<GridItem>();
                Info.itemInfo = gridItemInfos[i,j];
                gameObject.transform.SetParent(gridItemPrefab.transform, false); //��ʵ������������ΪgridItemPrefab��������

            }
        }  
    }
}
