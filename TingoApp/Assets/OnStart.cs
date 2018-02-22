using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
class OnStart
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void OnBeforeSceneLoadRuntimeMethod()
	{
		//get time as ascii value
		int d1 = DateTime.Now.Hour;
		System.DateTime epochStart = new System.DateTime(2018, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalHours;

		//check last time health was calculated
		PlayerPrefs.GetFloat ("timeOfLastHealthCheck");
		float healthDeduction = PlayerPrefs.GetFloat ("health") - (.05f * (cur_time - PlayerPrefs.GetFloat ("timeOfLastHealthCheck")));

		//reduce health
		PlayerPrefs.SetFloat ("health", healthDeduction);
		Debug.Log(cur_time-PlayerPrefs.GetFloat ("timeOfLastHealthCheck"));
		PlayerPrefs.SetFloat ("timeOfLastHealthCheck", cur_time);

		//Load scene from PlayerPrefs
		GameObject sliderObj = GameObject.Find("HealthSlider");
		if (sliderObj != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider>();
			healthSlider.value = PlayerPrefs.GetFloat ("health");
		}



	}
}