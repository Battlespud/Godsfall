using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Entity , IEventInitializer {


	void initialize(){
		if (isPlayer) {
			name = "Player";
		} else {
			name = "Town Guard";
		}
		mBlood = 25;
		blood = mBlood;
		status = Status.Normal;
		body = new Body (this);
		characterSheet = new CharacterSheet();
		eGameObject = this.gameObject;
		eGameObject.GetComponent<MovementController> ().isPlayer = isPlayer;
	}

	public override void modifyBlood(int i){
		blood += i;
		//TODO add event
	}

	public void die(){
		deathEvent.Invoke (this);
		status = Status.Dead;
	}

	public void breakEveryBone(){
		foreach (BodyPart b in body.bodyPartsList) {
			b.bone.destroyed ();
		}
	}

	public void maimEveryLimb(){
			foreach (BodyPart b in body.bodyPartsList) {
			b.destroyed ();
			}
	//	maimPermanent ();
		}

	//probably dont want to mess with this, but it works without any errors so yay
	void maimPermanent(){
		int c = body.bodyPartsList.Count;
		BodyPart[] bArray = new BodyPart[c];
		bArray = body.bodyPartsList.ToArray ();
		for (int i = 0; i < c; i++) {
			Debug.Log("Amputating: " + bArray[i].name);
			body.bodyPartsList.Remove (bArray [i]);
		}
	}

	// Use this for initialization
	new void Start () {
		initialize ();
		initializeEvents ();
	}

	// Update is called once per frame
	new void Update () {
		if (blood <= 0 && (status != Status.Dead)) {
			die ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log (string.Format("Breaking {0}...", this.name));
			//TESTING
			breakEveryBone();
		}

		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			Debug.Log (string.Format("Maiming {0}...", this.name));
			//TESTING
			maimEveryLimb();
		}

	}

	//
	public void initializeEvents(){
		deathEvent = new DeathEventArgs();
		deathEvent.AddListener (Announcer.AnnounceDeath);

		combatEvent = new CombatEvent ();
		combatEvent.AddListener (CombatHandler.ResolveCombat);

	}

}
