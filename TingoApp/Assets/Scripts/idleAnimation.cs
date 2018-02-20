using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class idleAnimation : MonoBehaviour {
	
	int target;
	public float timeForPath; 
	NavMeshAgent nav;
	NavMeshPath path;
	bool waitCheck;
	bool validPath;
	Vector3 newTarget;


	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		path = new NavMeshPath ();
	}

	Vector3 getNewPos(){
		float x = Random.Range(-16, 16);
		float z = Random.Range (-4, 4);
		Vector3 retVal = new Vector3 (x, 0, z); 
		return retVal;
	}

	// Update is called once per frame
	void Update () {
		if(!waitCheck){
			StartCoroutine (wait ());
		}
	}

	void getPath(){
		newTarget = getNewPos ();
		nav.SetDestination (newTarget);
	}

	//Waits a few seconds for new path
	IEnumerator wait(){	
		waitCheck = true;
		yield return new WaitForSeconds (timeForPath);
		getPath ();
		validPath = nav.CalculatePath (newTarget, path);

		if (!nav.CalculatePath (newTarget, path)) {
			Debug.Log ("invalid path");
		}

		while (!validPath) {
			yield return new WaitForSeconds (7.0f);
			getPath ();
			validPath = nav.CalculatePath (newTarget, path);
		}


		waitCheck = false;
	}
}
