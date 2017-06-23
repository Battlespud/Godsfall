using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {


	//use this to initialize static classes etc
	void Awake(){
		for (int i = 0; i < Faction.MaxFactions; i++) {
			Faction.FactionIDs [i] = i;
		}
	}

	// Use this for initialization
	void Start () {
		SpeechRepo.InitializeDictionary ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
