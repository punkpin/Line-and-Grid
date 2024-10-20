using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GridGame : MonoBehaviour
{
    private float spacing = 0.168f; // 单元格间距
    private Vector3 firstVector3 = new Vector3(-0.42f, 0.419f, -0.3f); //地图的第一个位置
    public List<GridItem> nowGridPrefab; //目前地图的元素实体
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
    public int nowStage; //当前关卡主题
    
    public List<GridItem> gridRoute; //游戏路线
    public GridItemInfo lastItem = new GridItemInfo();  //上一格(用于判断路径是否合法)
    public List<GridItem> gridDigits; //数字
    public bool isAnimPlaying;    //是否正在检查
    public Button NextLevelButton; //下一关按钮
    public Button PrevLevelButton; //上一关按钮
    public bool isPass; //是否过关
    private void Start() 
    {
        LoadGrid();
    }

    public void Update()
    {
        if (!Input.GetMouseButton(0) && isAnimPlaying == false) //已经松开了鼠标 并且不在检查时间内
        {
            isAnimPlaying = true;
            CheckRoute(); //确认游戏路线
        }
    }

    public void CheckRoute()
    {
        if (gridRoute.Count <= 0)
        {
            isAnimPlaying = false;
            return;//如果游戏线路为空就先返回
        }

        
        if (CheckDigit() && CheckShape()) {
            NextLevelButton.interactable = true;
            //if (nowPass + 1 > GameMgr.LoadData().currentLevel) GameMgr.SaveData(nowPass + 1);
            Debug.Log("恭喜过关！！！");
            isPass = true;
            CustomsPass.passRecord[nowStage - 1][nowPass - 1] = true;
            CustomsPass.routeRecord[(nowStage - 1) * 10 + nowPass - 1].Clear();
            for (int i = 0; i < gridRoute.Count; i++)
            {
                CustomsPass.routeRecord[(nowStage - 1) * 10 + nowPass - 1].Add(gridRoute[i].itemInfo.index_i * 10 + gridRoute[i].itemInfo.index_j);
            }
        }
        
        if (CustomsPass.passRecord[nowStage - 1][nowPass - 1] == false)
        {
            CustomsPass.routeRecord[(nowStage - 1) * 10 + nowPass - 1].Clear();
            for (int i = 0; i < gridRoute.Count; i++)
            {
                CustomsPass.routeRecord[(nowStage - 1) * 10 + nowPass - 1].Add(gridRoute[i].itemInfo.index_i * 10 + gridRoute[i].itemInfo.index_j);
            }
        }
        

        StartCoroutine(ClearRoute());
    }
    
    public bool CheckShape()//形状判断
    {
        bool isRight = true; //默认路径正确
        int circleValue = 0, squareValue = 0; //圆和方的数量初始为0
        Debug.Log("该路径长度为：" + gridRoute.Count);
        for (int i = 0; i < gridRoute.Count; i++)
        {
            if (gridRoute[i].itemInfo.isCircle) circleValue++; //路径是圆
            if (gridRoute[i].itemInfo.isSquare) squareValue++; //路径是方

            if (i == gridRoute.Count - 1 && !(gridRoute[i].itemInfo.isSquare || gridRoute[i].itemInfo.isCircle) && nowStage != 3)
            {
                Debug.Log("不是以方或圆结束");
                isRight = false;//如果路径末尾不是圆或者方
            }
        }

        if ((circleValue % 2 == 0 || squareValue % 2 == 0) && nowStage == 1)
        {
            Debug.Log(circleValue + " " + squareValue);
            Debug.Log("路径上圆或方的数量不为奇数！");
            isRight = false;
        }
        if(circleValue == 0 || squareValue == 0)
        {
            Debug.Log("路径中至少有一个方和圆");
            isRight = false;
        }
        return isRight;
    }

    public bool CheckDigit()//数字判断
    {
        bool isRight = true; //开始默认满足
        int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };
        int[] dy = { 1, -1, 0, 0, 1, -1, -1, 1 };
        for (int i = 0; i < gridDigits.Count; i++) //遍历地图数字元素
        {
            int tx = gridDigits[i].itemInfo.index_i; //数字元素的x坐标
            int ty = gridDigits[i].itemInfo.index_j; //数字元素的y坐标
            int value = gridDigits[i].itemInfo.value;//数字
            if (gridDigits[i].itemInfo.digitalType == 1) //为白色数字
            {
                for(int k = 0; k < 8; k++)
                {
                    int nx = tx + dx[k], ny = ty + dy[k];
                    if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;
                    if (gridItemInfos[nx, ny].value == 1 && gridItemInfos[nx,ny].isDigit == false) value--; //周围是路径就减一
                    if (value < 0) break;
                }
                if(value != 0)
                {
                    Debug.Log("(" + (tx + 1) + "," + (ty + 1) + ")位置的数字未满足或超过");
                    
                    isRight = false;
                }
            }
            else if (gridDigits[i].itemInfo.digitalType == 2) //为黑色数字
            {
                for (int k = 0; k < 8; k++)
                {
                    int nx = tx + dx[k], ny = ty + dy[k];
                    if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;
                    if (gridItemInfos[nx, ny].value != 1 || gridItemInfos[nx,ny].isDigit) value--; //周围不是路径或者是数字就减一
                    if (value < 0) break;
                }
                if (value != 0)
                {
                    Debug.Log("(" + (tx + 1) + "," + (ty + 1) + ")位置的数字未满足或超过");
                    
                    isRight = false;
                }
            }
            else if (gridDigits[i].itemInfo.digitalType == 3) //为绿色数字
            {       
                for(int j = 0; j < gridRoute.Count; j++)
                {
                    if (gridRoute[j].itemInfo.isSquare == false) continue;
                    int nx = gridRoute[j].itemInfo.index_i;
                    int ny = gridRoute[j].itemInfo.index_j;
                    if (value > 0)
                    {
                        if (Mathf.Abs(tx - nx) > value || Mathf.Abs(ty - ny) > value)
                        {
                            Debug.Log("起点的位置在(" + (tx + 1) + "," + (ty + 1) + ")的 " + value + "范围外！");
                            isRight = false;
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(tx - nx) <= value || Mathf.Abs(ty - ny) <= value)
                        {
                            Debug.Log("起点的位置在(" + (tx + 1) + "," + (ty + 1) + ")的 " + -value + "范围内！");
                            isRight = false;
                        }
                    }

                } 
            }
            else if (gridDigits[i].itemInfo.digitalType == 4) //为红色数字
            {
                for (int j = 0; j < gridRoute.Count; j++)
                {
                    if (gridRoute[j].itemInfo.isCircle == false) continue;
                    int nx = gridRoute[j].itemInfo.index_i;
                    int ny = gridRoute[j].itemInfo.index_j;
                    if (value > 0)
                    {
                        if (Mathf.Abs(tx - nx) > value || Mathf.Abs(ty - ny) > value)
                        {
                            Debug.Log("起点的位置在(" + (tx + 1) + "," + (ty + 1) + ")的 " + value + "范围外！");
                            isRight = false;
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(tx - nx) <= value || Mathf.Abs(ty - ny) <= value)
                        {
                            Debug.Log("起点的位置在(" + (tx + 1) + "," + (ty + 1) + ")的 " + -value + "范围内！");
                            isRight = false;
                        }
                    }

                }
            }
            else if(gridDigits[i].itemInfo.digitalType == 5) //为蓝色数字
            {
                int a = 0, b = 0;//a为被经过的格子，b为未被经过的格子
                for (int k = 0; k < 8; k++)
                {
                    int nx = tx + dx[k], ny = ty + dy[k]; 
                    if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;
                    if (gridItemInfos[nx, ny].value == 1 && gridItemInfos[nx, ny].isDigit == false) a++; //周围是路径就减一
                    else b++;
                }
                if (a - b != value)
                {
                    Debug.Log("(" + (tx + 1) + "," + (ty + 1) + ")位置的数字未满足或超过");

                    isRight = false;
                }
            }
            else if(gridDigits[i].itemInfo.digitalType == 6) //为橙色格子
            {
                int a = 0, b = 0;//a为被经过的格子，b为未被经过的格子
                for (int k = 0; k < 8; k++)
                {
                    int nx = tx + dx[k], ny = ty + dy[k];
                    if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;
                    if (gridItemInfos[nx, ny].value == 1 && gridItemInfos[nx, ny].isDigit == false) a++; //周围是路径就减一
                    else b++;
                }
                if (a * (a - b) != value)
                {
                    Debug.Log("(" + (tx + 1) + "," + (ty + 1) + ")位置的数字未满足或超过");

                    isRight = false;
                }
            }
            else if(gridDigits[i].itemInfo.digitalType == 7) //为紫色数字
            {
                int a = 0, b = 0;
                for (int k = 0; k < 8; k++)
                {
                    int nx = tx + dx[k], ny = ty + dy[k];
                    if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;
                    if (gridItemInfos[nx, ny].value == 1 && gridItemInfos[nx, ny].isDigit == false) a++; //周围是路径就减一
                    else b++;
                }
                if(math.abs(value) < 100)
                {
                    if (a / (a - b) != value)
                    {
                        Debug.Log("(" + (tx + 1) + "," + (ty + 1) + ")位置的数字未满足或超过");
                        isRight = false;
                    }
                }
                else
                {
                    if(a / (a - b) != (math.abs(value) / 100) / (value % 10))
                    {
                        Debug.Log("(" + (tx + 1) + "," + (ty + 1) + ")位置的数字未满足或超过");
                        isRight = false;
                    }
                }
                
            }
        }
        return isRight;
    }


    public IEnumerator ClearRoute()
    {
        isAnimPlaying=true;
        NextLevelButton.gameObject.SetActive(false);
        PrevLevelButton.gameObject.SetActive(false);
        foreach (GridItem Item in gridRoute)
        {
            Item.highLight.SetActive(false);
            yield return new WaitForSeconds(0.05f); // 可根据需要调整延迟时间
        }
        NextLevelButton.gameObject.SetActive(true);
        PrevLevelButton.gameObject.SetActive(true);
        gridRoute.Clear();
        isAnimPlaying = false;
        if (isPass == true) //如果通关
        {
            NextPass();
            GameAudio.Instance.Correct();
        }
        else
        {
            LoadGrid();
            GameAudio.Instance.Wrong();
            flicker.Instance.startFlick();
            prevRoute(false);
        }
    }

    public void LoadGrid() //加载地图
    {
        if (nowPass == 1)
        {
            PrevLevelButton.gameObject.SetActive(false);
        }
        else
        {
            PrevLevelButton.gameObject.SetActive(true);
        }
        //GameMgr.LoadData().currentLevel == nowPass ||
        if ( nowPass == 10)
        {
            NextLevelButton.gameObject.SetActive(false);
        }
        else
        {
            NextLevelButton.gameObject.SetActive(true);
        }
        lastItem.index_i = -1; //开始时上一个路径为空
        isAnimPlaying = false;
        isPass = false;
        gridItemInfos = new GridItemInfo[6, 6];
        gridDigits = new List<GridItem>();
        if(gridRoute.Count > 0) gridRoute.Clear();
        nowGridPrefab.Clear();
        for(int i = gridItemPrefab.transform.childCount - 1; i >= 0; i--)//删除地图子物体
        {
            GameObject.Destroy(gridItemPrefab.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < 6; i++)//����ͼԪ�طŵ���ͼ��ȥ
        {
            for (int j = 0; j < 6; j++)
            {
                gridItemInfos[i, j] = new GridItemInfo();
                int index = 0;
                if(nowStage == 1)
                {
                    index = CustomsPass.customPassesStage1[nowPass - 1][i][j];

                }
                else if(nowStage == 2)
                {
                    index = CustomsPass.customPassesStage2[nowPass - 1][i][j];
                }
                else if(nowStage == 3)
                {
                    index = CustomsPass.customPassesStage3[nowPass - 1][i][j];
                }
                else if(nowStage == 4)
                {
                    index = CustomsPass.customPassesStage4[nowPass - 1][i][j];
                }
                if (math.abs(index) == 2)
                {
                    gridItemInfos[i, j].isSquare = true;  //是否为方形
                    if (index < 0) gridItemInfos[i, j].digitalType = 1;//如果为负数就将其隐藏
                }
                else if (math.abs(index) == 3)
                {
                    gridItemInfos[i, j].isCircle = true;   //是否为圆形
                    if (index < 0) gridItemInfos[i, j].digitalType = 1;
                }
                else if (index != 0)
                {
                    gridItemInfos[i, j].isDigit = true; //是否为数字
                    gridItemInfos[i, j].value = index % 10000;
                    gridItemInfos[i, j].digitalType = Mathf.Abs(index / 10000);
                }
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
                nowGridPrefab.Add(Info);
                gameObject.transform.SetParent(gridItemPrefab.transform, false); //��ʵ������������ΪgridItemPrefab��������
            }
        }

        GridGameUI.Instance.LoadGridUI();
        prevRoute(true);
    }

    public void AddRoute(GridItem itemInfo)
    {
        prevRoute(false);
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
            List<int> index = new List<int>();
            if (nowStage == 3)
            {
                index = Mirroring(itemInfo);
                if (index.Count != 0 && index[0] == -1) return;
            }
            itemInfo.highLight.SetActive(true); //被选高亮
            lastItem = itemInfo.itemInfo;       //上一个节点变为该节点
            gridItemInfos[itemInfo.itemInfo.index_i, itemInfo.itemInfo.index_j].value = 1;
            gridRoute.Add(itemInfo);

            if(nowStage == 3)
            {
                for(int k = 0; k < index.Count; k++)
                {
                    int i = index[k] / 10, j = index[k] % 10;
                    if (i < 0 || i >= 6 || j < 0 || j >= 6) continue; //对称位置超出地图
                    nowGridPrefab[i * 6 + j].highLight.SetActive(true);
                    gridItemInfos[i, j].value = 1;
                    gridRoute.Add(nowGridPrefab[i * 6 + j]);
                }
            }
        }
    }

    public void prevRoute(bool visible) //上一次路径
    {
        for (int i = 0; i < CustomsPass.routeRecord[(nowStage - 1) * 10 + nowPass - 1].Count; i++)//遍历上一次路径
        {
            int dx = CustomsPass.routeRecord[(nowStage - 1) * 10 + nowPass - 1][i] / 10;
            int dy = CustomsPass.routeRecord[(nowStage - 1) * 10 + nowPass - 1][i] % 10;
            SpriteRenderer spriteRenderer = nowGridPrefab[dx * 6 + dy].lowLight.GetComponent<SpriteRenderer>();
            UnityEngine.Color color;
            if (CustomsPass.passRecord[nowStage - 1][nowPass - 1] == false) //如果上一次没通关
            {
                UnityEngine.ColorUtility.TryParseHtmlString("#F5A190", out color);//改为浅红色
                spriteRenderer.color = color;
            }
            if(visible) nowGridPrefab[dx * 6 + dy].lowLight.SetActive(true);
            else nowGridPrefab[dx * 6 + dy].lowLight.SetActive(false);
        }
    }

    public List<int> Mirroring(GridItem itemInfo) //计算对称位置
    {
        List<int> index = new List<int>();
        List<int> Error = new List<int>();
        int x1 = itemInfo.itemInfo.index_j;
        int y1 = itemInfo.itemInfo.index_i;
        Error.Add(-1);
        if (nowPass <= 7)
        {
            functionInfo info = CustomsPass.functionInfos[nowPass - 1];
            float d = math.abs(info.A * x1 + info.B * y1 + info.C) / math.sqrt(info.A * info.A + info.B * info.B);
            int x2, y2;
            if(info.A * x1 + info.B * y1 + info.C > 0)
            {
                x2 = (int)math.round(x1 - 2 * d * info.A / math.sqrt(info.A * info.A + info.B * info.B));
                y2 = (int)math.round(y1 - 2 * d * info.B / math.sqrt(info.A * info.A + info.B * info.B));
            }
            else
            {
                x2 = (int)math.round(x1 + 2 * d * info.A / math.sqrt(info.A * info.A + info.B * info.B));
                y2 = (int)math.round(y1 + 2 * d * info.B / math.sqrt(info.A * info.A + info.B * info.B));
            }
            if (x2 < 0 || x2 >= 6 || y2 < 0 || y2 >= 6) return index;
            if (nowGridPrefab[y2 * 6 + x2].itemInfo.isDigit == false)//对称的对面不是数字
            {
                index.Add(y2 * 10 + x2);
                return index;
            }
        }
        else if(nowPass == 8)
        {
            int x2 = (int)(2 * 3.5f - x1);
            int y3 = (int)(2 * 2.5f - y1);
            
            if (x2 >= 0 && x2 < 6 && y1 >= 0 && y1 < 6) //在地图内
            {
                if (nowGridPrefab[y1 * 6 + x2].itemInfo.isDigit == false) index.Add(y1 * 10 + x2); //不为数字
                else
                {
                    Debug.Log(y1 + " " + x2);
                    return Error;
                }
                
            }
            if (x1 >= 0 && x1 < 6 && y3 >= 0 && y3 < 6)
            {
                if (nowGridPrefab[y3 * 6 + x1].itemInfo.isDigit == false) index.Add(y3 * 10 + x1);
                else
                {
                    Debug.Log(y3 + " " + x1);
                    return Error;
                }
            }
            if (x2 >= 0 && x2 < 6 && y3 >= 0 && y3 < 6)
            {
                if (nowGridPrefab[y3 * 6 + x2].itemInfo.isDigit == false) index.Add(y3 * 10 + x2);
                else
                {
                    Debug.Log(y3 + " " + x2);
                    return Error;
                }

            }
            return index;
        }
        else if(nowPass == 9)
        {
            int x2 = (int)(2 * 2.5f - x1);
            int y3 = (int)(2 * 2.5f - y1);
            if (x2 >= 0 && x2 < 6 && y3 >= 0 && y3 < 6)
            {
                if (nowGridPrefab[y3 * 6 + x2].itemInfo.isDigit == false) index.Add(y3 * 10 + x2);
                else
                {
                    Debug.Log(y3 + " " + x2);
                    return Error;
                }
            }
            return index;
        }
        else if(nowPass == 10)
        {
            int x2, y2, x3, y3;
            if ((x1 == 0 && y1 == 0) || (gridRoute.Count > 0 && gridRoute[0].itemInfo.index_i == 0 && gridRoute[0].itemInfo.index_j == 0)) //第10关就两种出发路线，干脆就枚举这两种可能好了
            {
                x2 = 4 + x1;
                y2 = 2 + y1;
                x3 = 3 - x1;
                y3 = 5 - y1;
            }
            else
            {
                x2 = x1 - 4;
                y2 = y1 - 2;
                x3 = 7 - x1;
                y3 = 8 - y1;
            }
            if (x2 >= 0 && x2 < 6 && y2 >= 0 && y2 < 6) //在地图内
            {
                if (nowGridPrefab[y2 * 6 + x2].itemInfo.isDigit == false) index.Add(y2 * 10 + x2); //不为数字
                else
                {
                    Debug.Log(y2 + " " + x2);
                    return Error;
                }

            }
            if (x3 >= 0 && x3 < 6 && y3 >= 0 && y3 < 6)
            {
                if (nowGridPrefab[y3 * 6 + x3].itemInfo.isDigit == false) index.Add(y3 * 10 + x3);
                else
                {
                    Debug.Log(y3 + " " + x3);
                    return Error;
                }
            }
            return index;
        }
        return Error;
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
