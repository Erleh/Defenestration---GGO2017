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

    //Reference to start of shove coroutine, will allow us to keep track of coroutine activity
    public Coroutine shoveCoroutine = null;
    public Coroutine chargeCoroutine = null;
    public Coroutine extendCoroutine = null;
    public Coroutine kickCoroutine = null;

    public bool coRunning;

    public Vector3 shoveDistance;
    public Vector3 extension;

    //Kick arc vars
    public float kDist;
    public float frequency;
    public float amp;

    //Quick hack for extension distance
    public float extendFreq;
    //public bool onCeiling;
    private Vector3 beginPos;
    private bool pStart;
    void Awake()
    {
    }

    //EventHandler.onKick += this.OnCharacterKick;
    void Start()
    {
        pushing = false;
        pStart = true;
        player.GetComponent<Rigidbody2D>().freezeRotation = true;
        enemy.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    public void Push()
    {
		if (grapple)
        {
            float pushBack = speed / 2;
            Vector3 move = new Vector3(pushBack, 0f, 0f);
            player.transform.position += move;
            fc.AddFatigue(PushFatigue);
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
            kickCoroutine = StartCoroutine(CoKick(enemy.transform.position.x+kDist, StrOfKick));
            fc.AddFatigue(KickFatigue);
        }
    }

    //When player collides with enemy, make enemy a child object to the player
    //Turns grapple to true
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.transform.SetParent(player.transform);
            grapple = true;
        }
    }

    //If not grappling enemy, charge at the enemy
    public IEnumerator ChargeAtEnemy()
    {
        coRunning = true;
        //charging is used to trigger animation
        charging = true;
        player.transform.position = Vector3.MoveTowards(player.transform.position, enemy.transform.position, dashSpeed * Time.deltaTime);
        yield return new WaitUntil(() => grapple);
        pStart = false;
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
        grapple = false;
        
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
        float step = extendStr * Time.deltaTime;
        //Debug.Log(ceiling);
        if(!c)
        {
            while (enemy.transform.position != toPos)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, toPos, step);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            float index = 0;
            Vector3 CeilStart = enemy.transform.position;
            while (enemy.transform.position.x != toPos.x || enemy.transform.position.y > beginPos.y)
            {
                Vector3 trackPos = enemy.transform.position;
                index += Time.deltaTime;
                float x = Mathf.MoveTowards(trackPos.x, toPos.x, extendStr*Time.deltaTime);
                float y = Mathf.Clamp((CeilStart.y + amp * Mathf.Cos(extendFreq * extendStr * index)), beginPos.y, CeilStart.y);
                enemy.transform.position = new Vector3(x, y, beginPos.z);
                yield return new WaitForFixedUpdate();
            }
            Debug.Log("Kick extend goes here.");
        }
        kicking = false;
        shoving = false;
        coRunning = false;
        extendCoroutine = null;

        shoveCoroutine = null;
    }

    public IEnumerator CoKick(float targetX, float speed)
    {
        coRunning = true;
        float index = 0;
        kicking = true;
        grapple = false;
        beginPos = enemy.transform.position;
        while(enemy.transform.position.x != targetX || (enemy.transform.position.y != beginPos.y))
        {
            if (extend)
            {
                extendCoroutine = StartCoroutine(CoExtend(new Vector3(targetX, beginPos.y) + extension, ExtendStrength + StrOfKick));
                kickCoroutine = null;
                yield break;
            }
            Vector3 trackPos = enemy.transform.position;
            index += Time.deltaTime;
            float x = Mathf.MoveTowards(trackPos.x, targetX, speed*Time.deltaTime); //sends x position towards 
            float y = Mathf.Clamp((beginPos.y + amp * Mathf.Sin(frequency * speed * index)),beginPos.y, float.MaxValue); //unclamped max value, clamped lower value so there's no way the player can end up out of position
            enemy.transform.position = new Vector3(x ,y, beginPos.z);
            yield return new WaitForEndOfFrame();
        }
        kicking = false;
        kickCoroutine = null;
        coRunning = false;

    }
    public Vector3 getPlayerLoc() { return player.transform.position; }

    public bool getGrapple() { return grapple; }
    public bool getPushing() { return pushing; }
}
