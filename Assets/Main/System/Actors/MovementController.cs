using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	public GameObject player_go;
	CharacterController player_controller;

	bool rolling = false;

	float move_speed = 5f;

	private float timeAdjusted(float f){
		return (f * Time.deltaTime);
	}

	// Use this for initialization
	void Start () {
		player_go = this.gameObject;
		player_controller = player_go.GetComponent<CharacterController> ();
	}

	Vector3 toMove;

	// Update is called once per frame
	void Update () {
		clearBuffer ();
		checkMovementInput ();
		move (toMove);
	}

	private void clearBuffer(){
		toMove = new Vector3 (0f, 0f, 0f);
	}


	private void checkMovementInput(){
		if (Input.GetKey (InputCatcher.ForwardKey)) {
			toMove += Vector3.forward*move_speed;
		}
		if (Input.GetKey (InputCatcher.BackKey)) {
			toMove += Vector3.back*move_speed;
		}
		if (Input.GetKey (InputCatcher.LeftKey)) {
			toMove += Vector3.left*move_speed;
		}
		if (Input.GetKey (InputCatcher.RightKey)) {
			toMove += Vector3.right*move_speed;
		}
	}

	private void move(Vector3 vec){
		player_controller.Move (toMove * Time.deltaTime);
	}


}
