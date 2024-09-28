using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTile : BaseTile
{
    public override void Awake()
    {
        type = 3;
        canClick = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
