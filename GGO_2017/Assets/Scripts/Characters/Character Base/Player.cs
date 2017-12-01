using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour //, IPlayable
{
    //Reference to player and enemy
    public GameObject player;
    public GameObject enemy;//Enemy enemy;
    public GameObject obstacle;
    public FatigueController fc;
    public float speed;
	public float dashSpeed;
    public float maxHeightOnKick;

    //Action fatigue and strength
    public float PassiveFatigue { get; set; }
    public float PushFatigue { get; set; }
    public float ShoveFatigue { get; set; }
    public float KickFatigue { get; set; }
    public float StrOfKick { get; set; }
    public float StrOfShove { get; set; }
    public float ExtendStrength { get; set; }
    //Init. for shoving movement
    public bool grapple;
    public bool pushing;

    //Bools for states are being used to trigger animations
    public bool charging;
    public bool kicking;
    public bool shoving;
    public bool extend;
    public bool c;

    //win condition
    public bool win;
	public bool lose;

    //Duration of shove
    public float airTime;
    public float extendAirTime;
    //Reference to start of shove coroutine, will allow us to keep track of coroutine activity
    public Coroutine shoveCoroutine = null;
    public Coroutine chargeCoroutine = null;
    public Coroutine extendCoroutine = null;
    public Coroutine kickCoroutine = null;

    public bool coRunning;

    public Vector3 shoveDistance;
    public Vector3 kickDistance;
    public Vector3 extension;
    private float height;
    //Quick hack for extension distance
    //public Vector3 extendDist;
    //public bool onCeiling;

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
            shoveCoroutine = StartCoroutine(CoShove(enemy.transform.position + shoveDistance, StrOfShove));
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
            kickCoroutine = StartCoroutine(CoKick(enemy.transform.position + kickDistance, maxHeightOnKick, StrOfKick));
            fc.AddFatigue(KickFatigue);
            //Play Shove Animation
        }
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

        //charging is used to trigger animation
        charging = true;

        //Debug.Log("Charging at Enemy...");

        Vector3 move = new Vector3(dashSpeed, 0, 0);

        //playerLocation += move;

        player.transform.position += move;
        yield return new WaitUntil(() => getGrapple());

        //Debug.Log("Charged with speed: " + speed);
        extend = false;
        chargeCoroutine = null;

        //charging = false to end animation
        charging = false;

        coRunning = false;
    }

    //Coroutine to move enemy distance of the shove
    public IEnumerator CoShove(Vector3 toPos, float shoveStr)
    {
        coRunning = true;

        //shoving is used to trigger animation
        shoving = true;
        //Debug.Log("Shoving = " + shoving);
        //Debug.Log("We tryna shove");
        grapple = false;

        //float elapsedTime = 0f;
        
        float step = shoveStr * Time.deltaTime;

        while (enemy.transform.position != toPos)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, toPos, step);
            yield return new WaitForFixedUpdate();
        }

        if (extend)
        {
            extendCoroutine = StartCoroutine(CoExtend(toPos + extension, ExtendStrength + StrOfShove));
        }
        else
        {
            shoveCoroutine = null;
            shoving = false;
            coRunning = false;
        }
    }
    
    public IEnumerator CoExtend(Vector3 toPos, float extendStr)
    {
        coRunning = true;

        //Wait until shove is finished, then continue with rest of enum.
        // yield return new WaitUntil(() => !shoving);

        //float elapsedTime = 0f;
        //Debug.Log("Extending shove...");
        float step = extendStr * Time.deltaTime;
        //Debug.Log(ceiling);
        if(!c)
        {
            while (enemy.transform.position != toPos)
            {
                //Debug.Log("From: " + enemy.transform.position + "\t" + "To: " + toPos);
                //Debug.Log(StrOfShove+"  "+extendStr);
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, toPos, step);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            height = enemy.transform.position.y;
            while (enemy.transform.position != toPos)
            {
                //Debug.Log("Transforming... " + enemy.transform.position + ":" + toPos);
                enemy.transform.position = ArcingVector(enemy.transform.position, toPos, extendStr, .1f);
                yield return new WaitForFixedUpdate();
            }
            //Debug.Log("Kick extend goes here.");
        }
        kicking = false;
        shoving = false;
        coRunning = false;
        extendCoroutine = null;

        shoveCoroutine = null;
    }

    public IEnumerator CoKick(Vector3 toPos, float maxHeight, float kickStrength)
    {
        coRunning = true;
        
        //Kicking is used to trigger animation
        kicking = true;
        grapple = false;

        height = enemy.transform.position.y;
        //var counter = 0;
        //approx. max height
        //Vector3 vertex = new Vector3((toPos.x - enemy.transform.position.x) / 2, maxHeight); 
        while(enemy.transform.position != toPos)
        {
            //Debug.Log(SendInArc(enemy.transform.position, toPos, mH, maxHeight, kickStrength, nextX, baseY, arc));
            enemy.transform.position = ArcingVector(enemy.transform.position, toPos, kickStrength, maxHeight);
            if(extend)
            {
                extendCoroutine = StartCoroutine(CoExtend(toPos + extension, ExtendStrength+kickStrength));
                kickCoroutine = null;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
 
        //enemy.transform.position = new Vector3(enemy.transform.position.x, ogHeight, 0);

        //Debug.Log("We Kicked. Grapple: " + grapple);

        //Kicking ends animation
        kicking = false;

        kickCoroutine = null;
        coRunning = false;

        //return null;
    }
    public Vector3 ArcingVector(Vector3 startPos, Vector3 endPos, float arcSpeed, float arcHeight)
    {
        float x0 = startPos.x;
        float x1 = endPos.x;
        float xDist = x1 - x0;
        float nextX = Mathf.MoveTowards(startPos.x, x1, arcSpeed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, endPos.y, (nextX - x0) / (xDist));
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * (xDist) * (xDist));
        //Debug.Log(arc);
        return new Vector3(nextX, baseY + arc, startPos.z);
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
