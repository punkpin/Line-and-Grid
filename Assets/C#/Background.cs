using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Background : MonoBehaviour
{
    public GameObject image;
    public GameObject image1;
    private void Start()
    {
        InitializeItems();
    }
    
    void InitializeItems()
    {
        GameObject temp;
        float xOffset = Camera.main.pixelWidth/26f;
        float yOffset = Camera.main.pixelHeight / 14f;
        for (float x = xOffset; x < Camera.main.pixelWidth; x+=Camera.main.pixelWidth/13f)
        {
            for (float y = yOffset; y < Camera.main.pixelHeight; y+=Camera.main.pixelHeight/7f)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector2(x, y));
                temp=Instantiate(Random.Range(0,2)==0?image:image1,new Vector3(pos.x,pos.y,0),Quaternion.identity,transform);
                temp.GetComponent<RectTransform>().localPosition=new Vector3(temp.GetComponent<RectTransform>().localPosition.x,temp.GetComponent<RectTransform>().localPosition.y,0);
            }
        }
    }
}
