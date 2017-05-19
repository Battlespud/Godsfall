using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour {







	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))
		{
			Debug.Log ("clicked");
		}

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {

		//TESTING
		 //see actor for more testing stuff
		}
	}
}




