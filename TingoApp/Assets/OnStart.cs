using UnityEngine;
using System;
using UnityEngine.SceneManagement;
class MyClass
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void OnBeforeSceneLoadRuntimeMethod()
	{
		int d1 = DateTime.Now.Hour;
		System.DateTime epochStart = new System.DateTime(2018, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalHours;
		PlayerPrefs.GetFloat ("timeOfLastHealthCheck");




		PlayerPrefs.SetFloat ("timeOfLastHealthCheck", cur_time);
		Debug.Log(cur_time);
		//Get time and figure out health decreaces
	}
}