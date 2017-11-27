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

        Vector3 move = new Vector3(speed, 0, 0);

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
        while (enemy.transform.position!=toPos /*elapsedTime < airTime*/)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, toPos, step);
            yield return new WaitForFixedUpdate();
        }
        if (extend)
            extendCoroutine = StartCoroutine(CoExtend(toPos + extension, ExtendStrength));
        shoveCoroutine = null;
        shoving = false;
        coRunning = false;
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
            while(enemy.transform.position != toPos)
            //extend kick dist
            Debug.Log("Kick extend goes here.");
        }
        coRunning = false;
        extendCoroutine = null;
    }
    public IEnumerator CoKick(Vector3 toPos, float maxHeight, float kickStrength)
    {
        coRunning = true;
        
        //Kicking is used to trigger animation
        kicking = true;
        grapple = false;

        float ogHeight = enemy.transform.position.y;
        //var counter = 0;
        //approx. max height
        //Vector3 vertex = new Vector3((toPos.x - enemy.transform.position.x) / 2, maxHeight); 
        while(enemy.transform.position != toPos)
        {
            //Debug.Log(SendInArc(enemy.transform.position, toPos, mH, maxHeight, kickStrength, nextX, baseY, arc));
            enemy.transform.position = SendInArc(enemy.transform.position, toPos, ogHeight + 4.5f, maxHeight, kickStrength, 0f, 0f, 0f, (toPos.x-enemy.transform.position.x), enemy.transform.position.x, toPos.x);
            Debug.Log(kickStrength);
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
    public Vector3 SendInArc(Vector3 startPos, Vector3 endPos, float mH, float arcHeight, float arcSpeed, float nextX, float baseY, float arc, float xDist, float beginX, float finalX)
    {
        //Debug.Log("Kick Strength: " + kickStrength);
        if (startPos.y >= mH)
            arcSpeed += 3f;
        // Compute the next position, with arc added in
        /*MoveTowards x position while lerping y position*/
        Debug.Log(arcSpeed);
        //next x float is computed from this transform.position x -> final x position, step taken is kick strength multiplied by time
        nextX = Mathf.MoveTowards(startPos.x, endPos.x, arcSpeed * Time.deltaTime);

        //lerp current y to final y
        baseY = Mathf.Lerp(startPos.y, endPos.y, (nextX - beginX) / (xDist));

        //compute arc, never -actually- reaches this MaxHeight value, just uses it for calculating arc
        arc = arcHeight * (nextX - beginX) * (nextX - finalX) / (-0.25f * (xDist) * (xDist));

        Vector3 nextPos = new Vector3(nextX, baseY + arc, startPos.z);
        startPos = nextPos;
        return startPos;
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
