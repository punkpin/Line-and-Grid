using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridGame : MonoBehaviour
{
    private float spacing = 0.168f; // 单元格间距
    private Vector3 firstVector3 = new Vector3(-0.42f, 0.419f, -0.3f); //地图的第一个位置
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

    public GridItemInfo[,] gridItemInfos;   //目前的地图
    public GameObject gridItemPrefab;                               //��ͼ����
    public int nowPass = 1; //目前的关卡数
    
    public List<GridItem> gridRoute; //游戏路线
    public GridItemInfo lastItem = new GridItemInfo();  //上一格(用于判断路径是否合法)
    public List<GridItem> gridDigits;
    public bool inCheck;    //是否正在检查
    public bool isAnimPlaying;
    private void Start() 
    {
        LoadGrid();
    }

    public void Update()
    {
        if (!Input.GetMouseButton(0) && inCheck == false) //已经松开了鼠标 并且不在检查时间内
        {
            inCheck = true;
            CheckRoute(); //确认游戏路线
        }
    }

    public void CheckRoute()
    {
        if (gridRoute.Count <= 0)
        {
            inCheck = false;
            return;//如果游戏线路为空就先返回
        }

        int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };//八个方向
        int[] dy = { 1, -1, 0, 0, 1, -1, -1, 1 };
        bool isRight = true; //默认路径正确
        int circleValue = 0,squareValue = 0; //圆和方的数量初始为0
        Debug.Log(gridRoute.Count);
        for (int i = 0; i < gridRoute.Count; i++) 
        {
            if (gridRoute[i].itemInfo.isCircle) circleValue++; //路径是圆
            if (gridRoute[i].itemInfo.isSquare) squareValue++; //路径是方
            for (int k = 0; k < 8; k++)//给周围8个方向数字都+1
            {
                int nx = gridRoute[i].itemInfo.index_i + dx[k];
                int ny = gridRoute[i].itemInfo.index_j + dy[k];
                if(nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;

                if(gridItemInfos[nx, ny].isDigit && gridItemInfos[nx, ny].value - 1 < 0)//如果超过数字限度
                {
                    Debug.Log(nx + " " +  ny + " " + (gridItemInfos[nx,ny].value - 1));
                    Debug.Log("数字周围路径过多！");
                    isRight = false;
                }   
                gridItemInfos[nx, ny].value--;
            }
            if (i == gridRoute.Count - 1 && !(gridRoute[i].itemInfo.isSquare || gridRoute[i].itemInfo.isCircle))
            {
                Debug.Log("不是以方或圆结束");
                isRight = false;//如果路径末尾不是圆或者方
            }
        }

        if (circleValue % 2 == 0 || squareValue % 2 == 0)
        {
            Debug.Log(circleValue + " " + squareValue);
            Debug.Log("路径上圆或方的数量不为奇数！");
            isRight = false;
        }

        for (int i = 0; i < gridDigits.Count; i++)
        {
            //如果地图数字的value不为0
            if (gridItemInfos[gridDigits[i].itemInfo.index_i, gridDigits[i].itemInfo.index_j].value != 0){
                isRight = false;
                Debug.Log("未能将所有数字满足！");
            }
        }

        StartCoroutine(ClearRoute());
        if (isRight) {
            NextLevelButton.interactable=true;
            GameMgr.SaveData(nowPass+1);
            Debug.Log("恭喜过关！！！");
        }
    }
    public Button NextLevelButton;
    public IEnumerator ClearRoute()
    {
        isAnimPlaying=true;
        foreach (GridItem Item in gridRoute)
        {
            Item.highLight.SetActive(false);
            yield return new WaitForSeconds(0.05f); // 可根据需要调整延迟时间
        }
        gridRoute.Clear();
        LoadGrid();
        isAnimPlaying=false;
    }

    public void LoadGrid() //加载地图
    {
        if(GameMgr.LoadData().currentLevel==nowPass){
            NextLevelButton.interactable=false;
        }
        else{
            NextLevelButton.interactable=true;
        }
        lastItem.index_i = -1; //开始时上一个路径为空
        inCheck = false;
        gridItemInfos = new GridItemInfo[6, 6];
        gridDigits = new List<GridItem>();
        if(gridRoute.Count > 0) gridRoute.Clear();
        for(int i = gridItemPrefab.transform.childCount - 1; i >= 0; i--)//删除地图子物体
        {
            GameObject.Destroy(gridItemPrefab.transform.GetChild(i).gameObject);
        }

        int[] number = { -1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }; //下标对应数据，-1对应1,-2对应2...-10对应0
        for (int i = 0; i < 6; i++)//����ͼԪ�طŵ���ͼ��ȥ
        {
            for (int j = 0; j < 6; j++)
            {
                gridItemInfos[i, j] = new GridItemInfo();
                int index = CustomsPass.customPasses[nowPass - 1][i][j];
                if (gridItemInfos[i, j] == null) Debug.Log("NULL");
                if (index < 0) gridItemInfos[i, j].value = number[-index];//是数字就输入数值 否则为0 
                if (index < 0) gridItemInfos[i, j].isDigit = true;    //是否为数字
                if (index == 2) gridItemInfos[i, j].isSquare = true;  //是否为方形
                if (index == 3) gridItemInfos[i, j].isCircle = true;   //是否为圆形
                gridItemInfos[i, j].index_i = i;
                gridItemInfos[i, j].index_j = j;
            }
        }

        for (int i = 0; i < 6; i++)//����ͼԪ�طŵ���ͼ��ȥ
        {
            for (int j = 0; j < 6; j++)
            {
                //坐标转换
                Vector3 position = new Vector3(j * spacing, -i * spacing, 0) + firstVector3; //地图初始位置和间距矢量叠加
                GameObject Item = Resources.Load<GameObject>("Prefab/GridItem");  // ��ȡ��ͼ������������
                GameObject gameObject = Instantiate(Item, position, Quaternion.identity);  //ʵ����
                gameObject.name = $"({i},{j})";
                GridItem Info = gameObject.GetComponent<GridItem>();
                Info.itemInfo = gridItemInfos[i,j];
                
                if (Info.itemInfo.isDigit) gridDigits.Add(Info); //如果是数字放进判定数组

                gameObject.transform.SetParent(gridItemPrefab.transform, false); //��ʵ������������ΪgridItemPrefab��������
            }
        }
    }

    public void AddRoute(GridItem itemInfo)
    {
        if(lastItem.index_i == -1 && !(itemInfo.itemInfo.isCircle || itemInfo.itemInfo.isSquare))//如果不是圆或者方开始
        {
            Debug.Log("不是开始节点！");
            return;
        }

        if (lastItem.index_i != -1) //已经开始了
        {
            int[] dx = { 0, 0, 1, -1 };
            int[] dy = { 1, -1, 0, 0 };
            bool isRight = false; //默认路径不合法
            for (int k = 0; k < 4; k++)
            {
                int nx = itemInfo.itemInfo.index_i + dx[k];
                int ny = itemInfo.itemInfo.index_j + dy[k];
                if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;
                if (nx == lastItem.index_i && ny == lastItem.index_j)
                {
                    isRight = true; //能续上当前路径
                    break;
                }
            }
            if (isRight == false)
            {
                return;
            }
        }

        if (!gridRoute.Contains(itemInfo))//检查该节点是否在路径中
        {   
            itemInfo.highLight.SetActive(true); //被选高亮
            lastItem = itemInfo.itemInfo;       //上一个节点变为该节点
            gridItemInfos[itemInfo.itemInfo.index_i, itemInfo.itemInfo.index_j].value = 1;
            gridRoute.Add(itemInfo);
        }
    }

    public void LastPass()
    {
        if (nowPass > 1)
        {
            nowPass--;
        }
        else Debug.Log("已经到第一关了");

        LoadGrid();
    }

    public void NextPass()
    {
        if (nowPass < 10)
        {
            nowPass++;
        }
        else Debug.Log("已经到最后一关了");

        LoadGrid();
    }
}
