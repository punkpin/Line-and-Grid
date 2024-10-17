using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridItemUI : MonoBehaviour
{
    public int digit; //显示的数字
    public bool isDigit;    //是否为数字
    public int digitalType;
    public Text text;

    private void Start()
    {
        if(isDigit) text.text = digit.ToString();
        Color color;
        if(digitalType == 1) ColorUtility.TryParseHtmlString("#FFFFFF", out color);      //白
        else if(digitalType == 2) ColorUtility.TryParseHtmlString("#000000", out color); //黑
        else if (digitalType == 3) ColorUtility.TryParseHtmlString("#33F43B", out color);//绿
        else if (digitalType == 4) ColorUtility.TryParseHtmlString("#F43434", out color);//红
        else if (digitalType == 5) ColorUtility.TryParseHtmlString("#00BDF7", out color);//蓝
        else ColorUtility.TryParseHtmlString("#EC862B", out color);//橙


        text.color = color;
    }
}
