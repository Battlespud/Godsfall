using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	public Camera camera;
	public CameraModeManager camModeManager;
    public Transform teleportTarget;
	public GameObject character_go;
	CharacterController character_controller;
	SpriteController spriteController;
	public Animator animator;
	public bool isPlayer;
	public bool isMoving = false;
	public bool hasSpriteController = false;
	public bool Gravity = true;
	private const float GRAVITY = 2F;
	public bool lockout = false; //use to lockout input except from combat controller

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


	int gravityTimer=0;
	int mGrav = 20;

	// Use this for initialization
	void Start () {
		character_go = this.gameObject;
		camera = Camera.main;
		camModeManager = camera.gameObject.GetComponent<CameraModeManager> ();
		character_controller = character_go.GetComponent<CharacterController> ();
		if (GetComponentInChildren<SpriteController> () != null) {
			spriteController = GetComponentInChildren<SpriteController> ();
			hasSpriteController = true;
		}
		camera = Camera.main;
	}

	Vector3 toMove;
	Vector3 toSprite;

	// Update is called once per frame
	void Update () {
		if (toMove != new Vector3 (0, 0, 0)) {
			isMoving = true;
		} else {
			isMoving = false;
		}
		if (Gravity) {
			toMove.y += -2;
		}
		switch (isPlayer) {
		case (true):
			{
				checkMovementInput ();
				move (toMove);
				break;
			}
		case (false):
			{
				npcMove (toMove);
				break;
			}

		}
		if (hasSpriteController) {
			if (isPlayer && toMove != new Vector3()) {
				spriteController.UpdateSprite (toSprite, true);
			} 
			else if(!isPlayer && toMove != new Vector3()) {
				spriteController.UpdateSprite (toMove, false); //TODO this causes problems with diagonal movement
			}
		}
		clearBuffer ();


	}

	private void clearBuffer(){
		toMove = new Vector3 (0f, 0f, 0f);
		toSprite = new Vector3 (0f, 0f, 0f);
	}

	public void teleport(Vector3 vec){
        //TODO
        this.transform.position = vec;
		Debug.Log("Teleport was called");
	}


	private void checkMovementInput(){
		switch (camModeManager.cameraMode) {

		case (CameraMode.NORMAL):
			{
				if (Input.GetKey (InputCatcher.ForwardKey)) {
					toMove += camera.transform.forward;
					toSprite += transform.forward;
				}
				if (Input.GetKey (InputCatcher.BackKey)) {
					toMove += camera.transform.forward*-1;
					toSprite += transform.forward*-1;
				}
				if (Input.GetKey (InputCatcher.LeftKey)) {
					toMove += camera.transform.right*-1;	
					toSprite += transform.right*-1;
				}
				if (Input.GetKey (InputCatcher.RightKey)) {
					toMove += camera.transform.right;
					toSprite += transform.right;
				}
				break;
			}
		case (CameraMode.OVERHEAD):
			{
//TODO
				break;
			}
		case (CameraMode.SIDESCROLLER):
			{
				if (Input.GetKey (InputCatcher.LeftKey)) {
					toMove += camera.transform.right*-1;	
					toSprite += transform.right*-1;
				}
				if (Input.GetKey (InputCatcher.RightKey)) {
					toMove += camera.transform.right;
					toSprite += transform.right;
				}
				if (Input.GetKeyDown (InputCatcher.RollKey) && isGrounded()) {
					Debug.Log ("Jump");
				}
				break;
			}

		}

	}

	public bool isGrounded(){
		return(transform.position.y < 2.6); //TODO
	}

	private void move(Vector3 vec){
		if (canMove()) {
			//toMove.y = gameObject.transform.position.y;
			character_controller.Move (toMove.normalized * Time.deltaTime * move_speed);
		}
	}

	public void npcInputToMove(Vector3 i){
		if (canMove () && !lockout) {
			toMove += i;
		}
	}

	public void npcInputToMoveCombat(Vector3 i){
		if (canMove () && lockout) {
			toMove += i;
		}
	}

	public void MoveToPosition(Vector3 i){
		if (canMove ()) {
			Vector3 vec = i - transform.position;
			toMove += vec;
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
