using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour, IPlayable
{
	public GameObject player;
	public float speed = -.2f;

	private Vector3 playerLocation;
	private bool grapple = false;
	private bool pushing = false;

	//Placeholder for strength of shove
	public float strOfShove = -2f;
	//Placeholder
	public float passiveFatigue = 0f;

	void Awake()
	{
		playerLocation = player.transform.position;
	}

	public void OnEnable()
	{
		//Subscribe Events
		//EventHandler.onPush += this.OnCharacterPush;
		Push.onPush += this.OnCharacterPush;

		Shove.onShove += this.OnCharacterShove;

		EventHandler.onKick += this.OnCharacterKick;

		//Shove getShove = GetComponent<Shove>();

		//getShove.onShove += this.OnCharacterShove;
		//GetComponent<Shove>().onShove += this.OnCharacterShove;
	}

	public void OnDisable()
	{
		//Unsubscribe Events
		Push.onPush -= this.OnCharacterPush;

		Shove.onShove -= this.OnCharacterShove;
	}

	void Start () 
	{

	}

	void Update()
	{
	}

	public void OnCharacterPush(GameObject character)
	{
		if(grapple)
		{
			float pushBack = speed/2;
			Vector3 move = new Vector3(pushBack, 0f, 0f);
			player.transform.position += move;
		}
	}

	public void OnCharacterShove(GameObject character)
	{
		//TO ADD: add change in fatigue here

		//If !grapple do not do event, if grapple do action

	}

	public void OnCharacterKick(GameObject character)
	{
		
	}

	//When player collides with enemy, make enemy a child object to the player
	//Turns grapple to true
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Enemy"))
		{
			col.gameObject.transform.parent = player.transform;

			grapple = true;
		}
	}
		
	//public void movePlayer(Vector3 distance)
	//{

	//}

	public bool getGrapple()
	{
		return grapple;
	}
}
