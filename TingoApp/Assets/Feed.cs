using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Feed : MonoBehaviour {
	
	float berryValue = .1f;				//How much berries raise health by
	float healthDrainRate = .027f;		//How much health is lost per hour (this takes 36 houres to starve)

	int maxSteps = 5000; 				// After this ammount of steps has been hit in a day, the rest will go to XP || DEFAULT:5000
	int stepsPerBerry = 500; 			// Steps needed to get a berry || DEAFUALT : 500

	int steps = 1375;				//This will be replaced with the actual steps that were gathered that day

	private PedometerPlugin pedometerPlugin;


	public void OnClick(){
		GameObject sliderObj = GameObject.Find("HealthSlider");
		GameObject buttonObj = GameObject.Find ("Berry");
		GameObject feedObject = GameObject.Find ("Feed");

		//sometimes there is an error and the GameObjects can't be found and are null
		if (sliderObj != null && buttonObj != null && feedObject != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider>();
			var health = healthSlider.value;

			//tries to feed the Tingo if under full health
			if ((health + berryValue) < 1 && PlayerPrefs.GetInt("berries") > 0) {
				healthSlider.value = berryValue + health;
				PlayerPrefs.SetFloat("health", berryValue + health);
				PlayerPrefs.SetInt ("berries", PlayerPrefs.GetInt ("berries") - 1);
				//else tops off the Tingo's health
			} else {
				if (PlayerPrefs.GetInt ("berries") >= 1) {
					healthSlider.value = 1;
					PlayerPrefs.SetInt ("berries", PlayerPrefs.GetInt ("berries") - 1);
				}

			}
			//change text on berries button
			Button feedButton = feedObject.GetComponent<Button> ();
			feedButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("berries")+" Berries";

			//change text on xp level


		}
	}

	public void changeScene(string loadScene){
		SceneManager.LoadScene(loadScene);
	}

	//Kill the Tingo when health is zero
	public void OnHealthChange(){
		GameObject sliderObj = GameObject.Find("HealthSlider");
		if (sliderObj != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider>();
			var health = healthSlider.value;
			PlayerPrefs.SetFloat ("health", health);
			if (health <= 0) {
				changeScene ("death");
			}
		}
	
	}

	//Runs each time game starts
	public void Start () {
		
		pedometerPlugin = PedometerPlugin.GetInstance ();
		pedometerPlugin.SetDebug (0);

		pedometerPlugin.Init ();
		pedometerPlugin.StartPedometerService (SensorDelay.SENSOR_DELAY_FASTEST);
		steps = pedometerPlugin.GetStepToday ();

		//get time as ascii value
		int d1 = DateTime.Now.Hour;
		System.DateTime epochStart = new System.DateTime(2018, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalHours;

		//check last time health was calculated
		float healthDeduction = PlayerPrefs.GetFloat ("health") - (healthDrainRate * (cur_time - PlayerPrefs.GetFloat ("timeOfLastHealthCheck")));

		//Steps should be zero if its a new day
		int oldSteps = PlayerPrefs.GetInt("steps");
		if (oldSteps > steps) {
			oldSteps = steps;
			PlayerPrefs.SetInt ("steps", steps);
		}

		//calculate steps that go to different things
		
		int newSteps = steps - oldSteps;

		if (oldSteps > maxSteps) {
			// Convert all to XP
			float addedXP = (float)((double)newSteps * .0001); //DEFAULT .0001
			Debug.Log("steps:"+steps+" maxsteps "+maxSteps);
			if ((PlayerPrefs.GetFloat ("xp") + addedXP) > 1) {
				float xpToLevel = 1 - PlayerPrefs.GetFloat ("xp");
				addedXP = addedXP - xpToLevel;
				PlayerPrefs.SetInt ("level", PlayerPrefs.GetInt ("level") + 1);
				PlayerPrefs.SetFloat ("xp", 0);
			}
			PlayerPrefs.SetFloat ("xp", PlayerPrefs.GetFloat ("xp") + addedXP);
			PlayerPrefs.SetInt ("steps", PlayerPrefs.GetInt ("steps") + newSteps);
		}else if(steps < maxSteps){
			// Convert all to berries
			//find the number of new berries to add
			int newBerries = newSteps / stepsPerBerry;
			Debug.Log ("new berries "+ newSteps);
			PlayerPrefs.SetInt ("berries",PlayerPrefs.GetInt("berries")+newBerries);

			//find number to add to steps -> other steps aren't used for berries so aren't included
			if (newBerries != 0) {
				int usedSteps = stepsPerBerry / newBerries;
				PlayerPrefs.SetInt ("steps", (oldSteps + usedSteps));
			}
		}else{
			// Part berries part XP
			int berrySteps = newSteps / 2;
			int XPSteps = newSteps / 2;

			// Convert some to XP
			float addedXP = (float)((double)XPSteps * .0001); //DEFAULT .0001
			if ((PlayerPrefs.GetFloat ("xp") + addedXP) > 1) {
				float xpToLevel = 1 - PlayerPrefs.GetFloat ("xp");
				addedXP = addedXP - xpToLevel;
				PlayerPrefs.SetInt ("level", PlayerPrefs.GetInt ("level") + 1);
				PlayerPrefs.SetFloat ("xp", 0);
			}
			PlayerPrefs.SetFloat ("xp", PlayerPrefs.GetFloat ("xp") + addedXP);
			PlayerPrefs.SetInt ("steps", PlayerPrefs.GetInt ("steps") + XPSteps);


			//find the number of new berries to add
			int newBerries = berrySteps / stepsPerBerry;
			PlayerPrefs.SetInt ("berries",PlayerPrefs.GetInt("berries")+newBerries);

			//find number to add to steps -> other steps aren't used for berries so aren't included
			if (newBerries != 0) {
				int usedSteps = stepsPerBerry / newBerries;
				PlayerPrefs.SetInt ("steps", (oldSteps + usedSteps));
			}
		}

		//reduce health
		PlayerPrefs.SetFloat ("health", healthDeduction);
		PlayerPrefs.SetFloat ("timeOfLastHealthCheck", cur_time);

		//Load scene from PlayerPrefs
		GameObject sliderObj = GameObject.Find ("HealthSlider");
		if (sliderObj != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider> ();
			healthSlider.value = PlayerPrefs.GetFloat ("health");
		} else {
			Debug.Log ("Slider is null");
		}

		//Load number of berries in button
		GameObject feedObject = GameObject.Find ("Feed");
		if (feedObject != null) {
			Button feedButton = feedObject.GetComponent<Button> ();
			feedButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("berries")+" Berries";
		}


		//load slider
	}

}
	
