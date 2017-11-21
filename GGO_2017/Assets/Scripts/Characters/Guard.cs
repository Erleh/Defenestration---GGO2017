using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Player
{

    void Awake()
    {

        PassiveFatigue = 0.025f;
        PushFatigue = 0.05f;
        speed = -.2f;
    //StrOfShove = -3f;
    /*Need implementation first*/
    //ShoveFatigue = 5f;
    //KickFatigue = 7f;
}
    void Update (){}
}
