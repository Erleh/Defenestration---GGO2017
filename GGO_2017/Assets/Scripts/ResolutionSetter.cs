using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetter : MonoBehaviour {

	public int DefaultX = 960;
	public int DefaultY = 640;
	// Use this for initialization
	void Start () 
	{
		Screen.SetResolution(960, 640, false);
	}
}
