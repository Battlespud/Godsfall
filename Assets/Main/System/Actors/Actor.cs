using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Entity {


	//test constructor, setup overloads later
	void initialize(){
		name = "fizz"; //because i enjoy torturing fizz
		mBlood = 25;
		blood = mBlood;
		status = Status.Normal;
		deathEvent = new DeathEventArgs();
		combatEvent = new CombatEvent ();
		Debug.Log ("successfully made an actor");
		body = new Body ();
		body.parentEntity = this;
		characterSheet = new CharacterSheet();
	}

	private void registerListeners(){
		deathEvent.AddListener (Announcer.AnnounceDeath);
		combatEvent.AddListener (CombatHandler.ResolveCombat);

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



	// Use this for initialization
	new void Start () {
		initialize ();
	}
	
	// Update is called once per frame
	new void Update () {
		if (blood <= 0 && (status != Status.Dead)) {
			die ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Breaking...");
			//TESTING
			breakEveryBone();
		}


	}
}
