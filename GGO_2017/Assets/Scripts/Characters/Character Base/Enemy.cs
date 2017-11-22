using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour//, IPlayable 
{
	public GameObject enemy;
    public GameObject player;
    public Player p;
	public float speed;

	private bool grapple = false;

	private bool shove = false;
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
    }

	void Update () 
	{
        if(!p.pushing)
        {
            Debug.Log("Resisting..");
            Resist();
        }
	}
	public void Resist()
	{
		//TO ADD: play animation
		if(grapple)
		{
            float pushBack = speed / 8;
            Vector3 move = new Vector3(pushBack, 0f, 0f);

            Debug.Log("resist: " + move);
            enemy.transform.parent.position += move;
            Debug.Log("onPush");
			resisting = false;
		}
	}

	//When objects collide, and the object is the player, grappling is true and begin resisting
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
            Debug.Log("Collision");
			grapple = true;
		}
	}

    public Vector3 getEnemyLoc() { return enemy.transform.position; }
}
