using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class GridItemInfo
{
    public int value;          //��������
    public int index_i;        // i����
    public int index_j;        // j����
    public bool isDigit;       //�Ƿ�Ϊ����
    public bool isSquare;      //�Ƿ�Ϊ����
    public bool isCircle;       //�Ƿ�ΪԲ��
}

/// <summary>
/// ���ӻ���
/// </summary>
public class GridItem : MonoBehaviour
{
    public GridItemInfo itemInfo = new GridItemInfo();
    public GameObject iconDigit;          //��ȡͼ��
    public GameObject iconShape;          //��ȡͼ��
    public GameObject selected;      //����Ƿ񾭹�
    public GameObject highLight;     //�Ƿ�ѡ
    public bool isMouseOver;


    //public void OnPointerEnter(PointerEventData eventData)//�������UIʱ
    //{
    //    selected.transform.gameObject.SetActive(true);
    //    if (itemInfo.isDigit) //����ýڵ�������
    //    {
    //        int[] dx = {0,0,1,-1,1,1,-1,-1};    //8������
    //        int[] dy = {1,-1,0,0,-1,1,1,-1};
    //        for(int k = 0; k < 8; k++)
    //        {
    //            int nx = itemInfo.index_i + dx[k];
    //            int ny = itemInfo.index_j + dy[k];
    //            if(nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;//�����߽�
    //            GameObject aroundItem = GridGame.Instance.gridItemPrefab.transform.GetChild(nx * 6 + ny).gameObject;//��ȡ��Χ��Gameobject
    //            GridItem aroundGridItem = aroundItem.GetComponent<GridItem>();//��ȡ���
    //            aroundGridItem.selected.transform.gameObject.SetActive(true);//����ѡ����Ϊtrue
    //        }
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)//����뿪��UIʱ
    //{
    //    selected.transform.gameObject.SetActive(false);
    //    if (itemInfo.isDigit)
    //    {
    //        int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };
    //        int[] dy = { 1, -1, 0, 0, -1, 1, 1, -1 };
    //        for (int k = 0; k < 8; k++)
    //        {
    //            int nx = itemInfo.index_i + dx[k];
    //            int ny = itemInfo.index_j + dy[k];
    //            if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;
    //            GameObject aroundItem = GridGame.Instance.gridItemPrefab.transform.GetChild(nx * 6 + ny).gameObject;
    //            GridItem aroundGridItem = aroundItem.GetComponent<GridItem>();
    //            aroundGridItem.selected.transform.gameObject.SetActive(false);//����ѡ����Ϊfalse
    //        }
    //    }
    //}

    public void Update()
    {
        if(isMouseOver && Input.GetMouseButton(0) && !itemInfo.isDigit)
        {
            GridGame.Instance.AddRoute(this.gameObject.GetComponent<GridItem>()); // ��Ԫ����Ϣ����
        }
    }


    public virtual void OnMouseEnter() //��������UIʱ
    {
        selected.SetActive(true);
        if (itemInfo.isDigit) //����ýڵ�������
        {
            int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };    //8������
            int[] dy = { 1, -1, 0, 0, -1, 1, 1, -1 };
            for (int k = 0; k < 8; k++)
            {
                int nx = itemInfo.index_i + dx[k];
                int ny = itemInfo.index_j + dy[k];
                if (nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;//�����߽�
                GameObject aroundItem = GridGame.Instance.gridItemPrefab.transform.GetChild(nx * 6 + ny).gameObject;//��ȡ��Χ��Gameobject
                GridItem aroundGridItem = aroundItem.GetComponent<GridItem>();//��ȡ���
                aroundGridItem.selected.transform.gameObject.SetActive(true);//����ѡ����Ϊtrue
            }
        }
        isMouseOver = true;
    }

    public virtual void OnMouseExit()  //������뿪UIʱ
    {
        selected.SetActive(false);
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
                aroundGridItem.selected.transform.gameObject.SetActive(false);//����ѡ����Ϊfalse
            }
        }
        isMouseOver = false;
    }


    public void Start()
    {
        if (itemInfo.isDigit)  //���������
        {
            SpriteRenderer spriteRenderer = iconDigit.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Num/" + itemInfo.value);
            iconDigit.SetActive(true);
        }
        else if (itemInfo.isSquare) //����Ƿ���
        {
            SpriteRenderer spriteRenderer = iconShape.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Green Square");
            iconShape.SetActive(true);
        }
        else if (itemInfo.isCircle) //�����Բ��
        {
            SpriteRenderer spriteRenderer = iconShape.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Red Circle");
            iconShape.SetActive(true);
        }

    }

}
