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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ToTextAction(string newText){
		myText.text = newText;
	}
}
