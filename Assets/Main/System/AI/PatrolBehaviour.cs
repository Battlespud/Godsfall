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

	private Vector3 guidanceVec;
	private Vector3 correctionVec;
	public bool guidanceActive = false;
	public float guideScale = .5f;
	public float guideStep = .25f;

	[SerializeField]bool xBeingCorrected = false;
	[SerializeField]bool yBeingCorrected = false;
	[SerializeField]bool zBeingCorrected = false;

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
		updateGuidance ();
	}



	string patrolString;

	// Use this for initialization
	void Start () {
		patrolString = "I'm on Patrol!";
		movementController = this.gameObject.GetComponent<MovementController> ();
		initializeEvents ();
		go = this.gameObject;
		guidanceVec = new Vector3 (0f,0f,0f);
		updateGuidance ();
	}

	// Update is called once per frame
	void Update () {
		if (!paused) {
			Vector3 toMove = new Vector3 ();
			switch (axis) {
			case Axis.X:
				toMove = new Vector3 (heading * Time.fixedDeltaTime * speedMultiplier, 0, 0);
				break;
			case Axis.Y:
				toMove = new Vector3 (0, heading * Time.fixedDeltaTime * speedMultiplier, 0);
				break;
			case Axis.Z:
				toMove = new Vector3 (0, 0, heading * Time.fixedDeltaTime * speedMultiplier);
				break;

			}
			courseCorrection ();
			toMove += correctionVec;
			Debug.Log (correctionVec);
			movementController.npcInputToMove (toMove);

		}
	}

	void courseCorrection(){
		xBeingCorrected = false;yBeingCorrected = false;zBeingCorrected = false;
		correctionVec = new Vector3 (0f,0f,0f);
		if (guidanceActive) {
			switch (axis) {
			case Axis.X:
				if (Mathf.Abs(transform.position.z - guidanceVec.z) > guideScale) {
					guidanceFixZ ();
				}
				if (Mathf.Abs(transform.position.y - guidanceVec.y) > guideScale) {
					guidanceFixY ();
				}
				break;

			case Axis.Y:
				if (Mathf.Abs(transform.position.x - guidanceVec.x) > guideScale) {
					guidanceFixX ();
				}
				if (Mathf.Abs(transform.position.z - guidanceVec.z) > guideScale) {
					guidanceFixZ ();
				}
				break;

			case Axis.Z:
				if (Mathf.Abs(transform.position.x - guidanceVec.x) > guideScale) {
					guidanceFixX ();
				}
				if (Mathf.Abs(transform.position.y - guidanceVec.y) > guideScale) {
					guidanceFixY ();
				}
				break;

			}
		}
	}

	void guidanceFixX(){
		//too +x
		if (transform.position.x > guidanceVec.x) {
			correctionVec.x -= guideStep;
		}
		//too -x
		if (transform.position.x < guidanceVec.x) {
			correctionVec.x += guideStep;
		}
		xBeingCorrected = true;
	}

	void guidanceFixY(){
		//too +y
		if (transform.position.y > guidanceVec.y) {
		//	correctionVec.y -= guideStep;
		}
		//too -y
		if (transform.position.y < guidanceVec.y) {
		//	correctionVec.y += guideStep;
		}
		yBeingCorrected = true;
	}

	void guidanceFixZ(){
		//too +z
		if (transform.position.z > guidanceVec.z) {
			correctionVec.z -= guideStep;
		}
		//too -z
		if (transform.position.z < guidanceVec.z) {
			correctionVec.z -= guideStep;
		}
		zBeingCorrected = true;
	}

	void updateGuidance(){
		Debug.Log ("Updating Guidance");
		guidanceVec = go.transform.position;
		Invoke ("Unpause", timeToPause);
		}

	public void TurnAround(){
		heading = heading*-1;
		updateGuidance ();
		paused = true;
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
