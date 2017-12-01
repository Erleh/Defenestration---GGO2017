using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class FatigueController : MonoBehaviour {

    // Use this for initialization
    public FatigueBarController fatigueBar;

    public Slider fSlider;
    public GameObject pChar;
    public bool loseGame;
    private Guard g;
    private void Awake()
    {
        g = pChar.GetComponent<Guard>();
    }
    void FixedUpdate () {
        //increases passive fatigue using defined player character value...
		if (g.getGrapple() && !g.win && !g.lose){
            //Increments passive fatigue by character PF value over time each second
			fatigueBar.fatigue += pChar.GetComponent<Guard>().PassiveFatigue;
            //Debug.Log("Passive Fatigue Value: " + pChar.PassiveFatigue*Time.deltaTime);
        }
        //disabling player ability to perform actions on full fatigue...
        if (fatigueBar.fatigue >= fatigueBar.maxFatigue){
            loseGame = true;
			pChar.GetComponent<Guard>().Lose();
            //Debug.Log("FATIGUED");
        }
    }
    //Method subscribing to Push event
    public void AddFatigue(float amt){
        if (!g.win && !g.lose)
        {
            fatigueBar.fatigue += amt;
        }
        //Debug.Log("Current Fatigue: " + fatigueBar.fatigue);
    }
}
