using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Player
{

    private void Awake()
    {
        PassiveFatigue = 0.025f;
        PushFatigue = 0.05f;
        /*Need implementation first*/
        //ShoveFatigue = 5f;
        //KickFatigue = 7f;
    }

    void Update () 
	{
		ChargeAtBob();
	}

	//If not grappling bob Charge at bob
	void ChargeAtBob()
	{
		if(!getGrapple())
		{
			Vector3 move = new Vector3(speed, 0, 0);
			player.transform.position += move;
		}
	}
}
