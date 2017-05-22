using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Announcer {

//For now just relays events to the debug log, in the future will be linked to sound files etc

	public static void AnnounceDeath(Actor e){
		string deathAnnouncement = string.Format ("{0} has fallen!", e.name);
		Debug.Log (deathAnnouncement);
	}

	public static void AnnounceBoneBreak(Entity e, Bone b){
		string deathAnnouncement = string.Format ("{0}'s {1} {2} was shattered!", e.name, b.parentBodyPart.side.ToString(), b.name);
		Debug.Log (deathAnnouncement);
	}

	public static void AnnounceLimbLoss(Entity e, BodyPart b){
		string deathAnnouncement = string.Format ("{0}'s  {1} {2} was mutilated beyond recognition!", e.name, b.side.ToString(), b.name);
		Debug.Log (deathAnnouncement);
	}

	public static void AnnounceString(string s){
		string deathAnnouncement = s;
		Debug.Log (deathAnnouncement);
	}

}
