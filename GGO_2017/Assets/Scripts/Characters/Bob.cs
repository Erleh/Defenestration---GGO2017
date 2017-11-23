using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Enemy 
{
    void Awake()
    {
        shoveAir = 1f;
        shoveDist.Set(-2.5f, 0f, 0f);
    }

    void FixedUpdate()
    {
        if (p.grapple && p.shoveCoroutine == null && p.chargeCoroutine == null && !p.pushing)
        {
            //Debug.Log("Resisting..");
            Resist();
        }
    }
}
