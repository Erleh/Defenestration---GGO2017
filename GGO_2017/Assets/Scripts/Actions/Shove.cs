using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shove : MonoBehaviour {

	public delegate void makeShove(GameObject Character);
	public static event makeShove onShove;

	//public float shoveSpeed;
	//public float shoveDistance;

	public GameObject character;
	//private float step;
	//private Vector3 currPlace, addPlace;
    
	/*void Start()
	{
		addPlace = new Vector3(shoveDistance, 0 , 0);
	}*/

	public static void OnCharacterShove(GameObject character)
	{
        if(onShove != null)
            onShove(character);
        /*
		UpdatePos();

		if(onShove != null){
			if(character == enemy)
				gameObject.transform.position = Vector3.MoveTowards(currPlace, currPlace + addPlace, step);
		}*/
    }

	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Z))
            onShove(character);
		//step = shoveSpeed * Time.deltaTime;
	}
    
	/*void UpdatePos()
	{
		currPlace = gameObject.transform.position;
	}*/
		
}
