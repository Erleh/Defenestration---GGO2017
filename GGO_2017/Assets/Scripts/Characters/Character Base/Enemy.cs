using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IPlayable 
{
	public GameObject enemy;
    public GameObject player;

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

	public void OnEnable()
	{
        //Subscribes events on script enable
        //EventHandler.onKick += this.OnCharacterKick;
		//Push.onPush += this.OnCharacterPush;
		//Push.resist += this.EnemyResist;
        //Shove.onShove += this.OnCharacterShove;
	}

	public void OnDisable()
	{
		//Disable events on script disable
		//Push.onPush -= this.OnCharacterPush;
		//Push.resist -= this.EnemyResist;
        //Shove.onShove -= this.OnCharacterShove;
		//EventHandler.onKick -= this.OnCharacterKick;
	}

	// Update is called once per frame
	void Update () 
	{
		Debug.Log("Enemy Update");
		//If enemy is resisting the push, push back
		if(resisting)
		{
			float pushBack = speed/8;
			Vector3 move = new Vector3(pushBack, 0f, 0f);

			Debug.Log("resist: " + move);
			enemy.transform.parent.position += move;
		}
	}

	//Method to subscribe to push event
	public void OnCharacterPush()
	{
		//TO ADD: play animation
		if(grapple)
		{
			Debug.Log("onPush");
			resisting = false;
		}
	}
		
	//Method to subscribe to shove event
	public void OnCharacterShove()
	{
        //TO ADD: play animation
        // Debug.Log("We tryna shove");
        //newShoveLoc = enemy.transform.position + shoveDist;
        //Debug.Log(enemy.transform.position + ":" + newShoveLoc);

        //shoveCoroutine = StartCoroutine(CoShove(newShoveLoc, shoveAir));
        //Debug.Log("We did it reddit");
	}

	public void OnCharacterKick()
	{

	}

	//Subscribe to the resist event, will cause gameobject to push against player
	public void EnemyResist()
	{
		if(grapple)
		{
			Debug.Log("Resisting");
			resisting = true;
		}
	}

	/*
    public IEnumerator CoShove(Vector3 toPos, float airTime)
    {
        float elapsedTime = 0f;
        Debug.Log("We tryna shove");
        while (elapsedTime < airTime)
        {
            Vector3 startPos = enemy.transform.position;

            //Debug.Log("startPos = " + startPos);
            //Debug.Log("toPos = " + toPos);
            var lerpVal = (elapsedTime / airTime);// * pChar.StrOfShove
           // Debug.Log("(elapsedTime/airTime) * pChar.StrOfShove = " + lerpVal);

            enemy.transform.position = Vector3.Lerp(startPos, toPos, lerpVal);

            Debug.Log("enemy trans: " + enemy.transform.position);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("We shoved. Grapple: " + grapple); 
        grapple = false;
        shoveCoroutine = null;
    }
    */

	//When objects collide, and the object is the player, grappling is true and begin resisting
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
            Debug.Log("Collision");
			grapple = true;
			resisting = true;
		}
	}

    public Vector3 getEnemyLoc() { return enemy.transform.position; }
}
