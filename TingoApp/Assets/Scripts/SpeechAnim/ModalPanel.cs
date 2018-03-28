using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModalPanel : MonoBehaviour {
	public Text question;
	public Button yesButton;
	public Button noButton;
	public GameObject modalPanelObject;

	private static ModalPanel modalPanel;

	public static ModalPanel getInstance(){
		if (!modalPanel) {
			modalPanel = FindObjectOfType (typeof(ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.Log ("No panel found");
		}
		return modalPanel;
	}

	public void choice (string question, UnityAction yesEvent, UnityAction noEvent){
		modalPanelObject.SetActive (true);
		yesButton.onClick.RemoveAllListeners ();
		yesButton.onClick.AddListener (yesEvent);
		yesButton.onClick.AddListener (closePanel);

		noButton.onClick.RemoveAllListeners ();
		noButton.onClick.AddListener (noEvent);
		noButton.onClick.AddListener (closePanel);

		this.question.text = question;
		yesButton.gameObject.SetActive (true);
		noButton.gameObject.SetActive (true);
	}

	void closePanel (){
		modalPanelObject.SetActive (false);
	}
}
