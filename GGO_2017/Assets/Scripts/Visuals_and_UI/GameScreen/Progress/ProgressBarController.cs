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
        minProgress = 46.5f;
        maxProgress = -44.85756f; //winzone.GetComponent<BoxCollider2D>().offset.x + (winzone.GetComponent<BoxCollider2D>().size.x / 2f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateProgressBar();
        progress = Mathf.Clamp(progress, minProgress, maxProgress);

    }
    void UpdateProgressBar() { progressBar.value = progress; }
}
