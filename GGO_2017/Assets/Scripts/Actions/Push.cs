using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour {

	public delegate void makePush(GameObject character);

	public static event makePush onPush;

	public GameObject character;

	public static void OnCharacterPush(GameObject character)
	{
		if(onPush != null)
		{
			onPush(character);
		}
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.Space))
			onPush(character);
	}
}
