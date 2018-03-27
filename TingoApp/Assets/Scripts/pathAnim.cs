using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathAnim : MonoBehaviour {
	float jumpTime = .8f;
	float jumpHeight = 2.0f;
	// Use this for initialization

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
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
