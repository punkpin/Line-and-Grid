using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    //�ĸ���������·��������
    int[][] dirs = new int[][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
    //�˸����������ж����ָ����Ա�ĸ���
    int[][] dirsAll = new int[][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 },
    new int[] { 1, 1 }, new int[] { 1, -1 }, new int[] { -1, -1 }, new int[] { -1, 1 }};
    //ģ��ؿ����ݣ�0������ͨ���ӣ�-3�������ָ��ӣ�2������3����ԲȦ
    int[][] value = new int[6][]
    {
        new int[]{ 0,0,0,0,0,0 },
        new int[]{0,-3,0,0,0,0},
        new int[]{3,0,0,0,0,2},
        new int[]{ 0,0,0,0,-3,0},
        new int[]{0,0,0,0,0,0},
        new int[]{0,0,0,0,0,0}
    };
    //���������������ָ��ӵı�
    int[][] res = new int[6][]
    {
        new int[]{ 0,0,0,0,0,0 },
        new int[]{0,-3,0,0,0,0},
        new int[]{0,0,0,0,0,0},
        new int[]{ 0,0,0,0,-3,0},
        new int[]{0,0,0,0,0,0},
        new int[]{0,0,0,0,0,0}
    };
    //·��������·���ĸ��ӻᱻ���
    public List<BaseTile> pathTiles = new List<BaseTile>();
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // ��ʼ������
        GridManager.Instance.GenerateGrid(value);
    }
    void Update()
    {
        // ����������ɿ����н���ж�
        if (Input.GetMouseButtonUp(0))
        {
            EvaluatePath();
        }
    }
    //�����Ӽ���·����������ʾ
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
    //�жϽ��
    public void EvaluatePath()
    {
        if (pathTiles.Count <= 0) return;
        Debug.Log("over");
        // �ж�·���Ƿ�Ϸ����ж����ָ����Ա߾����ĸ����Ƿ��㹻
        if (canFormPath()==true&&isEnoughGrid()==true)
        {
            Debug.Log("can form path");
        }
        else
        {
            Debug.Log("it's not a path");
        }
        // ��յ���׼����һ��·��
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        if (pathTiles.Count <= 0) yield break;
        foreach (BaseTile tile in pathTiles)
        {
            tile.contentObject.SetActive(true);
            tile.highlightObject.SetActive(false);
            yield return new WaitForSeconds(0.05f); // �ɸ�����Ҫ�����ӳ�ʱ��
        }
        pathTiles.Clear();
    }
    //·���ж�
    private bool canFormPath()
    {
        //��ͷ�ͽ�β���ӵ��ж�
        //δ����ԲȦ�ͷ������ż�жϣ��������������ʵ��
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
    //���ָ����Աߵĸ����ж�
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
        //��ԭres
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
