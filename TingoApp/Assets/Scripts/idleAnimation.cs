using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Events;

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
		//Get random vector 3 of a position to go to
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

	//Gets the actual path that the Tingo will take
	void getPath(){
		newTarget = getNewPos ();
		nav.SetDestination (newTarget);
	}
		
	//Deals with waiting for the new path, whether or not the path is valid
	IEnumerator wait(){	
		waitCheck = true;
		yield return new WaitForSeconds (timeForPath);
		getPath ();
		validPath = nav.CalculatePath (newTarget, path);

		//pauses at location and then moves on to the next one
		while (!validPath) {
			yield return new WaitForSeconds (7.0f);
			//getBody. (Vector3.up * 10, ForceMode.Impulse);
			iTween.RotateBy (gameObject, iTween.Hash("z",180, "time", 1));
			getPath ();
			validPath = nav.CalculatePath (newTarget, path);
		}
		waitCheck = false;
	}
}
