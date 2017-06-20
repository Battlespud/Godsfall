using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorInMapTrigger : MonoBehaviour {

	[SerializeField]private Camera cam;
	private Camera playerCam;

	[SerializeField]GameObject wall;

	Transform recordedTransform;

	// Use this for initialization
	void Start () {
		playerCam = Camera.main;
		cam.enabled = true;
		cam.gameObject.SetActive (false);
	}




	void OnTriggerEnter(Collider col){
		if (CheckIsPlayer(col.gameObject)) {
			EntersInterior ();
		}
	}


	void EntersInterior(){
		//playerCam.gameObject.SetActive (false);
	//	playerCam = camera.
		cam.gameObject.SetActive(true);
		wall.SetActive (false);
		//makes it so the last known settings of the camera direction script is correct and stops the sprites + controls from glitching out if you change direction.
		playerCam.gameObject.transform.SetPositionAndRotation (cam.gameObject.transform.position, cam.gameObject.transform.rotation);
	}


	void OnTriggerExit(Collider col){
		if (CheckIsPlayer(col.gameObject)) {
			ExitsInterior ();
		}
	}

	void ExitsInterior(){
	//	playerCam.gameObject.SetActive (true);
		cam.gameObject.SetActive (false);
		wall.SetActive (true);
	}


	bool CheckIsPlayer(GameObject obj){
		if (obj.GetComponent<MovementController> () != null) {
			if (obj.GetComponent<MovementController> ().isPlayer) {
				return true;
			}
		}
		return false;
	}


	// Update is called once per frame
	void Update () {
		
	}
}
