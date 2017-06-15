using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis{
	X=0,
	Y=1,
	Z=2
};

public class PatrolBehaviour : MonoBehaviour , IEventInitializer {

	public Axis axis = Axis.X;

	StringEvent OnPatrolEvent;
	public MovementController movementController;

	//for detecting collision events
	GameObject go;

	public bool paused =false;

	//pauses at end of patrol
	public float timeToPause = 2f;

	public float speedMultiplier = 3;

	private const string endpointTag = ("PatrolEndpoint");

	//axis to travel along

	public int heading = 1;

	public int Heading {
		get {
			return heading;
		}
		set {
			heading = heading * -1;
			AnnounceNewPatrol ();
		}
	}

	public void DirectSet(int head, Axis ax){
		heading = head;
		axis = ax;
	}



	string patrolString;

	// Use this for initialization
	void Start () {
		patrolString = "I'm on Patrol!";
		movementController = this.gameObject.GetComponent<MovementController> ();
		initializeEvents ();
		go = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (!paused) {
			switch (axis) {
			case Axis.X:
				movementController.npcInputToMove (new Vector3 (heading * Time.fixedDeltaTime * speedMultiplier, 0, 0));
				break;
			case Axis.Y:
				movementController.npcInputToMove (new Vector3 (0, heading * Time.fixedDeltaTime * speedMultiplier, 0));
				break;
			case Axis.Z:
				movementController.npcInputToMove (new Vector3 (0, 0, heading * Time.fixedDeltaTime * speedMultiplier));
				break;

			}
		}
	}


	public void TurnAround(){
		heading = heading*-1;
		paused = true;
		Invoke ("Unpause", timeToPause);
	}

	void Unpause(){
		paused = false;
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
