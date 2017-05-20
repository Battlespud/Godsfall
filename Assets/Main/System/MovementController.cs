using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	public CharacterController cameraController;
	public Camera camera;
	public GameObject player_go;
	CharacterController player_controller;
	public Animator animator;

	//is the body damaged? ie, missing legs, broken bones etc. Set via event from Body.cs
	bool bodyCanMove = true;
	public void setBodyCanMove(bool b){
		bodyCanMove = b;
	}

	//are we disabled from a stun, sleep, paralysis or some other short term effect? Will be Set via event from Actor.cs
	bool disabled = false;
	public void setDisabled(bool b){
		disabled = b;
	}

	//combine all our various things that can affect whether we move or not
	bool canMove(){
		return (!disabled && bodyCanMove);
	}

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
		checkZoomInput ();
		move (toMove);
		lookAtMouse (); //todo remove later
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

	private void checkZoomInput(){
		var zoom = Input.GetAxis ("Mouse ScrollWheel");
		camera.fieldOfView += -20*zoom;
		if (camera.fieldOfView < 18)
			camera.fieldOfView = 18;
		if (camera.fieldOfView > 40)
			camera.fieldOfView = 40;
	}

	private void move(Vector3 vec){
		if (canMove()) {
			player_controller.Move (toMove * Time.deltaTime);
			cameraController.Move (toMove * Time.deltaTime);
		}
	}

	private void lookAtMouse(){
		Vector3 mouse_pos;
		Transform target = this.gameObject.transform;
		Vector3 object_pos;
		float angle;

		mouse_pos = Input.mousePosition;
		mouse_pos.z = -13;
		object_pos = Camera.main.WorldToScreenPoint (target.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (new Vector3 (0, -angle+90, 0));

	}

}
