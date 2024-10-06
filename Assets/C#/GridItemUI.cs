using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridItemUI : MonoBehaviour
{
    public int digit;
    public Text text;

    private void Start()
    {
        if(digit != -1) text.text = digit.ToString();
    }
}
