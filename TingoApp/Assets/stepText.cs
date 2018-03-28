using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class stepText : MonoBehaviour {

	public Text myText;

	private PedometerPlugin pedometerPlugin;

	//private string demoName = "[PedometerDemo] ";
	private UtilsPlugin utilsPlugin;

	bool isReady = false;

	// Use this for initialization
	void Start () {
		//get the instance of pedometer plugin
		pedometerPlugin = PedometerPlugin.GetInstance ();
		isReady = true;

//		//set to zero to hide debug toast messages
//		pedometerPlugin.SetDebug (0);
//
//		utilsPlugin = UtilsPlugin.GetInstance ();
//		utilsPlugin.SetDebug (0);
//
//		//check if step detector is supported
//		bool hasStepDetector = utilsPlugin.HasStepDetector ();
//		bool hasStepCounter = utilsPlugin.HasStepCounter ();
//
//		//if (hasStepDetector) {
//		if (hasStepCounter) {
//			//UpdateStepDetectorStatus ("available");
//			// event listeners
//			//AddEventListeners ();
//			//initialze pedometer
//			pedometerPlugin.Init ();
//			//pedometerPlugin.StartPedometerService (SensorDelay.SENSOR_DELAY_FASTEST);
//			isReady = true;
//
//
//
//		} else {
//			//UpdateStepDetectorStatus ("not available");
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isReady)myText.text = "" + pedometerPlugin.GetStepToday();
	}
}
