using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour {

    public GameObject charTracker;
    public GameObject progBar;
    public GameObject player;
    private float left, right;
    private Image progLine;
    private Vector3 progress;
	// Use this for initialization
	void Start () {
        progLine = progBar.GetComponent<Image>();
        progress = new Vector3();
        /*To do:*/
        //Get Rect Transform of prog line image to find bounds
        RectTransform rt = progLine.rectTransform;
        left = progLine.transform.position.x - (rt.rect.width / 2);
        right = progLine.transform.position.x + (rt.rect.width / 2);
        Debug.Log(rt.rect.width);


	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //clamp progress vector3 to bounds within prog line image
        charTracker.transform.position = new Vector3(Mathf.Clamp(charTracker.transform.position.x, left, right), 0f, 0f);
        //set charTracker to move within those bounds on that vector3
       // track(, player.transform.x);
    }
    void track(float prevPos, float newPos)
    {
        //if (prevPos > newPos)
          //  return false;
       // else
            //return true;
    }
}
