using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ThisEvent : UnityEvent<string>{}

public class EventManager : MonoBehaviour
{

	private Dictionary<string, ThisEvent> eventDictionary;
	private static EventManager eventManager;

	public static EventManager instance {
		get {
			if (!eventManager) {
				eventManager = FindObjectOfType (typeof(EventManager)) as EventManager;
				if (!eventManager) {
					Debug.LogError ("ERROR!");
				} else {
					eventManager.Init ();
				}
			}
			return eventManager;
		}
	}

	void Init ()
	{
		if (eventDictionary == null) {
			eventDictionary = new Dictionary<string, ThisEvent> ();
		}
	}

	public static void StartListening(string eventName, UnityAction<string> listener){
		ThisEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
			thisEvent.AddListener (listener);
		}else{
			thisEvent = new ThisEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction<string> listener){
		if (eventManager == null)
			return;
		ThisEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent(string eventName, string message){
		ThisEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.Invoke (message);
		}
	}
}
