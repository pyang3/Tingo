using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Feed : MonoBehaviour {
	
	float berryValue = .1f;

	public void OnClick(){
		Debug.Log ("Feeding...");
		GameObject sliderObj = GameObject.Find("HealthSlider");
		GameObject buttonObj = GameObject.Find ("Berry");
		if (sliderObj != null && buttonObj != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider>();
			var health = healthSlider.value;
			if ((health + berryValue) < 1 && PlayerPrefs.GetInt("berries") > 0) {
				healthSlider.value = berryValue + health;
				PlayerPrefs.SetFloat("health", berryValue + health);
				PlayerPrefs.SetInt ("berries", PlayerPrefs.GetInt ("berries") - 1);
				Debug.Log (PlayerPrefs.GetInt("berries"));
			} else {
				if (PlayerPrefs.GetInt ("berries") >= 1) {
					healthSlider.value = 1;
					PlayerPrefs.SetInt ("berries", PlayerPrefs.GetInt ("berries") - 1);
				}

			}

		}

	}

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt("berries", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
