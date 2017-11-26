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
    public float maxHeightOnKick;

    //Fatigue
    public float PassiveFatigue { get; set; }
    public float PushFatigue { get; set; }
    public float ShoveFatigue { get; set; }
    public float KickFatigue { get; set; }
    public float StrOfShove { get; set; }

    //Init. for shoving movement
    public bool grapple;
    public bool pushing;

    //Bools for states are being used to trigger animations
    public bool charging;
    public bool kicking;
    public bool shoving;

    //Duration of shove
    public float airTime;

    //Reference to start of shove coroutine, will allow us to keep track of coroutine activity
    public Coroutine shoveCoroutine = null;
    public Coroutine chargeCoroutine = null;
    public Coroutine kickCoroutine = null;

    public bool coRunning;

    public Vector3 shoveDistance;
    public Vector3 kickDistance;

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
            enemy.transform.SetParent(null);
            shoveCoroutine = StartCoroutine(CoShove(enemy.transform.position + shoveDistance, 1));
            fc.AddFatigue(ShoveFatigue);
            //Play Shove Animation
        }
    }

    public void Kick()
    {
        //If !grapple do not do event, if grapple do action
        if (grapple)
        {
            //Detach child from player
            enemy.transform.SetParent(null);
            kickCoroutine = StartCoroutine(CoKick(enemy.transform.position + kickDistance, 1, maxHeightOnKick));
            fc.AddFatigue(KickFatigue);
            //Play Shove Animation
        }       
    }

    //When player collides with enemy, make enemy a child object to the player
    //Turns grapple to true
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("grapple = " + grapple);    <== works

        Debug.Log("pushing = " + pushing);
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

        //charging is used to trigger animation
        charging = true;

        //Debug.Log("Charging at Enemy...");

        Vector3 move = new Vector3(speed, 0, 0);

        //playerLocation += move;

        player.transform.position += move;
        yield return new WaitUntil(() => getGrapple());

        //Debug.Log("Charged with speed: " + speed);

        chargeCoroutine = null;

        //charging = false to end animation
        charging = false;

        coRunning = false;
    }

    //Coroutine to move enemy distance of the shove
    public IEnumerator CoShove(Vector3 toPos, float airTime)
    {
        coRunning = true;

        //shoving is used to trigger animation
        shoving = true;
        Debug.Log("Shoving = " + shoving);

        float elapsedTime = 0f;

        //Debug.Log("We tryna shove");

        grapple = false;

        while (elapsedTime < airTime)
        {
            Vector3 startPos = enemy.transform.position;

            //Debug.Log("startPos = " + startPos);
            //Debug.Log("toPos = " + toPos);

            var lerpVal = (elapsedTime / airTime);

            // Debug.Log("(elapsedTime/airTime) * pChar.StrOfShove = " + lerpVal);

            enemy.transform.position = Vector3.Lerp(startPos, toPos, lerpVal);

            //Debug.Log("enemy trans: " + enemy.transform.position);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("We shoved. Grapple: " + grapple);

        //shoving = false ends animation
        shoving = false;

        Debug.Log("shoving = " + shoving);

        shoveCoroutine = null;
        coRunning = false;
    }

    public IEnumerator CoKick(Vector3 endPos, float airTime, float maxHeight)
    {
        coRunning = true;

        //Kicking is used to trigger animation
        kicking = true;

        float elapsedTime = 0f;

        grapple = false;

        float ogHeight = enemy.transform.position.y;
        //var counter = 0;

        while (elapsedTime <= airTime)
        {
            Vector3 startPos = enemy.transform.position;

            //Debug.Log("startPos = " + startPos);
            //Debug.Log("toPos = " + toPos);

            var lerpVal = (elapsedTime / airTime);
            //Debug.Log("lerpVal = " + lerpVal);

            //Debug.Log("lerpVal = " + lerpVal);
            // Debug.Log("(elapsedTime/airTime) * pChar.StrOfShove = " + lerpVal);

            Vector3 enemyPos = Vector3.Lerp(startPos, endPos, lerpVal);

            //Debug.Log("Mathf.Clamp01(lerpVal) = " + Mathf.Clamp01(lerpVal));
            enemyPos.y += maxHeight * Mathf.Sin(lerpVal * Mathf.PI);

            enemy.transform.position = enemyPos;

            //Debug.Log("enemy trans: " + enemy.transform.position);

            elapsedTime += Time.deltaTime;
            //counter++;

            yield return new WaitForEndOfFrame();
        }
        enemy.transform.position = new Vector3(enemy.transform.position.x, ogHeight, 0);

        //Debug.Log("We Kicked. Grapple: " + grapple);

        //Kicking ends animation
        kicking = false;

        kickCoroutine = null;
        coRunning = false;

        //return null;
    }

    public Vector3 getPlayerLoc() { return player.transform.position; }

    public bool getGrapple() { return grapple; }
    public bool getPushing() { return pushing; }

    /*
    public bool getKicking() { return kicking; }
    public bool getIdle() { return idle; }
    public bool getGrappleIdle() { return grappleIdle; }
    public bool getCharging() { return charging; }
    */
}
