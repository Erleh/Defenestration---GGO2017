using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : CollisionDetector {

	public GameObject applyTo;

	public double gravityModifier = .5;

	private Vector3 gravity;

	void Start()
	{
		CalculateRaySpacing();
		gravity= Physics2D.gravity * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () 
	{
		UpdateRayCastOrigins();
		VerticalCollision(ref gravity);
		applyTo.transform.position += gravity;
	}
}
