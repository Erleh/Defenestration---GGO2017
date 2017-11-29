using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelFade : MonoBehaviour {

    // Use this for initialization
    public GameObject panel;
    private Image blinkImage;
    private bool blink;
    private int ctr = 0;
    private int speed = 80;
	void Start () {
        blinkImage = panel.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ctr > 120)
            ctr = 0;
        if (ctr < speed)
            blink = true;
        else
            blink = false;
        ctr++;
	}
    private void OnGUI()
    {
        if (blink)
        {
            blinkImage.enabled = true;
        }
        else
        {
            blinkImage.enabled = false;
        }
    }

}
