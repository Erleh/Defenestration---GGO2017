using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class FatigueController : MonoBehaviour {

    // Use this for initialization
    public FatigueBarController fatigueBar;

    public Slider fSlider;
    public Player pChar;
    public bool loseGame;

    void FixedUpdate () {
        //increases passive fatigue using defined player character value...
        if (pChar.getGrapple()){
            //Increments passive fatigue by character PF value over time each second
            fatigueBar.fatigue += pChar.PassiveFatigue;
            //Debug.Log("Passive Fatigue Value: " + pChar.PassiveFatigue*Time.deltaTime);
        }
        //disabling player ability to perform actions on full fatigue...
        if (fatigueBar.fatigue >= fatigueBar.maxFatigue){
            loseGame = true;
            Debug.Log("FATIGUED");
        }
    }
    //Method subscribing to Push event
    public void AddFatigue(float amt){
        fatigueBar.fatigue += amt;
        //Debug.Log("Current Fatigue: " + fatigueBar.fatigue);
    }
}
