using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class testSpeach : MonoBehaviour {
	private UnityAction<string> action;
	string input = "";
	private Rigidbody getBody;

	void Awake(){
		action = new UnityAction<string>(ToTextAction);
	}

	void OnEnable(){
		EventManager.StartListening ("Speech", action);
	}

	void OnDisable(){
		EventManager.StopListening ("Speech", action);
	}

	void ToTextAction(string newText){
		string[] words = newText.Split (' ');
		int jump = 0;
		int flip = 0;
		int speak = 0;
		foreach (string x in words) {
			Debug.Log (x);
			if (x == "jump") {
				jump++;
			} else if (x == "flip") {
				flip++;
			} else if (x == "yell" || x == "speak") {
				speak++;
			}
		}
		if (speak > flip && speak > jump)
			input = "speak";
		else if (flip > jump && flip > speak)
			input = "flip";
		else if (jump > flip && jump > speak)
			input = "jump";
		else
			Debug.Log( "null");
	}

	float jumpTime = .8f;
	float jumpHeight = 5.0f;
	float realHeight = .5947088f;
	// Use this for initialization

	void Update(){
		if (input == "jump") {
			StartCoroutine(Jump ());
		}
		if (input == "flip") {
			Flip ();
		}
		input = null;
	}

	IEnumerator Jump(){
		float timer = 0.0f;
		while (timer <= jumpTime) {
			float height = Mathf.Sin (timer / jumpTime * Mathf.PI) * jumpHeight;
			transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
			timer += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = new Vector3(transform.localPosition.x, -1.9147f, transform.localPosition.z);
	}

	void Flip(){
		iTween.RotateBy (gameObject, iTween.Hash("x", -.5, "speed", 125, "delay", .001));
		StartCoroutine (Jump ());
	}
}