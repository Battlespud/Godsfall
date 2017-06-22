using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FormationTypes{
	LINE,
	SQUARE
};

public enum FormationRoles{
	FRONT,
	REAR
};


public struct Formation{
	public int troops; //number of troops
	public float spread; //how far soldiers are from each other
	public FormationTypes formationType;
	public Axis axis; //axis along which to form formation if applicable

	//Constructor
	public Formation(FormationTypes typ, int i, float spr, Axis ax)
	{
		troops = i;
		formationType = typ;
		spread = spr; //a gap of 1.5f with a closeEnoughFloat of .3f provides a solid spear wall that cant be walked through, and seems pretty stable.
		axis = ax;
	}
}




//CLASS STARTS BELOW DONT PUT STUFF UP HERE!!



public class FormationMaster : MonoBehaviour {

	//Collections
	List<FormationSlave> slaves = new List<FormationSlave>();
	List<Vector3> vectors = new List<Vector3> ();

	//TODO
	Formation formation;

	//Assign in inspector
	public FormationSlave captain; //who all the troops will gather around.

	//Ints/Floats
	float distanceBetweenRanks = 5f; //how far between each line of soldiers


	//Bools
	[SerializeField]bool formationFinished = false; //everyone has moved into position so we can move around now

	//Editor Testing Buttons
	public bool ChangeAxis = false;

	//						Optional SpeechModule Integration
	//Does NOT require that you manually add speech slaves or anything else.  To enable,
	//simply place a speechmaster anywhere in the scene and drag and drop to this script in inspector.
	//Adding slaveModules and registering them will be handled automatically once the game starts.
	//The Captain will not be registered to the slave module however, so their collider will still be active.

	bool speechModeEnabled = false;
	public SpeechMaster formationSpeechMaster; //the troops will  yell back
	public SpeechModule captainSpeech; //the captain will yell commands






	// Use this for initialization
	void Start () {
		formation = new Formation (FormationTypes.LINE, slaves.Count, 1.5f, Axis.X); //just a default one for testing
		if (formationSpeechMaster != null) {
			HandleSpeechSetup ();
		}
		AssignPositions ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!formationFinished) {
			bool weDidIt = true;
			foreach (FormationSlave slave in slaves) {
				if (!slave.inFormation) {
					weDidIt = false;
				}
			}
			formationFinished = weDidIt;
			if (formationFinished) {
				EnableColliders ();
				switch (formation.axis) {
				case(Axis.X):
					{
						MassMove(new Vector3(0,0f,-2f));
						break;
					}
				case(Axis.Z):
					{
						MassMove(new Vector3(-2f,0f,0f));
						break;
					}
				}
			}
		}
	}


	void AssignPositions(){
		DisableColliders ();
		Vector3 basePosition = captain.transform.position;
		int[] incrementsEven = new int[100];
		//evens
		for (int i = 0; i < slaves.Count; i += 2) {
			FormationSlave slave = slaves [i];
			if (slave != captain) {
				switch (formation.axis) {
				case(Axis.X):
					{
						slaves [i].formationPosition = new Vector3 (basePosition.x + (formation.spread *(1+ incrementsEven[slave.rank])), basePosition.y, basePosition.z-(distanceBetweenRanks*slaves[i].rank));
						break;
					}
				case(Axis.Z):
					{
						slaves [i].formationPosition = new Vector3 (basePosition.x-(distanceBetweenRanks*slaves[i].rank), basePosition.y, basePosition.z + (formation.spread *(1+ incrementsEven[slave.rank])));

						break;
					}
				}
				incrementsEven[slave.rank]++;
			}
		}

		int[] incrementsOdd = new int[100];
		//odds
		for (int i = 1; i < slaves.Count; i += 2) {
			FormationSlave slave = slaves [i];
			if (slave != captain) {
				switch (formation.axis) {
				case(Axis.X):
					{
						slaves [i].formationPosition = new Vector3 (basePosition.x - (formation.spread * (1 + incrementsOdd[slave.rank])), basePosition.y, basePosition.z-(distanceBetweenRanks*slaves[i].rank));
						break;
					}
				case(Axis.Z):
					{
						slaves [i].formationPosition = new Vector3 (basePosition.x-(distanceBetweenRanks*slaves[i].rank), basePosition.y, basePosition.z - (formation.spread * (1 + incrementsOdd[slave.rank])));

						break;
					}

				}
				incrementsOdd[slave.rank]++;
			}
		}

		captain.formationPosition = captain.transform.position;

	}





	void MassMove(Vector3 moveOrder){
		foreach(FormationSlave slave in slaves)
		{
			slave.mc.npcInputToMove (moveOrder);
		}
	}




	void EnableColliders(){
		foreach (FormationSlave slave in slaves) {
			slave.go.layer = 0;

		}
	}

	void DisableColliders(){
		foreach (FormationSlave slave in slaves) {
			slave.go.layer = 31;
		}
	}


	public void Register(FormationSlave slave){
		slaves.Add (slave);
	}

	public void DeRegister(FormationSlave slave){
		slaves.Remove (slave);
	}








	//speech
	void HandleSpeechSetup(){
		foreach (FormationSlave slave in slaves) {
			if (slave != captain) {
				SlaveSpeechModule slav = slave.GetComponentInChildren<SpeechModule> ().gameObject.AddComponent<SlaveSpeechModule> ();
				slav.ForceRegister (formationSpeechMaster);
			} else {
				captainSpeech = slave.GetComponentInChildren<SpeechModule> ();
			}
		}
		speechModeEnabled = true;
	}
}
