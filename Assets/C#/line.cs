using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(LineRenderer))]
public class line : MonoBehaviour
{
    public GameObject next;
    public int index;
    private LineRenderer _lineRenderer;
    private void Start()
    {
        _lineRenderer=GetComponent<LineRenderer>();
        Texture2D U = Resources.Load<Texture2D>("Sprite/Background/UnlockedLevels");
        Sprite Unlocked=Sprite.Create(U,new Rect(0,0,U.width,U.height),new Vector2(0.5f,0.5f));
        Texture2D L= Resources.Load<Texture2D>("Sprite/Background/LockedLevels");
        Sprite Locked=Sprite.Create(L,new Rect(0,0,L.width,L.height),new Vector2(0.5f,0.5f));
        if (GameMgr.LoadData(index).currentLevel==-1)
        {
            GetComponent<Image>().sprite = Locked;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Image>().sprite = Unlocked;
            GetComponent<Button>().interactable = true;
        }
        if (next != null)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, next.transform.position);
            if (index <= 3)
            {
                if (GameMgr.LoadData(index+1).currentLevel==-1)
                {
                    _lineRenderer.textureScale = new Vector2(3,1);
                    _lineRenderer.materials[1].color = Color.gray;
                }
                else
                {
                    _lineRenderer.textureScale=new Vector2(0,1);
                    _lineRenderer.materials[1].color = Color.green;
                }
            }
            
        }
    }
    
}
