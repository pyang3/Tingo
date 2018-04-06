using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour {
	public Light lt;
	public windowStatus window;
	public lightControl light;
	// Use this for initialization
	void Start () {
		lt = GetComponent<Light> ();

	}
	void dark(){
		lt.enabled = false;
	}

	void normal(){
		lt.enabled = true;
	}
	// Update is called once per frame
	void Update () {
		var currentTime = System.TimeSpan.Parse (System.DateTime.Now.ToString ("HH:mm:ss")); //Get current realtime
		var nightTime = System.TimeSpan.Parse ("19:00:00"); // 7:00pm;
		var dayTime = System.TimeSpan.Parse ("7:00:00"); // 7:00am
		/**
		 * Checks real time to see if it is night time 7pm-6:59am.	
		 */
		if (currentTime > nightTime || currentTime < dayTime) { 
			window.night();
			light.turnOn ();
			dark ();
		} 
		/*
		 * Checks real-time to see if it is day time 7am-6:59pm.
		 */
		if(currentTime < nightTime && currentTime > dayTime){
			window.day ();
			light.turnOff ();
			normal ();
		}
	}
}
