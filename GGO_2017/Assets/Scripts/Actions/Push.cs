﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour {

	public delegate void makePush(GameObject character);

	public static event makePush onPush;

	public static void OnCharacterPush(GameObject character)
	{
		if(onPush != null)
		{
			onPush(character);
		}
	}
}
