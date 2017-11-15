using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject player;
	public float speed = -.2f;

	private Vector3 playerLocation;
	private bool grapple = false;

	//Placeholder
	private float fatigue = 0f;

	void Awake()
	{
		playerLocation = player.transform.position;
	}

	void Start () 
	{
		//Subscribe Event for when character is pushing
		EventHandler.onPush += this.OnCharacterPush;
	}

	public void OnCharacterPush(GameObject character)
	{
		//When available add change in fatigue here

		if(grapple && fatigue != 100)
		{
			float pushBack = speed;
			Vector3 move = new Vector3(pushBack, 0f, 0f);

			//PLACEHOLDER
			fatigue += .5f;
			Debug.Log("Fatigue level = " + fatigue);

			player.transform.position += move;
		}
		else if(fatigue == 100)
		{
			Debug.Log("FATIGUED");
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Enemy"))
		{
			col.gameObject.transform.parent = player.transform;

			grapple = true;
		}
	}

	public bool getGrapple()
	{
		return grapple;
	}
}
