using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class flicker : MonoBehaviour
{
    private static flicker instance;
    public static flicker Instance
    {
        get
        {
            if(instance == null) instance = FindObjectOfType<flicker>();
            return instance;
        }
    }


    public float flickerTime = 0.5f;//闪烁持续时间
    public float flickerIntervalTime = 1.0f; //闪烁间隔时间
    public int flickerCount = 0; //闪烁次数
    public float prevTime; //上次闪烁时间
    public bool visible; //是否正在闪烁

    private void Start()
    {
        prevTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (flickerCount == 0) return;

        float elapsedTime = Time.time - prevTime;//计算上次切换以来经过的时间

        if (visible)
        {
            if (elapsedTime >= flickerTime)//可见且闪烁了足够时间
            {
                GridGame.Instance.prevRoute(false);
                visible = false;
                prevTime = Time.time;
                flickerCount--;
                Debug.Log("闪烁");
            }
        }
        else
        {
            if (elapsedTime >= flickerIntervalTime && flickerCount > 0)//闪烁次数还有就恢复可见
            {
                visible = true;
                GridGame.Instance.prevRoute(true);
                prevTime = Time.time;
            }
        }
    }

    public void startFlick()
    {
        flickerCount = 3;
        prevTime = Time.time;
    }
}
