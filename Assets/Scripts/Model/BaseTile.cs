using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 格子基类，理论上应该是抽象类，但是先这样放着
/// </summary>
public class BaseTile : MonoBehaviour
{
    [SerializeField] public GameObject highlightObject;
    [SerializeField] public GameObject contentObject;
    public bool isMouseOver;
    public bool canClick;//是否可以点击
    public int type;//类型，0表示普通，1表示数字，2表示方格，3表示圆圈
    public virtual void Awake()
    {

    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //加入路径
        if (isMouseOver && Input.GetMouseButton(0) && canClick)
        {
            GameController.Instance.AddPath(this.gameObject.GetComponent<BaseTile>());
        }
    }
    //public void AddPath()
    //{
    //    contentObject.SetActive(false);
    //    highlightObject.SetActive(true);
    //    if (!GridManager.Instance.pathTiles.Contains(this.gameObject))
    //    {
    //        GridManager.Instance.pathTiles.Add(this.gameObject);
    //    }
    //}
    public virtual void OnMouseEnter()
    {
        isMouseOver = true;
    }

    public virtual void OnMouseExit()
    {
        isMouseOver = false;
    }
}
