using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReviver : MonoBehaviour {

	public Camera playerCam;


	void Awake(){
		playerCam = Camera.main;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Camera.allCamerasCount == 0) {
			if (!playerCam.gameObject.activeInHierarchy) {
				playerCam.gameObject.SetActive (true);
			}
		}
		else if (Camera.allCamerasCount > 1) {
			if (playerCam.gameObject.activeInHierarchy) {
				playerCam.gameObject.SetActive (false);
			}
	}
}
}
