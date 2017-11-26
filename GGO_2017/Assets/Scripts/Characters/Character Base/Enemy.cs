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
	private bool resisting;
    //public Vector3 shovedTo;


    private float startTime;
    public Vector3 shoveDist;
    public Vector3 newShoveLoc;
    public float shoveAir;

    private Coroutine shoveCoroutine = null;

    void Start()
    {
        p = player.GetComponent<Player>();
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
			resisting = false;
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
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("GameObstacle"))
        {
            Debug.Log("Registered Obstacle Entrance");
            if (p.shoving || p.kicking)
            {
                GameObject o = col.gameObject;
                player.GetComponent<Player>().obstacle = o;
                p.extension = o.GetComponent<pObstacle>().extendDist;
                p.c = o.GetComponent<pObstacle>().onCeiling;
                p.extend = true;
               // Debug.Log(p.c);
                //Debug.Log(p.extension);
                //Debug.Log(extendDist);
                Destroy(col.gameObject);
            }
        }
    }
    public Vector3 getEnemyLoc() { return enemy.transform.position; }
}
