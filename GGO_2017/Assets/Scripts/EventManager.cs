using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
	private Dictionary<string, UnityEvent> eventDictionary;

	private static EventManager eventManager;

	//Gets the eventManager if it exists in the scene, which it should
	public static EventManager instance
	{
		//Attempts to get the eventManager from the scene
		get
		{
			//If there is no current eventManager try to find eventManager from the scene
			if(!eventManager)
			{
				//Finds object of type EventManager then upCast to EventManager
				eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

				//If there still is none found, then it is not in the scene
				if(!eventManager)
				{
					Debug.LogError("There needs to be an EventManager script on a GameObject in the scene");
				}
				//When found, initialize the eventDictionary
				else
				{
					eventManager.Init();
				}
			}

			return eventManager;
		}
	}

	//Initializes the eventDictionary for use in the eventManager
	public void Init()
	{
		//If there is not initiallized eventDictionary start one
		if(eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
	}

	//Adds Listeners for events
	public static void StartListening(string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		//If event name exists, add a listener to that event
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		//If the event doesnt exist, create a new one to add into the dictionary and add listener to new event
		else
		{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	//Makes sure to close and elliminate listeners after usage
	public static void StopListening(string eventName, UnityAction listener)
	{
		//Ensures that event manager exists
		if(eventManager == null)
		{
			return;
		}

		UnityEvent thisEvent = null;

		//If event is found remove listener from this event
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	//Trigger event argument passed
	public static void TriggerEvent(string eventName)
	{
		UnityEvent thisEvent = null;

		//If event exists within the dictionary, invoke methods of this event
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke();
		}
	}
}
