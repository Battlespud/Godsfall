using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnButtonClickTest : MonoBehaviour {




	// Use this for initialization
	void Start () {
	//	EventSystem.onTest += blowup;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void blowup(){
		GameObject boop = this.gameObject;
		boop.SetActive (false);
		Debug.Log ("boop");
	}
}
