using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Announcer {

//For now just relays events to the debug log, in the future will be linked to sound files etc

	public static void AnnounceDeath(Actor e){
		string deathAnnouncement = string.Format ("{0} has been slain!", e.name);
		Debug.Log (deathAnnouncement);
	}

	public static void AnnounceBoneBreak(Entity e, Bone b){
		string deathAnnouncement = string.Format ("{0} has had their {1} {2} shattered!", e.name, b.parentBodyPart.side.ToString(), b.name);
		Debug.Log (deathAnnouncement);
	}


}
