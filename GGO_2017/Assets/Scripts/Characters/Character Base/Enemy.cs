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
		//EventHandler.onPush += this.OnCharacterPush;
		//EventHandler.onShove += this.OnCharacterShove;
		EventHandler.onKick += this.OnCharacterKick;

		Push.resist += this.EnemyResist;
	}

	public void OnDisable()
	{
		//EventHandler.onPush -= this.OnCharacterPush;
		//EventHandler.onShove -= this.OnCharacterShove;
		EventHandler.onKick -= this.OnCharacterKick;
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		//if(shove);
		/*
		if(enemy.transform.parent != false)
		{
			float pushBack = speed/2;
			Vector3 move = new Vector3(pushBack, 0f, 0f);
			enemy.transform.parent.position += move;
		}
		*/

		//if(resisting)
		//{
			Vector3 move = new Vector3(speed, 0f, 0f);

			Debug.Log(move);
			enemy.transform.parent.position -= move;
		//}
	}

	public void OnCharacterPush(GameObject character)
	{
		//TO ADD: play animation
		if(grapple)
		{
			resisting = false;
		}
	}

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


	public void EnemyResist(GameObject character)
	{
		if(grapple)
		{
			Debug.Log("TEST");
			resisting = true;
		}
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			grapple = true;
		}
	}
}
