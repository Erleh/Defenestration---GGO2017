using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shove : MonoBehaviour {

	public delegate void makeShove(GameObject Character);

	public static event makeShove onShove;

	public float shoveSpeed;
	public float shoveDistance;

	private float step;
	private Vector3 currPlace, addPlace;

	void Start()
	{
		addPlace = new Vector3(shoveDistance, 0 , 0);
	}

	public static void OnCharacterShove(GameObject character)
	{
		UpdatePos();

		if(onShove != null)
		{
			onShove(character);

			if(character.CompareTag("Enemy"))
			{
				gameObject.transform.position = Vector2.MoveTowards(currPlace, currPlace + addPlace, step);
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		step = shoveSpeed * Time.deltaTime;
	}

	void UpdatePos()
	{
		currPlace = gameObject.transform.position;
	}
		
}
