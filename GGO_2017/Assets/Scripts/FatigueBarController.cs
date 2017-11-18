using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatigueBarController : MonoBehaviour {
    public float fatigue;
    public float minFatigue;
    public float maxFatigue;
    private Slider fatigueBar;
	// Use this for initialization
	void Start () {
        fatigueBar = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateFatigueBar();
        fatigue = Mathf.Clamp(fatigue, minFatigue, maxFatigue);

	}
    void UpdateFatigueBar(){     fatigueBar.value = fatigue;}
}
