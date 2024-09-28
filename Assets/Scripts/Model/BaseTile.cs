using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���ӻ��࣬������Ӧ���ǳ����࣬��������������
/// </summary>
public class BaseTile : MonoBehaviour
{
    [SerializeField] public GameObject highlightObject;
    [SerializeField] public GameObject contentObject;
    public bool isMouseOver;
    public bool canClick;//�Ƿ���Ե��
    public int type;//���ͣ�0��ʾ��ͨ��1��ʾ���֣�2��ʾ����3��ʾԲȦ
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
        //����·��
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
