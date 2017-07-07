using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationSlave : MonoBehaviour {

	[SerializeField]FormationMaster master; //drag and drop in inspector. will register itself to list.


	public bool UseNavMeshAgent;

	public MovementController mc;
	public bool inFormation = false;
	public Vector3 formationPosition; //where this soldier needs to go
	public Vector2 AgentFormationPosition;
	public float distanceToTarget;
	public int rank = 1; //which line this soldier stands in, 1 is first

	[Tooltip("Represents position in formation.  Centered on the Captain, X represents how far to the Captain's left or right this troop is in terms of soldiers (not in terms of actual Unity units, so this cant be used for navigation on its own), 0 is inline with the captain." +
		"Y represents which rank, negative is behind the captain, 0 is inline and + is in front.   ")]
	public Vector2 FormationSlot;

	private Vector3 debugFacingVector = new Vector3 (0f, 0f, -1f); //make them all face hte same way

	Vector3 closeEnough = new Vector3(.25f,.25f,.25f);
	float closeEnoughFloat = .3f; //a value around .4 or .5 will work, but requires a lower than average spread to ensure no gaps
	// Use this for initialization
	void Awake(){
		UseNavMeshAgent = master.UseNavMeshAgent;
		if(master!=null)
		master.Register (this);
	}

	void Start () {
		mc = gameObject.GetComponent<MovementController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!UseNavMeshAgent) {

			if (!inFormation && formationPosition != null) {
				mc.MoveToPosition (formationPosition);
			}
			if (Vector3.Distance (transform.position, formationPosition) <= closeEnoughFloat && !inFormation) {
				inFormation = true;
			}
		} else {
			if(!inFormation)
				mc.agentInputToMove (AgentFormationPosition);
			distanceToTarget = Vector3.Distance (transform.position, AgentFormationPosition);
			if (Vector3.Distance (transform.position, formationPosition) <= closeEnoughFloat && !inFormation) {
				inFormation = true;
			}



		}
	}

	public void AssignPosition(){
	}
}
