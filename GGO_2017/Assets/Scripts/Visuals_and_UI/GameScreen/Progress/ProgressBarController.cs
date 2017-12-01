using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour {


    public GameObject floor;
    public GameObject winzone;
    public float minProgress;
    public float maxProgress;
    private float rightSide;
    private Slider progressBar;
    public float progress;
    // Use this for initialization
    void Start()
    {
        progressBar = GetComponent<Slider>();
        //Right Endpoint of wall
        minProgress = 21; //note: values are currently hard-coded; later, change to be compliant with beginning and end-binding objects
        maxProgress = -48.554f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateProgressBar();
        progress = Mathf.Clamp(progress, minProgress, maxProgress);

    }
    void UpdateProgressBar() { progressBar.value = progress; }
}
