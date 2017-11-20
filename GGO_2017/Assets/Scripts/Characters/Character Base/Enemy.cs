using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IPlayable {

	public GameObject enemy;

	public float speed;

	private bool grapple = false;
	private bool shove = false;
	private bool kick = false;
	private bool resisting = false;

	public void OnEnable()
	{
		//Subscribes events on script enable
		EventHandler.onKick += this.OnCharacterKick;
		Push.onPush += this.OnCharacterPush;
		Push.resist += this.EnemyResist;
	}

	public void OnDisable()
	{
		//Disable events on script disable
		Push.onPush -= this.OnCharacterPush;
		Push.resist -= this.EnemyResist;
		EventHandler.onKick -= this.OnCharacterKick;
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		//If enemy is resisting the push, push back
		if(resisting)
		{
			float pushBack = speed/8;
			Vector3 move = new Vector3(pushBack, 0f, 0f);

			//Debug.Log("move: " + move);
			enemy.transform.parent.position += move;
		}
	}

	//Method to subscribe to push event
	public void OnCharacterPush(GameObject character)
	{
		//TO ADD: play animation
		if(grapple)
		{
			//Debug.Log("onPush");
			resisting = false;
		}
	}

	//Method to subscribe to shove event
	public void OnCharacterShove(GameObject character)
	{
		//TO ADD: play animation

		//If !grapple do not do event, else do action
		if(grapple)
		{
			shove = true;

			grapple = false;
		}
	}

	public void OnCharacterKick(GameObject character)
	{

	}

	//Subscribe to the resist event, will cause gameobject to push against player
	public void EnemyResist(GameObject character)
	{
		if(grapple)
		{
			//Debug.Log("TEST");
			resisting = true;
		}
	}

	//When objects collide, and the object is the player, grappling is true and begin resisting
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			grapple = true;
			resisting = true;
		}
	}
}
