using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
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
        if (isDigit)
        {
            if (math.abs(digit) < 100) text.text = digit.ToString();
            else
            {
                if (digit > 0) text.fontSize = 80;
                else text.fontSize = 69;
                text.text = $"{digit / 100}/{math.abs(digit) % 10}";
            }
        }
        Color color;
        if(digitalType == 1) ColorUtility.TryParseHtmlString("#FFFFFF", out color);      //��
        else if(digitalType == 2) ColorUtility.TryParseHtmlString("#000000", out color); //��
        else if (digitalType == 3) ColorUtility.TryParseHtmlString("#33F43B", out color);//��
        else if (digitalType == 4) ColorUtility.TryParseHtmlString("#F43434", out color);//��
        else if (digitalType == 5) ColorUtility.TryParseHtmlString("#00BDF7", out color);//��
        else if (digitalType == 6) ColorUtility.TryParseHtmlString("#EC862B", out color);//��
        else ColorUtility.TryParseHtmlString("#9C41D7", out color);//��


        text.color = color;
    }
}
