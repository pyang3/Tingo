using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class testModal : MonoBehaviour{
	private ModalPanel modalPanel;
	private UnityAction yesAction;
	private UnityAction noAction;

	void Awake(){
		modalPanel = ModalPanel.getInstance ();

		yesAction = new UnityAction (testYes);
		noAction = new UnityAction (testNo);
	}

	public void testModalYesNo(){
		modalPanel.choice ("Ben sucks major eggs", testYes, testNo);
	}

	void testYes(){
		Debug.Log ("Yes was clicked");
	}

	void testNo(){
		Debug.Log ("No was clicked");
	}
}