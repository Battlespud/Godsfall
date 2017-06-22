using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationSlave : MonoBehaviour {

	[SerializeField]FormationMaster master; //drag and drop in inspector. will register itself to list.

	public GameObject go;
	public Entity entity; //we'll use this to monitor body, movement ability etc 
	public MovementController mc;
	public Body body;
	public FormationRoles role; //which line we go into, front or rear.
	public Collider mainCollider;

	public bool inFormation = false;
	public Vector3 formationPosition; //where this soldier needs to go
	public int rank = 1; //which line this soldier stands in, 1 is first

	private Vector3 debugFacingVector = new Vector3 (0f, 0f, -1f); //make them all face hte same way

	Vector3 closeEnough = new Vector3(.25f,.25f,.25f);
	float closeEnoughFloat = .3f; //a value around .4 or .5 will work, but requires a lower than average spread to ensure no gaps
	// Use this for initialization
	void Awake(){
		go = this.gameObject;
		entity = this.gameObject.GetComponent<Entity> ();
		mainCollider = gameObject.GetComponent<CapsuleCollider> ();
		master.Register (this);
	}

	void Start () {
		mc = gameObject.GetComponent<MovementController> ();
		body = entity.body;
	}
	
	// Update is called once per frame
	void Update () {
		if (!inFormation && formationPosition != null) {
			mc.MoveToPosition (formationPosition);
		}
		if (Vector3.Distance (transform.position, formationPosition) <= closeEnoughFloat && !inFormation) {
			inFormation = true;
		//	Debug.Log ("I made it!");
		//	mc.npcInputToMove (debugFacingVector);
		}
	}
}
