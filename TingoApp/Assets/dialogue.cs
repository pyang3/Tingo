using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class dialogue : MonoBehaviour {
	int clicks = 0;

	public void onClick(){
		if (clicks == 0) {
			GameObject.Find ("Dialogue").GetComponentInChildren<Text> ().text = "Tingo: You can get more berries to feed me with by walking and getting enough steps throughout the day.";
			clicks++;
		} else {
			GameObject.Find ("Dialogue").SetActive(false);
		}
	}
}
