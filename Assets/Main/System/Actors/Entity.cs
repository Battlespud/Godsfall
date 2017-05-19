using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

	public GameObject eGameObject;

	public DeathEventArgs deathEvent;
	public CombatEvent combatEvent;

	public  new string name;

	public Body body;
	public CharacterSheet characterSheet;
	public Movement  movement;

	public int blood;
	public int mBlood;
	public int bleedRate = 0; // bleed per second


	public enum Status{
		Dead=0,
		Normal=1,
		Stunned=2

	};

	public Status status;

	// Use this for initialization
	public void Start () {
		deathEvent.AddListener (Announcer.AnnounceDeath);
	}
	
	// Update is called once per frame
	public void Update () {


		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			//TESTING
			Debug.Log(string.Format("Error, {0} did not successfully hide Entity.Update()", name));
		}
	}



}

public class Movement{



}
