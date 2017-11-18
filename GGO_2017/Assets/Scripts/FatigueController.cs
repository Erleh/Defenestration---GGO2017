using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class FatigueController : MonoBehaviour {

    // Use this for initialization
    public FatigueBarController fatigueBar;

    public Slider fSlider;
    public Player pChar;
    private Shove charShove;
    private Push charPush;
    private Kick charKick;
    private Use charUse;
    // Update is called once per frame
    private void Start()
    {

        charShove = GetComponent<Shove>();
        charPush = GetComponent<Push>();
        charKick = GetComponent<Kick>();
        charUse = GetComponent<Use>();
    }
    void FixedUpdate () {
        /*needs to update based on player fatigue values*/
        //increases passive fatigue using defined player character value...
        if (pChar.getGrapple()) 
            fatigueBar.fatigue += pChar.passiveFatigue;

        //disabling player ability to perform actions on full fatigue...
        if (fatigueBar.fatigue <= 0){ 
            charShove.enabled = !charShove.enabled;
            charPush.enabled = !charPush.enabled;
            charKick.enabled = !charKick.enabled;
            charUse.enabled = !charUse.enabled;
            Debug.Log("FATIGUED");
        }

    }
}
