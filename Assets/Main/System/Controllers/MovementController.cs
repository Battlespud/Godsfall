using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	public Camera camera;
    public Transform teleportTarget;
	public GameObject character_go;
	CharacterController character_controller;
	SpriteController spriteController;
	public Animator animator;
	public bool isPlayer;
	public bool isMoving = false;

	//is the body damaged? ie, missing legs, broken bones etc. Set via event from Body.cs
	public bool bodyCanMove = true;
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

	float move_speed = 6.5f;

	private float timeAdjusted(float f){
		return (f * Time.deltaTime);
	}

	// Use this for initialization
	void Start () {
		character_go = this.gameObject;
		character_controller = character_go.GetComponent<CharacterController> ();
		spriteController = GetComponentInChildren<SpriteController> ();
		camera = Camera.main;
	}

	Vector3 toMove;

	// Update is called once per frame
	void Update () {
		if (toMove != new Vector3 (0, 0, 0)) {
			isMoving = true;
		} else {
			isMoving = false;
		}
		toMove.y = -1f;
		switch (isPlayer) {
		case (true):
			{
				checkMovementInput ();
			//	checkZoomInput ();
				move (toMove);
			//	lookAtMouse (); 
				break;
			}
		case (false):
			{
				npcMove (toMove);
				break;
			}

		}
		if (toMove != new Vector3 (0, 0, 0)) {
			spriteController.UpdateSprite (toMove);
		}
		clearBuffer ();


	}

	private void clearBuffer(){
		toMove = new Vector3 (0f, 0f, 0f);
	}

	public void teleport(Vector3 vec){
        //TODO
        this.transform.position = vec;
		Debug.Log("Teleport was called");
	}


	private void checkMovementInput(){
		if (Input.GetKey (InputCatcher.ForwardKey)) {
			toMove += Vector3.forward;
		}
		if (Input.GetKey (InputCatcher.BackKey)) {
			toMove += Vector3.back;
		}
		if (Input.GetKey (InputCatcher.LeftKey)) {
			toMove += Vector3.left;
		}
		if (Input.GetKey (InputCatcher.RightKey)) {
			toMove += Vector3.right;
		}
	}

	private void checkZoomInput(){
		var zoom = Input.GetAxis ("Mouse ScrollWheel");
		camera.fieldOfView += -20*zoom;
		if (camera.fieldOfView < 18)
			camera.fieldOfView = 18;
		if (camera.fieldOfView > 75)
			camera.fieldOfView = 75;
	}

	private void move(Vector3 vec){
		if (canMove()) {
			character_controller.Move (toMove * Time.deltaTime * move_speed);
		}
//		camera.transform.position = new Vector3 (character_go.transform.position.x, camera.transform.position.y, character_go.transform.position.z - 14f);
	}

	public void npcInputToMove(Vector3 i){
		if (canMove ()) {
			toMove += i;
		}
	}

	private void npcMove(Vector3 vec){
		if (canMove ()) {
			character_controller.Move (vec * Time.deltaTime * move_speed);
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
