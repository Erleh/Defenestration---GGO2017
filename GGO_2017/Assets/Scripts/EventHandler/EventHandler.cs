using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour 
{
	//1. Define delegate and events
	//2. trigger the events
	//3. subscribing gameobject to the events

	public delegate void CharacterEventHandler(GameObject character);

	public static event CharacterEventHandler onPush;
	public static event CharacterEventHandler onShove;
	public static event CharacterEventHandler onKick;

	//possible related classes
	//	Stamina
	//	Location bar

	public static void OnCharacterPush(GameObject character)
	{
		if(onPush != null)
		{
			onPush(character);
		}
	}

	public static void OnCharacterShove(GameObject character)
	{
		if(onShove != null)
		{
			onShove(character);
		}
	}

	public static void OnCharacterKick(GameObject character)
	{
		if(onKick != null)
		{
			onKick(character);
		}
	}
		
}
