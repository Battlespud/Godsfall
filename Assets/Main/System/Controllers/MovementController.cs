﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour {




	//Dependencies:
		// Only the main camera that follows the player should be active at the start of the scene. 
		// Character Controller component must be present on same gameobject.
	//Can be used on vehicles, npcs and player without any changes (aside from setting the "Player" bool to negative, and disabling gravity for
	// vehicles like boats.
	#region declarations

	//speed for navmesh
	//TODO Make an enum
	public const int WALK = 3;
	public const int RUN = 5;
	public const int SPRINT = 7;

	//Camera
	public Camera camera;
	public CameraModeManager camModeManager;

	//Refs
	CharacterController character_controller;
	SpriteController spriteController;
	public Animator animator; //TODO
	NavMeshAgent agent;

	//Author Must Setup
	public bool isPlayer;
	[Tooltip("When active, only instructions to the actor will be followed, toMove modifications will be ignored.")]
	public bool useNavMeshAgent;

	public bool arrived = true;
	//Automated
	public bool isMoving = false;
	public bool hasSpriteController = false;
	[Tooltip("Blocks all input, used while under remote control from a master block (ie formation) or when being knocked back or stunned")]
	public bool lockout = false; //use to lockout input except from combat controller
	[Tooltip("Damage to limbs can reduce or remove ability to move.")]
	public bool bodyCanMove = true;
	bool disabled = false;

	//Constants
	const float GRAVITY = -.4f; //acceleration
	public const float BASESPEED = 11f;
	[Tooltip("Current Velocity from gravity.")]
	[SerializeField]float currGravity;

	//Floats
	public List<float> MovementSpeedModifiers;
	public float moveSpeedModifier;

	//Gravity
	//Polled every couple frames and applied as needed.
	[Tooltip("Gravity performance cost is negligible, its recommended to leave this enabled for most things")]
	[SerializeField]private bool usesGravity = true; //Select or Unselect in editor to determine if this object will use gravity automatically.
	[Tooltip("Gravity is currently being applied.  Also can function as a grounded bool.")]
	[SerializeField]private bool gravityEnabled = false;

	private const float Interval = 60; // Every 1/Interval a second, gravity will be checked.  TODO make a seperate interval for npc because we care less
	private float gravityTimer;
	private float mGravityTimer; //autocalculated from Interval to save us from the division every frame.
	private float gravityCheckDistance;
	private const float gravityCheckOffsetDistance = .15f; //check this far from the bottom.  .2f or so is a good number since the colliders arent usually touching the ground.
	private Ray GravityCheckingRay;


//dont touch, non agent
    Vector3 toMove;
    Vector3 toSprite;

//Agent stuff
	[Tooltip("Approximately where the Actor will attempt to navigate, must be near navmesh.")]
	[SerializeField]public Vector3 destination;

//Physics Emulation
	float mass = 10f;
	float jumpForce = 8f;
	Vector3 impact;

	#endregion


	#region randomass methods that should go somewhere else

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

	private Vector3 TimeAdjusted(Vector3 v){
		return (v * Time.deltaTime);
	}

	#endregion

	#region Initialization

	// Use this for initialization
	void Start () {
		GrabReferences ();
		GravitySetup ();
		AgentSetup ();
	}



	void GrabReferences(){
			camera = Camera.main;
			camModeManager = camera.gameObject.GetComponent<CameraModeManager> ();
		character_controller = gameObject.GetComponent<CharacterController> ();
			if (GetComponentInChildren<SpriteController> () != null) {
				spriteController = GetComponentInChildren<SpriteController> ();
				hasSpriteController = true;
			}
		if (GetComponent<NavMeshAgent> () != null) {
			agent = GetComponentInChildren<NavMeshAgent> ();
		}
			camera = Camera.main;
			MovementSpeedModifiers = new List<float> ();
		impact = new Vector3 ();
		}

	void GravitySetup(){
		if (!usesGravity) {
			gravityEnabled = false;
		} else {
			mGravityTimer = 1 / Interval;
			gravityCheckDistance = GetComponent<Collider> ().bounds.size.y / 2 + gravityCheckOffsetDistance;
		}
	}

	void AgentSetup()
	{
		if (agent != null) {
			agent.angularSpeed = 0f; //no rotating
		} else {
			useNavMeshAgent = false;
		}
	}

	#endregion
	#region update
	// Update is called once per frame
	void Update () {
		if (agent != null)
			agent.enabled = useNavMeshAgent;
		character_controller.enabled = !useNavMeshAgent;
		if (!useNavMeshAgent) {
			GravityLoop ();
			Collisions ();
			switch (isPlayer) {
			case (true):
				{
					if (!lockout)
						checkMovementInput ();
					CheckMoving ();
					Move ();
					spriteController.UpdateSprite (toSprite, true);
					break;
				}
			case (false):
				{
					Move ();
					CheckMoving ();
					if (hasSpriteController)
						spriteController.UpdateSprite (toMove, false); //TODO this causes problems with diagonal movement
					break;
				}
			}
			clearBuffer ();
		} else {
			//NavMeshAgentController

			spriteController.UpdateSprite (agent.velocity, false);
			if (canMove () && destination != null && !arrived) {
				agent.isStopped = false;
				agent.destination = destination;
			} else {
				agent.isStopped = true;
			}
			if (agent.remainingDistance <= agent.stoppingDistance) {
				arrived = true;
			}
		}
	}
	#endregion
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
				currGravity += GRAVITY * Time.deltaTime;
				ForceMove (new Vector3 (0f, currGravity, 0f));
			} else {
				currGravity = 0f; //reset to base acceleration
			}
		} else {
			gravityEnabled = false; //in case we change mid game for some reason
		}
	}

	private void CheckGravity(){ //TODO literally just doesnt work idfk why
		RaycastHit hit;

		//Debug.DrawRay (transform.position, Vector3.down * gravityCheckDistance); 
		GravityCheckingRay = new Ray (transform.position, Vector3.down);
		if (Physics.Raycast (GravityCheckingRay, out hit, gravityCheckDistance) ) {
			if (hit.collider.transform.parent != gameObject && hit.collider.transform.gameObject != gameObject) {
				if (isPlayer) {
				//	Debug.Log (hit.collider.name);
				}
				gravityEnabled = false;
			} else {
				gravityEnabled = true;
			}
		} else {
			gravityEnabled = true;
		}
	}

	#endregion 

	//processes collisions and locks out controls temporarily (might bug ai in formation, needs testing)
	void Collisions(){
		if (impact.magnitude > .1f) {
			lockout = false;
			ForceMove ( (impact));
			impact = Vector3.Lerp (impact, Vector3.zero, 5 * Time.deltaTime);
		} else {
			lockout = false;
		}
	}

	public void AddImpact(Vector3 dir, float force){
		dir.Normalize ();
		if (dir.y < 0)
			dir.y = -dir.y;
		impact += dir.normalized * force / mass;
	}

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
					PlayerInputToMove(camera.transform.forward);
					toSprite += transform.forward;
				}
				if (Input.GetKey (InputCatcher.BackKey)) {
					PlayerInputToMove(camera.transform.forward*-1);
					toSprite += transform.forward*-1;
				}
				if (Input.GetKey (InputCatcher.LeftKey)) {
					PlayerInputToMove(camera.transform.right*-1);	
					toSprite += transform.right*-1;
				}
				if (Input.GetKey (InputCatcher.RightKey)) {
					PlayerInputToMove(camera.transform.right);
					toSprite += transform.right;
				}
				if (Input.GetKeyDown (InputCatcher.RollKey) && !gravityEnabled) {
					gravityEnabled = true;
					AddImpact(transform.up, jumpForce);
				}
				break;
			}
		case (CameraMode.OVERHEAD):
			{
				if (Input.GetKey (InputCatcher.ForwardKey)) {
					PlayerInputToMove(transform.forward);
					toSprite += transform.forward;
				}
				if (Input.GetKey (InputCatcher.BackKey)) {
					PlayerInputToMove(transform.forward*-1);
					toSprite += transform.forward*-1;
				}
				if (Input.GetKey (InputCatcher.LeftKey)) {
					PlayerInputToMove(transform.right*-1);	
					toSprite += transform.right*-1;
				}
				if (Input.GetKey (InputCatcher.RightKey)) {
					PlayerInputToMove(transform.right);
					toSprite += transform.right;
				}
				break;
			}
		case (CameraMode.SIDESCROLLER):
			{
				if (Input.GetKey (InputCatcher.LeftKey)) {
					PlayerInputToMove(camera.transform.right*-1);	
					toSprite += transform.right*-1;
				}
				if (Input.GetKey (InputCatcher.RightKey)) {
					PlayerInputToMove(camera.transform.right);
					toSprite += transform.right;
				}
				if (Input.GetKeyDown (InputCatcher.RollKey) && !gravityEnabled) {
					gravityEnabled = true;
					AddImpact(transform.up, jumpForce);
				}
				break;
			}

		}

	}

	float MoveSpeed(){
		return BASESPEED + moveSpeedModifier;
	}

	//dont call this directly, add to toMove instead
	private void Move(){
		if (canMove()) {
			character_controller.Move (toMove.normalized * TimeAdjusted(MoveSpeed()));
		}
	}

	//For physics only
	private void ForceMove(Vector3 forcedVec){
			character_controller.Move (forcedVec);
	}

	public void PlayerInputToMove(Vector3 i){
		i.y = 0;
		toMove += i;
	}

	public void InputToMoveY(float f)
	{
		toMove.y += f;
	}

	public void BypassPlayerInputToMove(Vector3 i){
	//	i.y = 0;
		toMove += i;
	}

	public void agentInputToMove(Vector3 vec)
	{
		destination = vec;
		arrived = false;
//		Debug.Log ("New destination: " + destination);
	}

	public void agentInputToMove(Vector2 vec)
	{
		destination = new Vector3 (vec.x, transform.position.y, vec.y);
		arrived = false;
	//	Debug.Log ("New destination: " + destination);
	}

	public Vector3 GetDestination(){
		return destination;
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

	public void AgentInputDirectionalMovement(Vector3 vec){
		agent.Move (vec*Time.deltaTime);
	}

	public void AgentAdjustSpeed(int i){
		agent.speed = i;
	}

	//pretty sure we legit never use this
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


