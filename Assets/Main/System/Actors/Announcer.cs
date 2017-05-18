using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//all subscriptions go here
		Entity.onDied += AnnounceDeath;
	}

	void AnnounceDeath(Entity e){
		string deathAnnouncement = string.Format ("{0} has been slain!", e.name);
		Debug.Log (deathAnnouncement);
	}

	void AnnounceBoneBreak(Entity e, Bone b){
		string deathAnnouncement = string.Format ("{0} has had their {1} shattered!", e.name, b.name);
		Debug.Log (deathAnnouncement);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
