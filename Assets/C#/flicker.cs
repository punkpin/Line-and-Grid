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


    public float flickerTime = 0.5f;//��˸����ʱ��
    public float flickerIntervalTime = 1.0f; //��˸���ʱ��
    public int flickerCount = 0; //��˸����
    public float prevTime; //�ϴ���˸ʱ��
    public bool visible; //�Ƿ�������˸

    private void Start()
    {
        prevTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (flickerCount == 0) return;

        float elapsedTime = Time.time - prevTime;//�����ϴ��л�����������ʱ��

        if (visible)
        {
            if (elapsedTime >= flickerTime)//�ɼ�����˸���㹻ʱ��
            {
                GridGame.Instance.prevRoute(false);
                visible = false;
                prevTime = Time.time;
                flickerCount--;
                Debug.Log("��˸");
            }
        }
        else
        {
            if (elapsedTime >= flickerIntervalTime && flickerCount > 0)//��˸�������оͻָ��ɼ�
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
