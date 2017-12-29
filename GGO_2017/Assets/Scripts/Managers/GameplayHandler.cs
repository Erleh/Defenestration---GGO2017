using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayHandler : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public FatigueController fc;
    private Coroutine delayCoroutine;

    private Player p;
    public bool win;
    public bool lose;

    private void Awake(){
        p = player.GetComponent<Player>();

    }
    void Start () { 	
	}
	
    //gameloop
	void FixedUpdate () {
        p.anim.SetBool("ChargeAt", p.charging);
        if (enemy && !fc.loseGame && !win)
        {
            //If player is grappling enemy and no coroutines are running
            if (p.grapple && !p.coRunning)
            {
                //can't shove if already shoving
                if (Input.GetKeyDown(KeyCode.Z) && p.shoveCoroutine == null && p.extendCoroutine == null)
                {
                    p.pushing = false;
                    p.Shove();
                    delayCoroutine = StartCoroutine(CoDelay());
                }

                if (Input.GetKeyDown(KeyCode.X) && p.kickCoroutine == null)
                {
                    p.pushing = false;
                    p.Kick();
                    delayCoroutine = StartCoroutine(CoDelay());
                }

                //can't push when charging back at the enemy
                if (Input.GetKey(KeyCode.Space) && p.chargeCoroutine == null)
                {
                    p.pushing = true;
                    p.Push();
                }
                //sets variable to false so enemy can continue resisting in their update
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    p.pushing = false;
                }
            }
            else
            {
               p.pushing = false;
                //kicking = false;

                //waits for full  shove lerp to play before charging back at enemy
                //debug test statement:
                // if(extendShoveCoroutine != null) { Debug.Log("Extend shove still running..."); }

                if (p.shoveCoroutine == null && p.extendCoroutine == null && p.kickCoroutine == null && delayCoroutine == null)
                {
                    p.chargeCoroutine = StartCoroutine(p.ChargeAtEnemy());
                }
                p.anim.SetBool("Grapple", p.grapple);
            }
        }
        else if (p.win)
        {
            //Debug.Log("win = " + win);
        }
        else
        {
            p.pushing = false;
            //Lose Game here
            //Debug.Log("Lost the game. Fatigued.");
        }
    }
    public IEnumerator CoDelay()
    {
        Debug.Log("Delaying...");
        yield return new WaitForSeconds(1f);
        delayCoroutine = null;
    }
    public void Win()
    {
        win = true;
    }

    public void Lose()
    {
        enemy.transform.SetParent(null);
        lose = true;
    }
}
