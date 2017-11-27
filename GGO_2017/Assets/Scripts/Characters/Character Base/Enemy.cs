﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour//, IPlayable 
{
	public GameObject enemy;
    public GameObject player;
    public Player p;
	public float speed;

	public bool grapple;

	private bool shove;
	//private bool kick = false;
	public bool resisting;
    //public Vector3 shovedTo;


    private float startTime;
    public Vector3 shoveDist;
    public Vector3 newShoveLoc;
    public float shoveAir;

    private Coroutine shoveCoroutine = null;

    void Start()
    {
    }

	public void Resist()
	{
        //TO ADD: play animation

        //Debug.Log("resist");   <= triggers correctly

		if(grapple)
		{
            float pushBack = speed;
            Vector3 move = new Vector3(pushBack, 0f, 0f);

            //Debug.Log("resist: " + move);
            //Debug.Log("enemy transform : " + enemy.transform);

            //enemy.transform.position += move;
            player.transform.position += move;

            //Debug.Log("onPush");
			resisting = true;
		}
	}

	//When objects collide, and the object is the player, grappling is true and begin resisting
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
            //Debug.Log("Collision");    <= works
			grapple = true;
		}
	}

    public Vector3 getEnemyLoc() { return enemy.transform.position; }
}
