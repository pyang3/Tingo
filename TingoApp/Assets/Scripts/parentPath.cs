using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentPath : MonoBehaviour {
	double getPosition;

	// Use this for initialization
	void Start () {
		getPosition = transform.position.x;
		Debug.Log (transform.position.x);
		Debug.Log (getPosition);
		if (getPosition < -4.095 && getPosition > -4.111) {
			positionOne ();
		}
	}

	void positionTwo(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathTwo"), "time", 5, "delay", 2));
		positionThree ();
	}

	void positionOne(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathOne"), "time", 5, "delay", 2));
		positionTwo ();
	}

	void positionThree(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathThree"), "time", 5, "delay", 2));
		positionTwo ();		
	}
}
