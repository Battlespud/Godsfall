using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//add an update loop and have them inhereit from monobehaviour TODO
public class Bone : IHealth, IEventInitializer {

	public BrokenBoneEventArgs brokenBoneEvent;
	public UnityEvent alertBodyEvent;

	//destruction = disabling of limb
		//contained within bodypart, so we dont need to repeat location information here
	public bool isDestroyed = false;

	public string name;

	public HitPoints hitPoints;

	public Bone(int h, string nam, BodyPart b){
		hitPoints = new HitPoints (h);
		name = nam;
		hitPoints.refName = name + " hp";
		parentBodyPart = b;
		//Debug.Log (parentBodyPart.getName());
	}

	public BodyPart parentBodyPart;

	//IHealthImplementation\\

	public void takeDamage(int hpLost){
		hitPoints.Hp -= hpLost;
		Debug.Log (parentBodyPart.parentBody.parentEntity.name + "'s " + parentBodyPart.side  + name + " takes " + hpLost + " points of damage.");
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
		if (isDestroyed) {
			Debug.Log (string.Format ("Error, {0}'s {1} {2} is already destroyed", this.parentBodyPart.parentBody.parentEntity.name, this.parentBodyPart.side.ToString(),this.name));
			return;
		}
		isDestroyed = true;
		hitPoints.Hp = 0;
		hitPoints.locked = true;
		alertBodyEvent.Invoke ();
		brokenBoneEvent.Invoke (this.parentBodyPart.parentBody.parentEntity, this);

	}

	//IEventInitializer Implementation
	public void initializeEvents(){
		brokenBoneEvent = new BrokenBoneEventArgs ();
		brokenBoneEvent.AddListener (Announcer.AnnounceBoneBreak);	

		alertBodyEvent = new UnityEvent ();
		alertBodyEvent.AddListener (parentBodyPart.parentBody.checkMovement);
	}

}
