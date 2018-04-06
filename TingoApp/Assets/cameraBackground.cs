using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBackground : MonoBehaviour {
	public Camera cBackground;
	public Color32 nightColor = new Color32(0,24,72,255);
	public Color32 dayColor = new Color32 (229, 229, 229, 255);
	// Use this for initialization
	void Start () {
		cBackground = GetComponent<Camera> ();
	}

	public void nightTime(){
		cBackground.backgroundColor = nightColor;
	}

	public void dayTime(){
		cBackground.backgroundColor = dayColor;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
