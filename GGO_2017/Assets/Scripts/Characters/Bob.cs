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
    private void Start()
    {
        groundY = this.gameObject.transform.position.y;
    }
    void FixedUpdate()
    {
        Debug.Log(canBreak);
        Debug.Log(groundY);
        Debug.Log(this.gameObject.transform.position.y);
        if(this.gameObject.transform.position.y == groundY)
        {
            canBreak = true;
        }
        if (p.grapple && p.kickCoroutine == null && p.shoveCoroutine == null && p.chargeCoroutine == null && !p.pushing)
        {
            //Debug.Log("Resisting..");
            Resist();
        }
    }
}
