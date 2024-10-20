using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMgr : MonoBehaviour
{
    void Update()
    {
        GetComponent<Toggle>().isOn = Screen.fullScreen;
    }
}
