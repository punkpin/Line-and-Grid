using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShakeItem : MonoBehaviour
{
    private Vector2 originalPosition;
    private float time;
    private void Start()
    {
        originalPosition=transform.localPosition;
    }

    private void Update()
    {
        time+=Time.deltaTime;
        if (time > 0.3f)
        {
            time=0;
            transform.localPosition=Random.insideUnitCircle*3+originalPosition;
        }
    }
}
