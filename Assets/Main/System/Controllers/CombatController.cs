using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Confidence{ //TODO
	Scared=0,
	Wary=1,
	Neutral=2,
	Confident=3,
	Fearless=4
};

public enum CombatState{ //determined by confidence + health, formation and level
	Flee=0, //run away
	Reactive=1, //maintain distance
	Attacking=2, //close and attack
	Assaulting=3
};

public class CombatController : MonoBehaviour {

	public const int defaultCooldown = 2;
	public float cdTimer;

	bool canAttack = true;

	ActionController actionController;
	MovementController movementController;
	Entity entity;

	public GameObject target;
	public Vector3 targetPos;
	public Vector3 moveInput;

	//random value added to movement so it isnt perfect
	float updateTimer;
	float updateTimerMax = .075f; //max offset in percent
	bool doUpdate = true;

	//better to calculate only a few times a second instead of constantly to make it seem more natural
	float recalcTargetPosCounter = 0f;		//just the counter
	const float recalcTargetPosMax = .2f; // how often to recalculate in seconds
	bool recalculateTargetPos = true;

	public Confidence confidence = Confidence.Neutral;
	public CombatState combatState = CombatState.Reactive;

	public const int maxConfidence = 4; //max confidence value, 
	public const int minConfidence = 0;

	const float fleeDistance = 20f;
	private bool OutsideFleeDistance(){
		return(Vector3.Distance(transform.position, target.transform.position) > fleeDistance);
	}
	private bool OutsideFleeDistance(int i){
		return(Vector3.Distance(transform.position, target.transform.position) > fleeDistance*i);
	}

	const float closeDistance = 2.5f;
	private bool InsideCloseDistance(){
		return(Vector3.Distance(transform.position, target.transform.position) <= closeDistance);
	}
	private bool InsideCloseDistance(int i){
		return(Vector3.Distance(transform.position, target.transform.position) <= closeDistance*i);
	}

	public bool inCombat = false;
	private bool isPlayer = false;

	[SerializeField]float distanceToTarget;

	public bool doAttack = false;
	public void AttackNextFrame()
	{
		doAttack = true;
	}

	// Use this for initialization
	void Start () {
		actionController = gameObject.GetComponent<ActionController> ();
		movementController = gameObject.GetComponent<MovementController> ();
		entity = gameObject.GetComponent<Entity> ();
		isPlayer = movementController.isPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		if (inCombat) {
			if (!canAttack) {
				Cooldown ();
			}
		switch (isPlayer) {
		case(true):
			{
				PlayerLoop ();
				break;
			}
		case(false):
			{
				NPCLoop ();
				break;
			}
		}

			if (doAttack && InsideCloseDistance()) {
				Attack ();
				doAttack = false;
				canAttack = false;
			}
		}
	}

	void Cooldown(){
		cdTimer += Time.deltaTime;
		if (cdTimer >= defaultCooldown) {
			canAttack = true;
			cdTimer = cdTimer - defaultCooldown;
		}
	}

	void PlayerLoop(){

		
	}

	void NPCLoop(){
		if (canAttack) {
			doAttack = true;
		}
		LockoutControl ();
		UpdateCalc();
		if(doUpdate || Random.Range(0,4) == 2)
		TrackTarget ();
	}

	void LockoutControl(){
		if (inCombat) {
			movementController.lockout = true;
		} else {
			movementController.lockout = false;
		}
	}

	void UpdateCalc(){
		updateTimer += Time.deltaTime;
		if (updateTimer >= updateTimerMax) {
			doUpdate = true;
			updateTimer -= updateTimerMax;
		}
	}

	void TrackTarget(){
		doUpdate = false;
		moveInput = new Vector3();
		targetPos = target.transform.position;
		distanceToTarget = Vector3.Distance (transform.position, target.transform.position);
	//	moveInput = Vector3.MoveTowards (gameObject.transform.position, target.transform.position, );
		moveInput = target.transform.position - transform.position;
		if (target != null && movementController.bodyCanMove) {
			switch (combatState) {
			case(CombatState.Flee):{
					if(!OutsideFleeDistance())
						movementController.npcInputToMoveCombat (moveInput.normalized * -1);
					if (doAttack)
						doAttack = false; //cannot attack while fleeing
					break;
				}

			case(CombatState.Reactive):{
					//close
					if (!InsideCloseDistance (3)) {
						movementController.npcInputToMoveCombat (moveInput.normalized  * 1);
					}
					//but not too close
					else if (InsideCloseDistance(2) && !doAttack) {
						movementController.npcInputToMoveCombat (moveInput.normalized  * -1);
					}
					break;
			}		
			
			case(CombatState.Attacking):{
					if (!InsideCloseDistance (2)) {
						movementController.npcInputToMoveCombat (moveInput.normalized * 1);
					}
					else if (!doAttack && InsideCloseDistance() ) {
						movementController.npcInputToMoveCombat (moveInput.normalized * -1);
					}
					break;
			}			
			
			case(CombatState.Assaulting):{
					if (!InsideCloseDistance()) {
						movementController.npcInputToMoveCombat (moveInput.normalized * 1);
					}
					break;
				}
			}
			if(doAttack)
				movementController.npcInputToMoveCombat (moveInput.normalized  * 1);
		}
	}


	void Attack(){
		actionController.tryAttack ();
	}
}
