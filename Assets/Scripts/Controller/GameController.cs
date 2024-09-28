using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    //四个方向用于路径的搜索
    int[][] dirs = new int[][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
    //八个方向用于判断数字格子旁别的格子
    int[][] dirsAll = new int[][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 },
    new int[] { 1, 1 }, new int[] { 1, -1 }, new int[] { -1, -1 }, new int[] { -1, 1 }};
    //模拟关卡数据，0代表普通格子，-3代表数字格子，2代表方格，3代表圆圈
    int[][] value = new int[6][]
    {
        new int[]{ 0,0,0,0,0,0 },
        new int[]{0,-3,0,0,0,0},
        new int[]{3,0,0,0,0,2},
        new int[]{ 0,0,0,0,-3,0},
        new int[]{0,0,0,0,0,0},
        new int[]{0,0,0,0,0,0}
    };
    //用来单独计算数字格子的表
    int[][] res = new int[6][]
    {
        new int[]{ 0,0,0,0,0,0 },
        new int[]{0,-3,0,0,0,0},
        new int[]{0,0,0,0,0,0},
        new int[]{ 0,0,0,0,-3,0},
        new int[]{0,0,0,0,0,0},
        new int[]{0,0,0,0,0,0}
    };
    //路径，加入路径的格子会被标记
    public List<BaseTile> pathTiles = new List<BaseTile>();
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // 初始化格子
        GridManager.Instance.GenerateGrid(value);
    }
    void Update()
    {
        // 检测鼠标左键松开进行结果判断
        if (Input.GetMouseButtonUp(0))
        {
            EvaluatePath();
        }
    }
    //将格子加入路径并高亮显示
    public void AddPath(BaseTile grid)
    {
        //grid.GetComponent<BaseTile>().contentObject.SetActive(false);
        grid.contentObject.SetActive(false);
        grid.highlightObject.SetActive(true);
        if (!pathTiles.Contains(grid))
        {
            pathTiles.Add(grid);
        }
    }
    //判断结果
    public void EvaluatePath()
    {
        if (pathTiles.Count <= 0) return;
        Debug.Log("over");
        // 判断路径是否合法，判断数字格子旁边经过的格子是否足够
        if (canFormPath()==true&&isEnoughGrid()==true)
        {
            Debug.Log("can form path");
        }
        else
        {
            Debug.Log("it's not a path");
        }
        // 清空点以准备下一个路径
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        if (pathTiles.Count <= 0) yield break;
        foreach (BaseTile tile in pathTiles)
        {
            tile.contentObject.SetActive(true);
            tile.highlightObject.SetActive(false);
            yield return new WaitForSeconds(0.05f); // 可根据需要调整延迟时间
        }
        pathTiles.Clear();
    }
    //路径判断
    private bool canFormPath()
    {
        //开头和结尾格子的判断
        //未进行圆圈和方格的奇偶判断，加入计数器即可实现
        if (pathTiles[pathTiles.Count - 1].type != 2 && pathTiles[pathTiles.Count - 1].type != 3) return false;
        if (pathTiles[0].type != 2 && pathTiles[0].type != 3) return false;
        if (pathTiles[pathTiles.Count - 1].type ==pathTiles[0].type) return false;

        for (int i = 0; i < pathTiles.Count - 1; i++)
        {
            bool isNext = false;
            int x = (int)-pathTiles[i].transform.position.y;
            int y = (int)pathTiles[i].transform.position.x;
            foreach (var dir in dirs)
            {
                int newX = x + dir[0];
                int newY = y + dir[1];
                if (newX >= 0 && newX < 6 && newY >= 0 && newY < 6)
                {
                    if (newX == (int)-pathTiles[i + 1].transform.position.y && newY == (int)pathTiles[i + 1].transform.position.x)
                    {
                        isNext = true;
                    }
                }
            }
            if (isNext == false) return false;
        }
        return true;
    }
    //数字格子旁边的格子判断
    private bool isEnoughGrid()
    {
        bool flag = true;
        for (int i = 0; i < pathTiles.Count; i++)
        {
            int x = (int)-pathTiles[i].transform.position.y;
            int y = (int)pathTiles[i].transform.position.x;
            foreach (var dir in dirsAll)
            {
                int newX = x + dir[0];
                int newY = y + dir[1];
                if (newX >= 0 && newX < 6 && newY >= 0 && newY < 6 && value[newX][newY] < 0)
                {
                    
                    res[newX][newY]++;
                }
            }
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (res[i][j] != 0)
                {
                    flag=false;
                    break;
                }
            }
        }
        //还原res
        res = new int[6][]
        {
            new int[]{ 0,0,0,0,0,0 },
            new int[]{0,-3,0,0,0,0},
            new int[]{0,0,0,0,0,0},
            new int[]{ 0,0,0,0,-3,0},
            new int[]{0,0,0,0,0,0},
            new int[]{0,0,0,0,0,0}
        };
        //return flag==true? true:false;
        return flag;
    }
}
