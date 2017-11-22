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
    public bool pushing;


    //Duration of shove
    public float airTime;

    //Reference to start of shove coroutine, will allow us to keep track of coroutine activity
    private Coroutine shoveCoroutine = null;
    private Coroutine chargeCoroutine = null;
    private bool coRunning;
    public Vector3 shoveDistance;

    void Awake() { }

    //EventHandler.onKick += this.OnCharacterKick;

    void Start()
    {
        pushing = false;
        player.GetComponent<Rigidbody2D>().freezeRotation = true;
        enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    void Update()
    {
        //Debug.Log(pushing);
        //Debug.Log(coRunning);
        //Debug.Log("Player Update");
        if (enemy && !fc.loseGame)
        {
            if (grapple && !coRunning) //If player is grappling enemy and no coroutines are running
            {
                //if (Input.GetKey(KeyCode.Space))
                //{

                // }
                if (Input.GetKeyDown(KeyCode.Z) && shoveCoroutine == null) //can't shove if already shoving
                {
                    Shove();
                }
                if (Input.GetKey(KeyCode.Space) && chargeCoroutine == null) //can't push when charging back at the enemy
                {
                    pushing = true;
                    Push();
                    //Debug.Log("Work it.");
                }
                if (Input.GetKeyUp(KeyCode.Space)) //sets variable to false so enemy can continue resisting in their update
                {
                    pushing = false;
                }
            }
			else
			{
                if (shoveCoroutine == null) //waits for full shove lerp to play before charging back at enemy
                    chargeCoroutine = StartCoroutine(ChargeAtEnemy());
            }
        }
    }

    public void Push()
    {
        if (grapple)
        {
            //Debug.Log("We tryna push");
            //Debug.Log(getGrapple());
            float pushBack = speed / 2;
            //Debug.Log("pushBack = " + pushBack);
            Vector3 move = new Vector3(pushBack, 0f, 0f);
            player.transform.position += move;
            fc.AddFatigue(PushFatigue);
            //Debug.Log("We did it reddit");
        }
    }

    public void Shove()
    {
        //If !grapple do not do event, if grapple do action
        if (grapple)
        {
            //Detach child from player
            shoveCoroutine = StartCoroutine(CoShove(enemy.transform.position + shoveDistance, 1));
            enemy.transform.SetParent(null);
            fc.AddFatigue(ShoveFatigue);
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
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.transform.parent = player.transform;
            grapple = true;
            // Debug.Log("we on it");
        }
    }
    //If not grappling enemy, charge at the enemy
    public IEnumerator ChargeAtEnemy()
    {
        coRunning = true;
        //Debug.Log("Charging at Enemy...");
        Vector3 move = new Vector3(speed, 0, 0);
        //playerLocation += move;
        player.transform.position += move;
        yield return new WaitUntil(() => getGrapple());
        //Debug.Log("Charged with speed: " + speed);
        chargeCoroutine = null;
        coRunning = false;
    }
    //Coroutine to move enemy distance of the shove
    public IEnumerator CoShove(Vector3 toPos, float airTime)
    {
        coRunning = true;
        float elapsedTime = 0f;
        Debug.Log("We tryna shove");
        grapple = false;
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

        shoveCoroutine = null;
        coRunning = false;
    }

    public Vector3 getPlayerLoc() { return player.transform.position; }

    public bool getGrapple() { return grapple; }
}
