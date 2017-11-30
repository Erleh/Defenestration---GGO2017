using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pAction : MonoBehaviour {
    public float fVal; //Fatigue value

    public pAction(){
        fVal = 1; //Default fatigue value for each action
    }
    public pAction(float val){
        fVal = val; //Constructor with defined fatigue value
    }
    void Start()
    { }
    void Update()
    { }
}
