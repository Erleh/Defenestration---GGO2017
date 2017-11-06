using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* IInteractable - Defines fatigue values of interactable objects and the 
 * parameter by which they may be destroyed*/
public interface IInteractable{
    //Property Declaration - Amount of fatigue that is relieved from player
    int fatigue{
        get;
        set;
    }
    bool usedCorrectly();
    void Kill();
}
