using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Player
{
    private void Start()
    {
        passiveFatigue = 3;
    }
    void Update () 
	{
		ChargeAtBob();
	}

	void ChargeAtBob()
	{
		if(!getGrapple())
		{
			Vector3 move = new Vector3(speed, 0, 0);

			player.transform.position += move;
		}
	}
}
