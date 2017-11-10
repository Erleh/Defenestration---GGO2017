using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

	public GameObject guard;
	public float speed = -.2f;

	private Vector3 guardLocation;
	private bool grapple = false;

	void Awake()
	{
		guardLocation = guard.transform.position;
	}
		
	void Start () 
	{
		//Subscribe Event for when character is pushing
		EventHandler.onPush += this.OnCharacterPush;
	}

	// Update is called once per frame
	void Update () 
	{
		ChargeAtBob();
	}

	public void OnCharacterPush(GameObject character)
	{
		//When available add change in fatigue here

		if(grapple)
		{
			Vector3 move = new Vector3(speed, 0, 0);

			guard.transform.position += move;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("Collide");

		if(col.gameObject.CompareTag("Enemy"))
		{
			col.gameObject.transform.parent = guard.transform;

			speed *= -1;

			grapple = true;
		}
	}

	void ChargeAtBob()
	{
		if(!grapple)
		{
			Vector3 move = new Vector3(speed, 0, 0);

			guard.transform.position += move;
		}
	}
}
