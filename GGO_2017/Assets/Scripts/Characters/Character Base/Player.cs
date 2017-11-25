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
    public bool grapple;
    public bool pushing;
    public bool shoving;
    //Duration of shove
    public float airTime;
    public float extendAirTime;
    //Reference to start of shove coroutine, will allow us to keep track of coroutine activity
    public Coroutine shoveCoroutine = null;
    public Coroutine chargeCoroutine = null;
    public Coroutine extendShoveCoroutine = null;

    public bool coRunning;

    public Vector3 shoveDistance;

    void Awake() { }

    //EventHandler.onKick += this.OnCharacterKick;

    void Start()
    {
        pushing = false;
        player.GetComponent<Rigidbody2D>().freezeRotation = true;
        enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
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
            //enemy.transform.position += move;

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
        //Debug.Log("grapple = " + grapple);    <== works

        //Debug.Log("pushing = " + pushing);
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.transform.SetParent(player.transform);
            grapple = true;

            //Debug.Log("we on it");            <== works
        }

        //Debug.Log("grapple = " + grapple);     <== works
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
        shoving = true;
        grapple = false;
        float elapsedTime = 0f;

        while (elapsedTime < airTime)
        {
            Vector3 startPos = enemy.transform.position;
            var lerpVal = (elapsedTime / airTime);
            enemy.transform.position = Vector3.Lerp(startPos, toPos, lerpVal);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //Debug.Log("We shoved. Grapple: " + grapple);

        shoveCoroutine = null;
        shoving = false;
        coRunning = false;
    }
    public IEnumerator CoSExtend(Vector3 toPos, float airTime)
    {
        coRunning = true;
        
        //Wait until shove is finished, then continue with rest of enum.
        // yield return new WaitUntil(() => !shoving);

        float elapsedTime = 0f;
        Debug.Log("Extending shove...");

        while (elapsedTime < airTime)
        {
            Vector3 startPos = enemy.transform.position;
            var lerpVal = (elapsedTime / airTime);
            enemy.transform.position = Vector3.Lerp(startPos, toPos, lerpVal);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Finished coroutine.");
        coRunning = false;
        extendShoveCoroutine = null;
    }
    public Vector3 getPlayerLoc() { return player.transform.position; }

    public bool getGrapple() { return grapple; }
}
