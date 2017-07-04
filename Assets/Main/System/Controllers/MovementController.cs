using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {


	//Dependencies:
		// Only the main camera that follows the player should be active at the start of the scene. 
		// Character Controller component must be present on same gameobject.
	//Can be used on vehicles, npcs and player without any changes (aside from setting the "Player" bool to negative, and disabling gravity for
	// vehicles like boats.

	//Camera
	public Camera camera;
	public CameraModeManager camModeManager;

	CharacterController character_controller;
	SpriteController spriteController;
	public Animator animator; //TODO

	//Author Must Setup
	public bool isPlayer;

	//Automated
	public bool isMoving = false;
	public bool hasSpriteController = false;
	public bool lockout = false; //use to lockout input except from combat controller
	public bool bodyCanMove = true;
	bool disabled = false;

	//Constants
	const float GRAVITY = -2f;
	public const float BASESPEED = 7f;

	//Floats
	public List<float> MovementSpeedModifiers;
	public float moveSpeedModifier;

	//Gravity
	//Polled every couple frames and applied as needed.
	[SerializeField]private bool usesGravity = true; //Select or Unselect in editor to determine if this object will use gravity automatically.
	[SerializeField]private bool gravityEnabled = false;
	private bool grounded = false;
		public bool IsGrounded(){
			return grounded;
		}
	private const float Interval = 10; // Every 1/Interval a second, gravity will be checked.  TODO make a seperate interval for npc because we care less
	private float gravityTimer;
	private float mGravityTimer; //autocalculated from Interval to save us from the division every frame.
	private float gravityCheckDistance;
	private const float gravityCheckOffsetDistance = 5f; //check this far from the bottom.  .2f or so is a good number since the colliders arent usually touching the ground.
	private Ray gravityCheckingRay;


//dont touch
    Vector3 toMove;
    Vector3 toSprite;


    //is the body damaged? ie, missing legs, broken bones etc. Set via event from Body.cs
    public void setBodyCanMove(bool b){
		bodyCanMove = b;
	}

	//are we disabled from a stun, sleep, paralysis or some other short term effect? Will be Set via event from Actor.cs
	public void setDisabled(bool b){
		disabled = b;
	}

	//combine all our various things that can affect whether we move or not
	bool canMove(){
		return (!disabled && bodyCanMove);
	}

	private float TimeAdjusted(float f){
		return (f * Time.deltaTime);
	}

	#region Initialization

	// Use this for initialization
	void Start () {
		GrabReferences ();
		GravitySetup ();
	}



	void GrabReferences(){
			camera = Camera.main;
			camModeManager = camera.gameObject.GetComponent<CameraModeManager> ();
		character_controller = gameObject.GetComponent<CharacterController> ();
			if (GetComponentInChildren<SpriteController> () != null) {
				spriteController = GetComponentInChildren<SpriteController> ();
				hasSpriteController = true;
			}
			camera = Camera.main;
			MovementSpeedModifiers = new List<float> ();
		}

	void GravitySetup(){
		if (!usesGravity) {
			gravityEnabled = false;
		} else {
			mGravityTimer = 1 / Interval;
			gravityCheckDistance = GetComponent<Collider> ().bounds.size.y / 2 + gravityCheckOffsetDistance;
			Ray GravityCheckingRay = new Ray (transform.position, Vector3.down);
		}
	}

	#endregion

	// Update is called once per frame
	void Update () {
		GravityLoop ();
		switch (isPlayer) {
		case (true):
			{
				checkMovementInput ();
				CheckMoving ();
				Move ();
				break;
			}
		case (false):
			{
				Move ();
				CheckMoving ();
				break;
			}
		}
		if (hasSpriteController) {
			if (isPlayer && isMoving) {
				spriteController.UpdateSprite (toSprite, true);
			} 
			else if(!isPlayer && isMoving) {
				spriteController.UpdateSprite (toMove, false); //TODO this causes problems with diagonal movement
			}
		}
		clearBuffer ();
	}

	private void clearBuffer(){
		toMove = new Vector3 (0f, 0f, 0f);
		toSprite = new Vector3 (0f, 0f, 0f);
	}


	#region Gravity

	private void GravityLoop(){
		if (usesGravity) {
			gravityTimer += Time.fixedUnscaledDeltaTime;
			if (gravityTimer >= mGravityTimer) {
				gravityTimer -= mGravityTimer;
				CheckGravity ();
			}
			if (gravityEnabled) {
				toMove.y += GRAVITY;
			}
		} else {
			gravityEnabled = false; //in case we change mid game for some reason
		}
	}

	private void CheckGravity(){ //TODO literally just doesnt work idfk why
		RaycastHit hit;

		Debug.DrawRay (transform.position, Vector3.down * gravityCheckDistance); 

		if (Physics.Raycast (gravityCheckingRay, out hit, 10f)) {
			Debug.Log (hit.collider.name);
			gravityEnabled = false;
		} else {
			gravityEnabled = true;
		}
	}

	#endregion 

	private void CheckMoving(){
		if (toMove != new Vector3 (0, 0, 0)) {
			isMoving = true;
			moveSpeedModifier = 0f;
			foreach (float flo in MovementSpeedModifiers) {
				moveSpeedModifier += flo;
			}
		} else {
			isMoving = false;
		}
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
				if (Input.GetKeyDown (InputCatcher.RollKey) && IsGrounded()) {
					Debug.Log ("Jump");
				}
				break;
			}

		}

	}

	float MoveSpeed(){
		return BASESPEED + moveSpeedModifier;
	}

	private void Move(){
		if (canMove()) {
			character_controller.Move (toMove.normalized * TimeAdjusted(MoveSpeed()));
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
