using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefab;// 格子预制体
    [SerializeField] private Transform camaraTransform;
    public float spacing; // 单元格间距
    public static GridManager Instance;
    //public Rect restrictedArea; // 在 Inspector 中设置该区域
    //public Color gizmoColor; // Gizmo 的颜色
    private void Awake()
    {
        Instance=this;
    }
    private void Start()
    {

    }
    public void GenerateGrid(int[][] grid)
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                //坐标转换
                Vector3 position = new Vector3(j * spacing, -i * spacing,0 );
                if (grid[i][j] >= 0)
                {
                    GameObject go = Instantiate(tilePrefab[grid[i][j]], position, Quaternion.identity);
                    go.transform.SetParent(transform);
                }
                else
                {
                    GameObject go = Instantiate(tilePrefab[1], position, Quaternion.identity);
                    go.transform.SetParent(transform);
                }
                
            }
        }
        camaraTransform.position = new Vector3(2.5f, -2.5f, -10);
    }

}
