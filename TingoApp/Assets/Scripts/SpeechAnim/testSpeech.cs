/*Questions for ben
 * Would it be better to catagorize the inputs into the NN?
 * What would the output of the NN be? A simple string of what we want it to do? Something else
 * Confusion
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class testSpeach : MonoBehaviour {
	string[] actionWords = new string[] {"jump", "hello", "spin"};
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
		int hello = 0;
		int flip = 0;
		int speak = 0;
		foreach (string x in words) {
			Debug.Log (x);
			if (x == "jump") {
				jump++;
			} else if (x == "hello") {
				hello++;
			} else if (x == "spin") {
				flip++;
			} else if (x == "yell" || x == "speak") {
				speak++;
			}
		}
		if (hello > flip && hello > jump && flip > speak)
			input = "hello";
		else if (flip > hello && flip > jump && flip > speak)
			input = "flip";
		else if (jump > hello && jump > flip && jump > speak)
			input = "jump";
		else
			Debug.Log( "null");
	}

	float jumpTime = .8f;
	float jumpHeight = 2.0f;
	// Use this for initialization

	void Update(){
		if (input == "jump") {
			StartCoroutine(Jump ());
		}
	}

	IEnumerator Jump(){
		float timer = 0.0f;
		//iTween.RotateBy (gameObject, iTween.Hash("z",180, "time", 1));
		while (timer <= jumpTime) {
			float height = Mathf.Sin (timer / jumpTime * Mathf.PI) * jumpHeight;
			transform.localPosition = new Vector3(transform.localPosition.x, height, 0);
			timer += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = Vector3.zero;
	}
}