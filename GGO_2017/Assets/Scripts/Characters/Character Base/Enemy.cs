using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IPlayable {

	public GameObject enemy;

	private bool grapple = false;
	private bool shove = false;
	private bool kick = false;

	public void OnEnable()
	{
		EventHandler.onPush += this.OnCharacterPush;
		//EventHandler.onShove += this.OnCharacterShove;
		EventHandler.onKick += this.OnCharacterKick;
	}

	public void OnDisable()
	{
		EventHandler.onPush -= this.OnCharacterPush;
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
	}

	public void OnCharacterPush(GameObject character)
	{
		//TO ADD: play animation
	}

	public void OnCharacterShove(GameObject character)
	{
		//TO ADD: play animation

		if(grapple)
		{
			shove = true;

			grapple = false;
		}
	}

	public void OnCharacterKick(GameObject character)
	{

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			grapple = true;
		}
	}
}
