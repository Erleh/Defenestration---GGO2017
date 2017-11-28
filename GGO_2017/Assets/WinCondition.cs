using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour {

	/*
	void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("hit1");
        if (col.GetComponent<Guard>() != null)
        {
            //Debug.Log("hit2");
            if (col.GetComponent<Guard>().pushing || col.GetComponent<Guard>().shoving || col.GetComponent<Guard>().kicking)
            {
                //Debug.Log("hit3");
                col.GetComponent<Guard>().Win();
            }
        }
    }
    */

	void OnTriggerStay2D(Collider2D col) 
	{
		Debug.Log("hit1");
		if (col.GetComponent<Guard>() != null)
		{
			Debug.Log("hit2");
			if (col.GetComponent<Guard>().pushing || col.GetComponent<Guard>().shoving || col.GetComponent<Guard>().kicking)
			{
				Debug.Log("hit3");
				col.GetComponent<Guard>().Win();
			}
		}
	}
}
