using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    Toggle toggle;
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = !AudioListener.pause;
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);

        // 左右边界坐标
        float leftBoundary = corners[0].x; // 左边界
        float rightBoundary = corners[2].x;
        RectTransform rectTransform = transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        rectTransform.position = AudioListener.pause
            ? new Vector2((rightBoundary - leftBoundary) / 4 + leftBoundary, rectTransform.position.y)
            : new Vector2(rightBoundary - (rightBoundary - leftBoundary) / 4, rectTransform.position.y);
        toggle.onValueChanged.AddListener(arg0 =>
        {
            GetComponent<RectTransform>().GetWorldCorners(corners);

            // 左右边界坐标
            leftBoundary = corners[0].x; // 左边界
            rightBoundary = corners[2].x; // 右边界
            rectTransform.DOMove(!arg0
                ? new Vector3((rightBoundary - leftBoundary) / 4 + leftBoundary, rectTransform.position.y, 0)
                : new Vector3(rightBoundary - (rightBoundary - leftBoundary) / 4, rectTransform.position.y, 0), 0.1f);
        } 
        );
    }
}
