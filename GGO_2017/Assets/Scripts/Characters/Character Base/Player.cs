using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour //, IPlayable
{
	public GameObject player;
    public Enemy enemy;
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
    


    void Awake() {}

	void Start () {}

	void Update()
    {
        if(enemy && !fc.loseGame)
        {
            if (grapple)
            {
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
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Shove(enemy);
                }

            }
            else
            {
                StartCoroutine(ChargeAtEnemy());
            }
        }
    }

	public void Push()
	{
        Debug.Log("We tryna push");
        Debug.Log(getGrapple());

        //Player pushes Enemy back
        float pushBack = speed/2;
		Vector3 move = new Vector3(pushBack, 0f, 0f);
		player.transform.position += move;

        //Adds passive fatigue value to the fatigue bar
        fc.AddFatigue(PushFatigue);

        //Debug.Log("We did it reddit");
	}

	public void Shove(Enemy e)
	{
        //Detach child from player
        e.transform.SetParent(null);

        //Start Coroutine for Shove movement
        fc.AddFatigue(ShoveFatigue);
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
