using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class GridItemInfo
{
    public int value;          //��������
    internal int index_i;        // i����
    internal int index_j;        // j����
    public bool isDigit;       //�Ƿ�Ϊ����
    public bool isSquare;      //�Ƿ�Ϊ����
    public bool isCirle;       //�Ƿ�ΪԲ��
}

public class GridItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GridItemInfo itemInfo = new GridItemInfo();
    public Image iconDigit;          //��ȡͼ��
    public Image iconShape;          //��ȡͼ��
    public Image selected;      //�Ƿ�ѡ


    public void OnPointerEnter(PointerEventData eventData)//�������UIʱ
    {
        selected.transform.gameObject.SetActive(true);
        if (itemInfo.isDigit) //����ýڵ�������
        {
            int[] dx = {0,0,1,-1,1,1,-1,-1};    //8������
            int[] dy = {1,-1,0,0,-1,1,1,-1};
            for(int k = 0; k < 8; k++)
            {
                int nx = itemInfo.index_i + dx[k];
                int ny = itemInfo.index_j + dy[k];
                if(nx < 0 || nx >= 6 || ny < 0 || ny >= 6) continue;//�����߽�
                GameObject aroundItem = GridGame.Instance.gridItemPrefab.transform.GetChild(nx * 6 + ny).gameObject;//��ȡ��Χ��Gameobject
                GridItem aroundGridItem = aroundItem.GetComponent<GridItem>();//��ȡ���
                aroundGridItem.selected.transform.gameObject.SetActive(true);//����ѡ����Ϊtrue
            }
        }
        
        
    }

    public void OnPointerExit(PointerEventData eventData)//����뿪��UIʱ
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
                aroundGridItem.selected.transform.gameObject.SetActive(false);//����ѡ����Ϊfalse
            }
        }
    }

    public void Start()
    {
        if (itemInfo.isDigit)  //���������
        {
            iconDigit.sprite = Resources.Load<Sprite>("Sprite/Num/" + itemInfo.value);
            iconDigit.transform.gameObject.SetActive(true);
        }
        else if (itemInfo.isSquare) //����Ƿ���
        {
            iconShape.sprite = Resources.Load<Sprite>("Sprite/Green Square");
            iconShape.transform.gameObject.SetActive(true);
        }
        else if (itemInfo.isCirle) //�����Բ��
        {
            iconShape.sprite = Resources.Load<Sprite>("Sprite/Red Circle");
            iconShape.transform.gameObject.SetActive(true);
        }

    }

}
