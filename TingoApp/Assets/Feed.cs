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
			if (health <= 0) {
				Debug.Log ("Death");
			}
		}
	
	}

	// Add berries for testing
//	void Start () {
//		PlayerPrefs.SetInt("berries", 3);
//	}

}
