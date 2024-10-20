using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMgr : MonoBehaviour
{
    Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = Screen.fullScreen;
        toggle.onValueChanged.AddListener((bool isFull) =>
        {
            Screen.fullScreen = isFull;
            if (!isFull)
            {
                Screen.SetResolution(1920, 1080, false);
            }
        });
    }
}
