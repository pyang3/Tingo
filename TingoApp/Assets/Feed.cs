using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Feed : MonoBehaviour {
	
	float berryValue = .1f;

	public void OnClick(){
		GameObject sliderObj = GameObject.Find("HealthSlider");
		GameObject buttonObj = GameObject.Find ("Berry");

		//sometimes there is an error and the GameObjects can't be found and are null
		if (sliderObj != null && buttonObj != null) {
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

		}
	}
	//Kill the Tingo when health is zero
	public void OnHealthChange(){
		GameObject sliderObj = GameObject.Find("HealthSlider");
		if (sliderObj != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider>();
			var health = healthSlider.value;
			PlayerPrefs.SetFloat ("health", health);
			if (health <= 0) {
				Debug.Log ("Death");
			}
		}
	
	}

	 //Add berries for testing
	public void Start () {
		PlayerPrefs.SetInt("berries", 5);
		Debug.Log ("Start");
		GameObject.Find ("HealthSlider").GetComponent <Slider> ().value = .5f;

		//get time as ascii value
		int d1 = DateTime.Now.Hour;
		System.DateTime epochStart = new System.DateTime(2018, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalHours;

		//check last time health was calculated
		PlayerPrefs.GetFloat ("timeOfLastHealthCheck");
		float healthDeduction = PlayerPrefs.GetFloat ("health") - (.05f * (cur_time - PlayerPrefs.GetFloat ("timeOfLastHealthCheck")));

		//reduce health
		PlayerPrefs.SetFloat ("health", healthDeduction);
		PlayerPrefs.SetFloat ("timeOfLastHealthCheck", cur_time);

		//Load scene from PlayerPrefs
		GameObject sliderObj = GameObject.Find ("HealthSlider");
		//		GameObject.Find ("HealthSlider").GetComponent <Slider> ().value = .5f;
		if (sliderObj != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider> ();
			Debug.Log (PlayerPrefs.GetFloat ("health"));
			healthSlider.value = PlayerPrefs.GetFloat ("health");
		} else {
			Debug.Log ("Slider is null");
		}
	}

}
