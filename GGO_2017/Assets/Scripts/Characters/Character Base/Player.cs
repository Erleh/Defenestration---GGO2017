using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour //, IPlayable
{
	//Reference to player and enemy
	public GameObject player;
	public GameObject enemy;//Enemy enemy;

	public FatigueController fc;
  public float speed;

    //Fatigue
    public float PassiveFatigue { get; set; }
    public float PushFatigue { get; set; }
    public float ShoveFatigue { get; set; }
    public float KickFatigue { get; set; }
    public float StrOfShove { get; set; }

    //Init. for shoving movement
	private bool grapple;
    //private bool pushing = false;
    

	//Duration of shove
	public float airTime;

	//Reference to start of shove coroutine, will allow us to keep track of coroutine activity
	private Coroutine shoveCoroutine = null;

	public Vector3 shoveDistance; 

    void Awake() {}

		//EventHandler.onKick += this.OnCharacterKick;
	}

	public void OnDisable()
	{
		//Unsubscribe Events
		//Push.onPush -= this.OnCharacterPush;
		//Shove.onShove -= this.OnCharacterShove;
        //need kick as well
    }

	void Start () 
	{
		player.GetComponent<Rigidbody2D>().freezeRotation = true;
		enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
	}

	void Update()
    {
		Debug.Log("Player Update");
        if(enemy && !fc.loseGame)
        {
            if (grapple)
            {
                //if (Input.GetKey(KeyCode.Space))
                //{

               // }
				        if (Input.GetKeyDown(KeyCode.Z) && shoveCoroutine == null)
				        {
					        OnCharacterShove();
					        shoveCoroutine = StartCoroutine(CoShove(enemy.transform.position + shoveDistance, 1));
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    Push();
                    Debug.Log("Work it.");
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    Debug.Log("STOP RESISTING");
                    enemy.Resist();
                }
            }
            else
            {
                StartCoroutine(ChargeAtEnemy());
            }
			else
			{
				StartCoroutine(ChargeAtEnemy());
			}
        }
    }

	public void Push()
	{
		if(grapple)
		{
            //Debug.Log("We tryna push");
            Debug.Log(getGrapple());
            float pushBack = speed/2;

			Debug.Log("pushBack = " + pushBack);

			Vector3 move = new Vector3(pushBack, 0f, 0f);
			player.transform.position += move;
            //Debug.Log("We did it reddit");
		}
    
    /* Debug.Log("We tryna push");
        Debug.Log(getGrapple());

        //Player pushes Enemy back
        float pushBack = speed/2;
		Vector3 move = new Vector3(pushBack, 0f, 0f);
		player.transform.position += move;

        //Adds passive fatigue value to the fatigue bar
        fc.AddFatigue(PushFatigue);

        //Debug.Log("We did it reddit");
        */
	}

	//Coroutine to move enemy distance of the shove
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

	public void OnCharacterShove()
	{
        //If !grapple do not do event, if grapple do action
        if (grapple)
        {
            //Detach child from player
            enemy.transform.SetParent(null);
          
            //fc.AddFatigue(ShoveFatigue);
            //Play Shove Animation
        }
	}

	public void Kick(Enemy e)
	{
        fc.AddFatigue(KickFatigue);
	}

	//When player collides with enemy, make enemy a child object to the player
	//Turns grapple to true
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Enemy"))
		{
			col.gameObject.transform.parent = player.transform;

			grapple = true;
           // Debug.Log("we on it");
		}
	}
    //If not grappling enemy, charge at the enemy
    public IEnumerator ChargeAtEnemy()
    {
        //Debug.Log("Charging at Enemy...");
        Vector3 move = new Vector3(speed, 0, 0);
        //playerLocation += move;
        player.transform.position += move;
        yield return new WaitUntil(() => getGrapple());
        //Debug.Log("Charged with speed: " + speed);
    }

    public Vector3 getPlayerLoc() { return player.transform.position; }

    public bool getGrapple(){return grapple;}
}
