using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour {
	public Light lt;
	public windowStatus window;
	public lightControl light;
	public cameraBackground cam;
	private Color maxColor = new Color32(255,244,215,255);
	private Color minColor = new Color32 (0, 0, 0, 0);
	// Use this for initialization
	void Start () {
		lt = GetComponent<Light> ();

	}
	//Slowly transition into nighttime
	void dark(){
		if (lt.color.r < 0 && lt.color.b < 0 && lt.color.g < 0 && lt.color.a < 0) {
			window.night();
			light.turnOn ();
			cam.nightTime ();
		} else {
			lt.color -= Color.white / 5.0F * Time.deltaTime;
		}
	}
	//Slowly transition into daytime.
	void normal(){
		if (lt.color.r > 0.780 && lt.color.b > 0.881 && lt.color.g > 1 && lt.color.a > 0.780) {
			window.day ();
			light.turnOff ();
			cam.dayTime ();
		} else {
			lt.color += Color.white / 5.0F * Time.deltaTime;
		}
	}
	// Update is called once per frame
	void Update () {
		var currentTime = System.TimeSpan.Parse (System.DateTime.Now.ToString ("HH:mm:ss")); //Get current realtime
		var nightTime = System.TimeSpan.Parse ("18:59:50"); // 7:00pm;
		var dayTime = System.TimeSpan.Parse ("6:59:50"); // 7:00am
		/**
		 * Checks real time to see if it is night time 7pm-6:59am.
		 */
		if (currentTime > nightTime || currentTime < dayTime) { 
			dark ();
		} 
		/*
		 * Checks real-time to see if it is day time 7am-6:59pm.
		 */
		if(currentTime < nightTime && currentTime > dayTime){
			normal ();
		}
	}
}
