using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//add an update loop and have them inhereit from monobehaviour TODO
public class Bone : IHealth, IEventInitializer {

	public BrokenBoneEventArgs brokenBoneEvent;

	//destruction = disabling of limb
		//contained within bodypart, so we dont need to repeat location information here
	public bool isDestroyed = false;

	public string name;

	public HitPoints hitPoints;

	public Bone(int h, string nam, BodyPart b){
		hitPoints = new HitPoints (h, HitPoints.TypeOfHP.bodyPart);
		name = nam;
		hitPoints.refName = name + " hp";
		parentBodyPart = b;
		initializeEvents ();
		Debug.Log (parentBodyPart.getName());
	}

	public BodyPart parentBodyPart;

	//IHealthImplementation\\

	public void takeDamage(int hpLost){
		hitPoints.Hp -= hpLost;
		onHpChanged ();
	}

	public void heal(int hpGain){
		hitPoints.Hp += hpGain;
		onHpChanged ();
	}

	void onHpChanged(){
		if (hitPoints.outOfHP ()) {
			destroyed ();
		}
	}

	public void destroyed(){
		isDestroyed = true;
		hitPoints.Hp = 0;
		hitPoints.locked = true;
		brokenBoneEvent.Invoke (this.parentBodyPart.parentBody.parentEntity, this);
	}

	//IEventInitializer Implementation
	public void initializeEvents(){
		brokenBoneEvent = new BrokenBoneEventArgs ();
		brokenBoneEvent.AddListener (Announcer.AnnounceBoneBreak);	
	}

}
