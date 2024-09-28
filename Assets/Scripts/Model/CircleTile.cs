using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTile : BaseTile
{
    public override void Awake()
    {
        canClick = true;
        type = 2;
    }

    
}
