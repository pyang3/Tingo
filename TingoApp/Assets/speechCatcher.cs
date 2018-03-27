using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class speechCatcher : MonoBehaviour {

	private UnityAction<string> action;
	public Text myText;

	void Awake(){
		action = new UnityAction<string>(ToTextAction);
		myText.text = "Im ready";
	}

	void OnEnable(){
		EventManager.StartListening ("Speech", action);
	}

	void OnDisable(){
		EventManager.StopListening ("Speech", action);
	}

	// Use this for initialization
	void Start () {
		var plugin = new AndroidJavaObject ("com.tingoapp.unityplugin.PluginClass");

		AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
		plugin.Call ("setContext", activity);

		myText.text = "steps = " + plugin.Call<long> ("sayHi", "James");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ToTextAction(string newText){
		myText.text = newText;
	}
}
