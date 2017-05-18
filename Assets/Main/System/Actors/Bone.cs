using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//add an update loop and have them inhereit from monobehaviour TODO
public class Bone : IHealth {



	public BrokenBoneEventArgs brokenBoneEvent;


	//destruction = disabling of limb
		//contained within bodypart, so we dont need to repeat location information here
	public bool isDestroyed = false;

	public string name;

	public void takeDamage(int hpLost){
		hitPoints.Hp -= hpLost;
	}

	public HitPoints hitPoints;

	public void destroyed(){
		isDestroyed = true;
		hitPoints.Hp = 0;
		hitPoints.locked = true;
		brokenBoneEvent.Invoke (this.parentBodyPart.parentBody.parentEntity, this);
	}

	public Bone(int h, string nam){
		hitPoints = new HitPoints (h, HitPoints.TypeOfHP.bodyPart);
		name = nam;
		hitPoints.refName = name + " hp";
		brokenBoneEvent = new BrokenBoneEventArgs ();
		brokenBoneEvent.AddListener (Announcer.AnnounceBoneBreak);
	}

	public BodyPart parentBodyPart;

}
