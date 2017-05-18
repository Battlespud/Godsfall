using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSystem : MonoBehaviour {

	public delegate void testDelegate();
	public static event testDelegate onTest;

	public delegate void hit();
	public static event hit onHit;

	public delegate void hpChanged();
	public static event hpChanged onHpChanged;



	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))
		{
			if(onTest != null)
				onTest();
			Debug.Log ("clicked");
		}

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			onTest ();
		}
	}
}




