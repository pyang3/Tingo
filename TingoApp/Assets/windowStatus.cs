using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windowStatus : MonoBehaviour {
	public Color32 nightColor = new Color32(0,24,72,255);
	public Color32 dayColor = new Color32 (15, 133, 203, 255);
	// Use this for initialization
	void Start () {
		
	}

	public void day ()
	{
		if (gameObject.tag == "Window") { //get window object
			GetComponent<Renderer> ().materials[1].color = dayColor; //set material_1 to night color

		}
	}

	public void night(){
		if (gameObject.tag == "Window") { //get window object
			GetComponent<Renderer> ().materials[1].color = nightColor; //set material_1 to day color
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
