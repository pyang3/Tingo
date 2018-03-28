using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathAnim : MonoBehaviour {
	float jumpTime = .8f;
	float jumpHeight = 5.0f;
	float realHeight = .5947088f;
	public AudioClip yay;
	private AudioSource source;
	// Use this for initialization

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine(Jump ());
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Flip ();
		}
	}

	IEnumerator Jump(){
		source.PlayOneShot(yay);
		//iTween.Stab (gameObject, iTween.Hash ("delay", .25, "clip", yay, "pitch", 1));
		transform.localPosition = new Vector3(0, realHeight, 0);
		float timer = 0.0f;
		while (timer <= jumpTime) {
			float height = Mathf.Sin (timer / jumpTime * Mathf.PI) * jumpHeight;
			transform.localPosition = new Vector3(transform.localPosition.x, height, 0);
			timer += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = new Vector3(0, realHeight, 0);
	}

	void Flip(){
		iTween.RotateBy (gameObject, iTween.Hash("z", -.5, "speed", 125, "delay", .001));
		StartCoroutine (Jump ());
	}
}
