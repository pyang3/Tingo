using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnitySentiment;

public class testSpeach : MonoBehaviour {
	private UnityAction<string> action;
	public SentimentAnalysis predictionObject;
	string input = "";
	private Rigidbody getBody;
	public AudioClip yay;
	private AudioSource source;

	//used for animations
	float jumpTime = .8f;
	float jumpHeight = 5.0f;
	float realHeight = .5947088f;

	//used for sentiment analysis
	private bool threadStarted = false;
	private bool responseFromThread = false;
	private Vector3 SentimentAnalysisResponse;

	void Awake(){
		action = new UnityAction<string>(ToTextAction);
		predictionObject.Initialize ();
	}

	void OnEnable(){
		source = GetComponent<AudioSource> ();
		SentimentAnalysis.OnAnlysisFinished += GetAnalysisFromThread;
		EventManager.StartListening ("Speech", action);
	}

	void OnDisable(){
		EventManager.StopListening ("Speech", action);
	}

	void ToTextAction(string newText){
		string[] words = newText.Split (' ');
//		int jump = 0;
//		int flip = 0;
//		int speak = 0;
//		foreach (string x in words) {
//			Debug.Log (x);
//			if (x == "jump") {
//				jump++;
//			} else if (x == "flip") {
//				flip++;
//			} else if (x == "yell" || x == "speak") {
//				speak++;
//			}
//		}
//		if (speak > flip && speak > jump)
//			input = "speak";
//		else if (flip > jump && flip > speak)
//			input = "flip";
//		else if (jump > flip && jump > speak)
//			input = "jump";
//		else
//			Debug.Log( "null");
//	}

		predictionObject.PredictSentimentText (newText);
		if (!threadStarted)
		{// Thread Started
			threadStarted = true;
			StartCoroutine(WaitResponseFromThread());
		}
	}

	private void GetAnalysisFromThread(Vector3 analysisResult)
	{		
		SentimentAnalysisResponse = analysisResult;
		responseFromThread = true;
		//trick to call method to the main Thread
	}

	private void chooseResponse(){
		// x Vector = positve
		// y Vector = negative
		// z Vector = neutral

		if (SentimentAnalysisResponse.x > SentimentAnalysisResponse.y && SentimentAnalysisResponse.x > SentimentAnalysisResponse.z) {
			Debug.Log ("This is a postive response");
			input = "jump";
		}
		else if (SentimentAnalysisResponse.y > SentimentAnalysisResponse.x && SentimentAnalysisResponse.y > SentimentAnalysisResponse.z) {
			Debug.Log ("This is a negative response");
			input = "flip";
		}
		else if (SentimentAnalysisResponse.z > SentimentAnalysisResponse.y && SentimentAnalysisResponse.z > SentimentAnalysisResponse.x) {
			Debug.Log ("This is a neutral response");
		}

	}
		
	// Use this for initialization

	void Update(){
		if (input == "jump") {
			StartCoroutine(Jump ());
		}
		if (input == "flip") {
			Flip ();
		}
		if (input == "speak") {
			source.PlayOneShot(yay);
		}
		input = null;
	}


	private IEnumerator WaitResponseFromThread()
	{
		while(!responseFromThread) // Waiting For the response
		{
			yield return null;
		}
		// Main Thread Action
		chooseResponse();
		// Reset
		responseFromThread = false;
		threadStarted = false;
	}


	IEnumerator Jump(){
		source.PlayOneShot(yay);
		float timer = 0.0f;
		while (timer <= jumpTime) {
			float height = Mathf.Sin (timer / jumpTime * Mathf.PI) * jumpHeight;
			transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
			timer += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = new Vector3(transform.localPosition.x, .5947088f, transform.localPosition.z);
	}

	void Flip(){
		iTween.RotateBy (gameObject, iTween.Hash("x", -.5, "speed", 125, "delay", .001));
		StartCoroutine (Jump ());
	}

	void chooseAction(){
		
	}
}