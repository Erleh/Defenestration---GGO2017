using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class FatigueController : MonoBehaviour {

    // Use this for initialization
    public FatigueBarController fatigueBar;

    public Slider fSlider;
    public Player pChar;


    // Update is called once per frame
    public void OnEnable(){
        //Subscribe Events
        Push.onPush += this.OnCharacterPush;
        Shove.onShove += this.OnCharacterShove;
        EventHandler.onKick += this.OnCharacterKick;
    }
    public void OnDisable(){
        Push.onPush -= this.OnCharacterPush;
        Shove.onShove -= this.OnCharacterShove;
        EventHandler.onKick -= this.OnCharacterKick;
    }
    void FixedUpdate () {
        //increases passive fatigue using defined player character value...
        if (pChar.getGrapple()){
            //Increments passive fatigue by character PF value over time each second
            fatigueBar.fatigue += pChar.PassiveFatigue;
            //Debug.Log("Passive Fatigue Value: " + pChar.PassiveFatigue*Time.deltaTime);
        }
        //disabling player ability to perform actions on full fatigue...
        if (fatigueBar.fatigue >= 100){
            Debug.Log("FATIGUED");
        }
    }
    //Method subscribing to Push event
    public void OnCharacterPush(GameObject character){
        fatigueBar.fatigue += pChar.PushFatigue;
        Debug.Log("Current Fatigue: " + fatigueBar.fatigue);
    }
    //need values for fatiguing
    public void OnCharacterShove(GameObject character) { }
    public void OnCharacterKick(GameObject character) { }
}
