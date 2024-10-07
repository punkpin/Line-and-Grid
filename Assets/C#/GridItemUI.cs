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
        if(digitalType == 1) text.color = Color.white;
        else if(digitalType == 2) text.color = Color.black;
        else if (digitalType == 3) text.color = Color.green;
        else if (digitalType == 4) text.color = Color.red;
    }
}
