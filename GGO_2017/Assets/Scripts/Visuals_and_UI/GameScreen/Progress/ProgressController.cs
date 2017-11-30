using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressController : MonoBehaviour {

    public GameObject player;
    public ProgressBarController progBar;
    private void Awake()
    {
        progBar.progress = player.transform.localPosition.x;
    }
    void FixedUpdate()
    {
        progBar.progress = player.transform.localPosition.x;
        //Debug.Log(progBar.progress);
    }

}
