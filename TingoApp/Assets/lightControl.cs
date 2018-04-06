using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControl : MonoBehaviour {
	private Light lightStatus;
	// Use this for initialization
	void Start () {
		lightStatus = GetComponent<Light> ();
	}
	//Turn on Light in Lamp
	public void turnOn(){
		lightStatus.enabled = true;
	}
	//Turn off Light in Lamp
	public void turnOff(){
		lightStatus.enabled = false;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
