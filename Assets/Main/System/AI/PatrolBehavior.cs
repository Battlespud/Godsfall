using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatrolBehavior : MonoBehaviour , IEventInitializer{

	StringEvent OnPatrolEvent;
	MovementController movementController;

	float patrolTimer = 0;
	const float patrolTimerM = 5;

	private int heading = 1;

	public int Heading {
		get {
			return heading;
		}
		set {
			heading = heading * -1;
			AnnounceNewPatrol ();
		}
	}


	string patrolString;

	// Use this for initialization
	void Start () {
		patrolString = "I'm on Patrol!";
		movementController = this.gameObject.GetComponent<MovementController> ();
		initializeEvents ();
	}
	
	// Update is called once per frame
	void Update () {
		if (patrolTimer <= 0) {
			resetTimer();
		}
		movementController.npcInputToMove (new Vector3 (heading, 0, 0));
		patrolTimer -= Time.deltaTime;
	}


	private void resetTimer(){
		patrolTimer = patrolTimerM;
		heading = heading*-1;
	}

	private void AnnounceNewPatrol(){
		OnPatrolEvent.Invoke (patrolString);
	}


	//implement Interface
	public void initializeEvents(){
		OnPatrolEvent = new StringEvent();
		OnPatrolEvent.AddListener (Announcer.AnnounceString);	
	}

}
