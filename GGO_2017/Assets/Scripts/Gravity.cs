using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

	public Rigidbody2D applyTo;

	public double gravityModifier = .5;

	private Vector3 gravity;

	void Start()
	{
		gravity= Physics2D.gravity * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () 
	{
		applyTo.transform.position += gravity;

		Debug.Log(gravity);
	}
}
