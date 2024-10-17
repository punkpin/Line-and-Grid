using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridItemUI : MonoBehaviour
{
    public int digit; //��ʾ������
    public bool isDigit;    //�Ƿ�Ϊ����
    public int digitalType;
    public Text text;

    private void Start()
    {
        if(isDigit) text.text = digit.ToString();
        Color color;
        if(digitalType == 1) ColorUtility.TryParseHtmlString("#FFFFFF", out color);      //��
        else if(digitalType == 2) ColorUtility.TryParseHtmlString("#000000", out color); //��
        else if (digitalType == 3) ColorUtility.TryParseHtmlString("#33F43B", out color);//��
        else if (digitalType == 4) ColorUtility.TryParseHtmlString("#F43434", out color);//��
        else if (digitalType == 5) ColorUtility.TryParseHtmlString("#00BDF7", out color);//��
        else ColorUtility.TryParseHtmlString("#EC862B", out color);//��


        text.color = color;
    }
}
