using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject player;
	public float speed = -.2f;

	private Vector3 playerLocation;
	private bool grapple = false;

	void Awake()
	{
		playerLocation = player.transform.position;
	}

	void Start () 
	{
		//Subscribe Event for when character is pushing
		EventHandler.onPush += this.OnCharacterPush;
	}

	void Update () 
	{
	}

	public void OnCharacterPush(GameObject character)
	{
		//When available add change in fatigue here
		//UpdateRayCastOrigins();

		if(grapple)
		{
			Vector3 move = new Vector3(speed, 0, 0);

			player.transform.position += move;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		//Test collision
		Debug.Log("Collide");

		if(col.gameObject.CompareTag("Enemy"))
		{
			//col.gameObject.transform.parent = player.transform;

			speed *= -1;

			grapple = true;
		}
	}

	public bool getGrapple()
	{
		return grapple;
	}
}
