using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour, IPlayable
{
	public GameObject player;


	public float speed = -.2f;

    //Fatigue
    public float PassiveFatigue { get; set; }
    public float PushFatigue { get; set; }
    public float ShoveFatigue { get; set; }
    public float KickFatigue { get; set; }
    public float StrOfShove { get; set; }

    //Init. for shoving movement
    public Vector3 shoveDist;
    public float lerpMov;

    public Vector3 playerLocation;
	private bool grapple = false;
	//private bool pushing = false;



	void Awake(){playerLocation = player.transform.position;}

	public void OnEnable()
	{
		//Subscribe Events
		Push.onPush += this.OnCharacterPush;

		Shove.onShove += this.OnCharacterShove;

		EventHandler.onKick += this.OnCharacterKick;
	}

	public void OnDisable()
	{
		//Unsubscribe Events
		Push.onPush -= this.OnCharacterPush;
		Shove.onShove -= this.OnCharacterShove;
        //need kick as well
    }

	void Start () 
	{
	}

	void Update()
	{
	}

	//Method to subscribe to the push event
	public void OnCharacterPush(GameObject character)
	{
		if(grapple)
		{
            Debug.Log("We tryna push");
			float pushBack = speed/2;
			Vector3 move = new Vector3(pushBack, 0f, 0f);
			player.transform.position += move;
            Debug.Log("We did it reddit");
		}
	}

	public void OnCharacterShove(GameObject character)
	{
        //If !grapple do not do event, if grapple do action
        if (grapple)
        {
            grapple = false;
            Debug.Log("We tryna shove");
            Vector3 shoved = Vector3.MoveTowards(playerLocation, playerLocation+shoveDist, StrOfShove*lerpMov);
            player.transform.position += shoved;
            Debug.Log("We did it reddit");
        }
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

    //If not grappling enemy, charge at the enemy
    public void ChargeAtEnemy()
    {
        if (!getGrapple())
        {
            //Debug.Log("Charging at Enemy...");
            Vector3 move = new Vector3(speed, 0, 0);
            Debug.Log(move);
            //playerLocation += move;
            player.transform.position += move;
            //Debug.Log("Charged with speed: " + speed);
        }
    }
    public bool getGrapple(){return grapple;}
}
