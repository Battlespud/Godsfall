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



[RequireComponent(typeof(Formation))]
public class FormationMaster : MonoBehaviour {

	//DEPENDENCY:
	//A Formation component must be present on this GameObject and properly configured.

	//Collections
	List<FormationSlave> slaves = new List<FormationSlave>();
	List<Vector3> vectors = new List<Vector3> ();
	public Dictionary<Vector2,FormationSlave> FormationMatrix;
	public bool UseNavMeshAgent = true;

	Formation formation;

	float timeKeyDown = 0f;
	float mTimeKeyDown = .2f;

	//Assign in inspector
	public FormationSlave captain; //who all the troops will gather around.

	//Ints/Floats
	const int maxRanks = 100;

	//Bools
	[SerializeField]bool formationFinished = false; //everyone has moved into position so we can move around now

//Deprecated
	public bool ChangeAxis = false;

	//						Optional SpeechModule Integration
	//Does NOT require that you manually add speech slaves or anything else.  To enable,
	//simply place a speechmaster on this gameobject.
	//Adding slaveModules and registering them will be handled automatically once the game starts.
	//The Captain will not be registered to the slave module however, so their collider will still be active.

	bool speechModeEnabled = false;
	public SpeechMaster formationSpeechMaster; //the troops will  yell back
	public SpeechModule captainSpeech; //the captain will yell commands

	public List<Vector2> listOfFormationSlots;

	// Use this for initialization
	void Start () {
	//	formation = new Formation (FormationTypes.LINE, slaves.Count, 1.5f, Axis.X); //just a default one for testing
		formation = GetComponent<Formation>();
		formationSpeechMaster = GetComponent<SpeechMaster> ();
		if (formationSpeechMaster != null) {
			HandleSpeechSetup ();
		}
		Debug.Log ("Assigning positions..");
		listOfFormationSlots = new List<Vector2> ();
		AssignPositions();

	}
	
	// Update is called once per frame
	void Update () {
		if (!UseNavMeshAgent) {
			if (ChangeAxis) {
				ChangeFormationAxis ();
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				MassMove (transform.forward);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				MassMove (transform.forward * -1);
			}

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
							MassMove (new Vector3 (0, 0f, -2f));
							break;
						}
					case(Axis.Z):
						{
							MassMove (new Vector3 (-2f, 0f, 0f));
							break;
						}
					}
				}
			}
		} else {


			//NavMeshSection
			if (Input.GetKey (KeyCode.UpArrow)) {
				timeKeyDown += Time.deltaTime;
				MassMove (transform.forward);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				timeKeyDown += Time.deltaTime;
				MassMove (transform.forward * -1);
			}
			if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.UpArrow) || timeKeyDown >= mTimeKeyDown) {
				timeKeyDown = 0f;
				AssignMovementOrders ();
			}

		}
	}


	void ChangeFormationAxis(){
		if (formation.axis == Axis.X) {
			formation.axis = Axis.Z;
		} else {
			formation.axis = Axis.X;
		}
		formationFinished = false;
		AssignPositions ();
		ChangeAxis = false;
	}

	#region AssignPositions()
	void AssignPositions(){
		if (!UseNavMeshAgent) {
			DisableColliders ();
			Vector3 basePosition = captain.transform.position;
			foreach (FormationSlave slave in slaves) {
				slave.inFormation = false;
			}
			switch (formation.formationType) {

			case(FormationTypes.LINE):
				{
					int[] incrementsEven = new int[maxRanks];
					//evens
					for (int i = 0; i < slaves.Count; i += 2) {
						FormationSlave slave = slaves [i];
						//slave.formationSlot = i;
						if (slave != captain) {
							switch (formation.axis) {
							case(Axis.X):
								{
									slaves [i].formationPosition = new Vector3 (basePosition.x + (formation.xSpread * (1 + incrementsEven [slave.rank])), basePosition.y, basePosition.z - (formation.zSpread * slaves [i].rank));
									break;
								}
							case(Axis.Z):
								{
									slaves [i].formationPosition = new Vector3 (basePosition.x - (formation.zSpread * slaves [i].rank), basePosition.y, basePosition.z + (formation.xSpread * (1 + incrementsEven [slave.rank])));

									break;
								}
							}
							incrementsEven [slave.rank]++;
						}
					}

					int[] incrementsOdd = new int[maxRanks];
					//odds
					for (int i = 1; i < slaves.Count; i += 2) {
						FormationSlave slave = slaves [i];
						//	slave.formationSlot = i;
						if (slave != captain) {
							switch (formation.axis) {
							case(Axis.X):
								{
									slaves [i].formationPosition = new Vector3 (basePosition.x - (formation.xSpread * (1 + incrementsOdd [slave.rank])), basePosition.y, basePosition.z - (formation.zSpread * slaves [i].rank));
									break;
								}
							case(Axis.Z):
								{
									slaves [i].formationPosition = new Vector3 (basePosition.x - (formation.zSpread * slaves [i].rank), basePosition.y, basePosition.z - (formation.xSpread * (1 + incrementsOdd [slave.rank])));

									break;
								}

							}
							incrementsOdd [slave.rank]++;
						}
					}
					break;
				}

			case(FormationTypes.SQUARE):
				{
					break;
				}
			}
			captain.formationPosition = captain.transform.position;

		} else {


				//NavMeshSection
			switch (formation.formationType) {
			case(FormationTypes.LINE):
				{

					int maxSoldiers = slaves.Count;
					int maxIndex = maxSoldiers - 1;
					int ranks = formation.ranks;
					int maxSoldiersPerRank = Mathf.CeilToInt (maxSoldiers / ranks);

					int soldierCount = 0;

					//start adding from the bottom left, go across then next rank
					FormationMatrix = new Dictionary<Vector2, FormationSlave>();
					int lastRank;
					for (int rank = ranks; rank > 0; rank--) {
						for (int i = Mathf.CeilToInt (0f - maxSoldiersPerRank * .5f); i <= Mathf.CeilToInt (0f + maxSoldiersPerRank * .5f); i++) {
							if (soldierCount <= maxIndex) {
								slaves [soldierCount].AssignPosition (i, rank);
								Debug.Log (soldierCount);
								soldierCount++;
							}
						}
					}

					captain.FormationSlot = new Vector2 (0, 0);

					foreach (FormationSlave slave in slaves) {
						FormationMatrix.Add(slave.FormationSlot, slave);
					}

					AssignMovementOrders ();

					/*

					int[] incrementsEven = new int[maxRanks];
					//evens
					Vector3 basePosition = captain.transform.position;
					for (int i = 0; i < slaves.Count; i += 2) {
						FormationSlave slave = slaves [i];
						//slave.formationSlot = i;
						if (slave != captain) {
							switch (formation.axis) {
							case(Axis.X):
								{
									slaves [i].formationPosition = new Vector3 (basePosition.x + (formation.xSpread * (1 + incrementsEven [slave.rank])), basePosition.y, basePosition.z - (formation.zSpread * slaves [i].rank));
									break;
								}
							case(Axis.Z):
								{
									slaves [i].formationPosition = new Vector3 (basePosition.x - (formation.zSpread * slaves [i].rank), basePosition.y, basePosition.z + (formation.xSpread * (1 + incrementsEven [slave.rank])));

									break;
								}
							}
							incrementsEven [slave.rank]++;
						}
					}

					int[] incrementsOdd = new int[maxRanks];
					//odds
					for (int i = 1; i < slaves.Count; i += 2) {
						FormationSlave slave = slaves [i];
						//	slave.formationSlot = i;
						if (slave != captain) {
							switch (formation.axis) {
							case(Axis.X):
								{
									slaves [i].formationPosition = new Vector3 (basePosition.x - (formation.xSpread * (1 + incrementsOdd [slave.rank])), basePosition.y, basePosition.z - (formation.zSpread * slaves [i].rank));
									break;
								}
							case(Axis.Z):
								{
									slaves [i].formationPosition = new Vector3 (basePosition.x - (formation.zSpread * slaves [i].rank), basePosition.y, basePosition.z - (formation.xSpread * (1 + incrementsOdd [slave.rank])));

									break;
								}

							}
							incrementsOdd [slave.rank]++;
						}
					}
					break;
*/
					/*
					if (formation.DynamicSize) {
						formation.SetTroops (slaves.Count);
					}


					int numberOfTroops = slaves.Count;
					int ranks = formation.ranks;
					int soldiersWithNonZeroX = numberOfTroops - ranks; //overhead of 1 soldier per rank
					bool unevenSides = false;
					if (soldiersWithNonZeroX % 2 != 0) {
						unevenSides = true;
					}

					int soldiersLeft;
					int soldiersRight;

					if (unevenSides) {
						soldiersLeft = Mathf.RoundToInt (soldiersWithNonZeroX/2);
						soldiersRight = soldiersWithNonZeroX - soldiersLeft;
					} else {
						soldiersLeft = soldiersWithNonZeroX/2;
						soldiersRight = soldiersWithNonZeroX - soldiersLeft;
					}

					bool unevenLeftFlank = false;
					bool unevenRightFlank = false;

					if (soldiersLeft % ranks != 0) {
						unevenLeftFlank = true;
					}
					if (soldiersRight % ranks != 0) {
						unevenRightFlank = true;
					}

					int soldiersPerLeftRank;  //full rows
					int soldiersLastLeftRank; //because we might not have enough to fil this row
					if (unevenLeftFlank) {
						soldiersPerLeftRank = Mathf.RoundToInt (soldiersLeft / ranks);
						soldiersLastLeftRank = soldiersLeft-(soldiersPerLeftRank*ranks-1);
					}
					else
					{
						soldiersPerLeftRank = soldiersLeft / ranks;
						soldiersLastLeftRank = soldiersPerLeftRank;
					}




					int soldiersPerRightRank;  //full rows
					int soldiersLastRightRank; //because we might not have enough to fil this row


					if (unevenRightFlank) {
						soldiersPerRightRank = Mathf.RoundToInt (soldiersRight / ranks);
						soldiersLastRightRank = soldiersRight-(soldiersPerRightRank*ranks-1);
					}
					else
					{
						soldiersPerRightRank = soldiersRight / ranks;
						soldiersLastRightRank = soldiersPerRightRank;
					}
			


					int soldierCounter = 0;

					//just the first rank
					for (int x = 1; x <= soldiersLastLeftRank; x++) {
						slaves [soldierCounter].FormationSlot = new Vector2 (x * -1, 1);
						listOfFormationSlots.Add (new Vector2 (x * -1, 1));
						soldierCounter++;
					}

					for (int x = 1; x <= soldiersLastRightRank; x++) {
						slaves [soldierCounter].FormationSlot = new Vector2 (x, 1);
						listOfFormationSlots.Add (new Vector2 (x, 1));
						soldierCounter++;
					}

					//all other ranks
					for (int y = 2; y <= formation.ranks; y++) {
						int side = -1;
						for (int x = 1; x <= soldiersPerLeftRank; x++) {
							slaves[soldierCounter].FormationSlot = new Vector2(x*-1,y);
							listOfFormationSlots.Add(new Vector2(x*-1,y));
							soldierCounter++;
						}
	
						for (int x = 1; x <= soldiersPerRightRank; x++) {
							slaves[soldierCounter].FormationSlot = new Vector2(x,y);
							listOfFormationSlots.Add(new Vector2(x,y));
							soldierCounter++;
						}
					}


					//Middle troops
					{
						for (int g = 1; g <= ranks; g++) {
							slaves[soldierCounter].FormationSlot = new Vector2(0,g);
							listOfFormationSlots.Add(new Vector2(0,g));
							soldierCounter++;
						}

						Debug.Log (soldierCounter + " were assigned out of " + numberOfTroops);

					}
*/

					//////////////////////////////////////////////////////////////////////////////////////////
					/*

					int flankTroops = formation.troops - formation.ranks; //1 troop per rank will be in line with the captain, so not on a flank and their number will be = 0;
					float troopsPerRank = formation.troops / formation.ranks;
					float remainder = formation.troops % formation.ranks;
					troopsPerRank -= remainder;
					int troopsPerSide = ((int)troopsPerRank - 1) / 2;
					int soldierCounter = 0;
					for (int y = 1; y <= formation.ranks; y++) {
						//for each rank
						int side = -1;
						for (int x = 1; x <= troopsPerSide; x++) {
							//foreach soldier in that rank on left side
							slaves[soldierCounter].FormationSlot = new Vector2(x*side,y);
							soldierCounter++;
						}
						//the middle soldier
						slaves[soldierCounter].FormationSlot = new Vector2(0,y);
						soldierCounter++;
						side = 1;
						for (int x = 1; x <= troopsPerSide; x++) {
							//foreach soldier in that rank on right side
							slaves[soldierCounter].FormationSlot = new Vector2(x*side,y);
							soldierCounter++;
						}
					}
					AssignMovementOrders ();
					Debug.Log ("Troops Per Side: " + troopsPerSide + ". Per Row: " + troopsPerRank );
					*/
					break;
				}


			case(FormationTypes.SQUARE):
				{


					break;
				}
			}
		}
	}

	#endregion

	void AssignMovementOrders(){
		int dir = -1;
		if (!formation.HeadingPositive)
			dir = 1;
		if (formation.CaptainLeadsFromTheFront)
			dir *= -1;
		switch (formation.axis) {
		case(Axis.X):{
				Debug.Log ("Assigning X axis movement orders");
				foreach (FormationSlave slave in slaves) {
					Vector3 vec = captain.transform.position;
					vec.x += (formation.xSpread * slave.FormationSlot.x );
					vec.z += (formation.zSpread * slave.FormationSlot.y * dir);
					slave.AgentFormationPosition = vec;
//					slave.mc.arrived = false;
				}
				captain.AgentFormationPosition = captain.transform.position;
			//	DisableColliders();
			//	Invoke ("EnableColliders", 3);
				break;
			}
		case(Axis.Z):{
				Debug.Log ("Assigning Z axis movement orders");
				foreach (FormationSlave slave in slaves) {
					Vector3 vec = captain.transform.position;
					vec.z += (formation.xSpread * slave.FormationSlot.x );
					vec.x += (formation.zSpread * slave.FormationSlot.y * dir);
					slave.AgentFormationPosition = vec;
				}
				captain.AgentFormationPosition = captain.transform.position;

				break;
			}
		default:
			{
				Debug.Log ("Invalid axis");
				break;
			}
		}
	}

	void MassMove(Vector3 moveOrder){
		if (!UseNavMeshAgent) {
			foreach (FormationSlave slave in slaves) {
				slave.mc.npcInputToMove (moveOrder);
			}
		} else {
			captain.mc.AgentAdjustSpeed (MovementController.WALK);
			captain.mc.AgentInputDirectionalMovement (moveOrder);
		}
	}




	void EnableColliders(){
		Debug.Log ("Colliders Enabled");
		foreach (FormationSlave slave in slaves) {
			slave.gameObject.layer = 0;
		}
		captain.gameObject.layer = 0;
	}

	public void EnableColliders(FormationSlave slave){
			slave.gameObject.layer = 0;
	}

	void DisableColliders(){
		Debug.Log ("Colliders Enabled");
		foreach (FormationSlave slave in slaves) {
			slave.gameObject.layer = 31;
		}
		captain.gameObject.layer = 31;
	}

	public void DisableColliders(FormationSlave slave){
			slave.gameObject.layer = 31;
		}

	public void Register(FormationSlave slave){
		if (slave != captain) {
			slaves.Add (slave);
		} else {
			captain.FormationSlot = new Vector2 (0, 0);
		}
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
