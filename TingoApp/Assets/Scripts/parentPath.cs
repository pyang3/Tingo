using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentPath : MonoBehaviour {
	double getPositionX, getPositionZ;
	bool check;

	// Use this for initialization
	void Start () {
		Debug.Log (transform.position.x);
		Debug.Log (getPositionX);
		positionOne ();
	}

	void Update(){
		getPositionX = transform.position.x;
		getPositionZ = transform.position.z;
		if (getPositionX < -4.095 && getPositionX > -4.111 && (getPositionZ > 1.61 && getPositionZ < 1.65)) {
			positionOne ();
		}

		if ((getPositionX < 5.22 && getPositionX > 5.20) && (getPositionZ > 2.8 && getPositionZ < 2.88)) {
			positionTwo ();
		}

		if ((getPositionX < -8.35 && getPositionX > -8.409) && (getPositionZ > -3.38 && getPositionZ < -3.0)) {
			positionThree ();
		}
	}

	void positionTwo(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathTwo"), "speed", 4, "delay", 2, "orientToPath", true));
//		positionThree ();
	}

	void positionOne(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathOne"), "speed", 4, "delay", 2, "orientToPath", true));
//		StartCoroutine (waitTime ());
	}
//
	void positionThree(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathThree"), "speed", 4, "delay", 2, "orientToPath", true));	
	}

}
