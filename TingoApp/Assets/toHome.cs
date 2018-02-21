using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toHome : MonoBehaviour {

	public void backPressed(string loadScene){
		Debug.Log("Pressed");
		SceneManager.LoadScene(loadScene);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
