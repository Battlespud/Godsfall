using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Entity , IEventInitializer {


	void initialize(){
		name = "fizz"; //because i enjoy torturing fizz
		mBlood = 25;
		blood = mBlood;
		status = Status.Normal;
		body = new Body (this);
		characterSheet = new CharacterSheet();
		eGameObject = this.gameObject;
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
			Debug.Log ("Breaking...");
			//TESTING
			breakEveryBone();
		}

		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			Debug.Log ("Maiming...");
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
