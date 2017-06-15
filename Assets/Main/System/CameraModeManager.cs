using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CameraMode{
	NORMAL=0,
	SIDESCROLLER,
	OVERHEAD
};


public class CameraModeManager : MonoBehaviour {

	GameObject player;


	public CameraMode cameraMode;

	// Use this for initialization
	void Start () {
		cameraMode = CameraMode.NORMAL;
		player = gameObject.GetComponent<CameraController> ().target.gameObject;
		player.GetComponent<MovementController> ().camModeManager = this;
		gameObject.GetComponent<CameraController> ().manager = this;
	}




	public void SwitchMode(CameraMode camMode){
		cameraMode = camMode;
	}





	// Update is called once per frame
	void Update () {
		
	}
}
