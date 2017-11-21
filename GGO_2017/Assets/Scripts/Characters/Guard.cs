using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Player
{

    void Awake()
    {

        PassiveFatigue = 0.025f;
        PushFatigue = 0.05f;
        shoveDist.Set(5f, 0f, 0f);
        StrOfShove = 3f;
        lerpMov = 0f;
        /*Need implementation first*/
        //ShoveFatigue = 5f;
        //KickFatigue = 7f;
    }

    void Update () 
	{
        //lerpMov += Time.deltaTime;
		ChargeAtEnemy();
        Debug.Log("Player Location: " + playerLocation);
        Debug.Log("Attempting to ChargeAtEnemy...");
	}
}
