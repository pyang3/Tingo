using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void menuPressed(string loadScene){
		Debug.Log("Pressed");
		SceneManager.LoadScene(loadScene);
	}
}
