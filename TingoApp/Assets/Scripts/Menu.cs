using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	//change scene
	public void changeScene(string loadScene){
		SceneManager.LoadScene(loadScene);
	}
}
