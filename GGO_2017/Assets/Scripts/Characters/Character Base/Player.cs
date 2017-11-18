using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour, IPlayable
{
	public GameObject player;
	public float speed = -.2f;

	private Vector3 playerLocation;
	private bool grapple = false;

	//Placeholder for strength of shove
	public float strOfShove = -2f;
	//Placeholder
	public float fatigue = 0f;
	public float maxFatigue = 100f;

	void Awake()
	{
		playerLocation = player.transform.position;
	}

	public void OnEnable()
	{
		//Subscribe Events
		//EventHandler.onPush += this.OnCharacterPush;

		Shove.onShove += this.OnCharacterShove;

		EventHandler.onKick += this.OnCharacterKick;

		//Shove getShove = GetComponent<Shove>();

		//getShove.onShove += this.OnCharacterShove;
		//GetComponent<Shove>().onShove += this.OnCharacterShove;
	}

	public void OnDisable()
	{
		//Unsubscribe Events
		EventHandler.onPush -= this.OnCharacterPush; 
	}

	void Start () 
	{

	}

	public void OnCharacterPush(GameObject character)
	{
		//TO ADD: add change in fatigue here

		if(grapple && fatigue != maxFatigue)
		{
			float pushBack = speed/2;
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

	public void OnCharacterShove(GameObject character)
	{
		//TO ADD: add change in fatigue here

		if(grapple && fatigue != 100)
		{
		}
	}

	public void OnCharacterKick(GameObject character)
	{
		
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
