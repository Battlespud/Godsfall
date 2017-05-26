using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BodyPart :  IHealth , IEventInitializer {

	public LostLimbEvent lostLimbEvent;
	public UnityEvent alertBodyEvent;

	//just rando numbers
	const int skullHP = 10;
	const int armHP = 15;
	const int legHP = 15;

	const float boneHealthModifier = .45f;

	public string name;

	public string getName(){
		return name;
	}

	public bool initialized = false;

	public enum BodyPartType{
		Head=0,
		Arm=1,
		Leg=2
	};
	public BodyPartType bodyPartType;

	enum BodyPartName{
		Skull=0, //each side is half a skull
		Humerus=1,
		Femur=2
	};

	public enum Side{
		Left=0,
		Right=1
	};
	public Side side;

	public bool isDestroyed = false;

	public HitPoints hitPoints;
	public Bone bone;
	//pointer to parent
	public Body parentBody;





	public BodyPart(BodyPartType typ, Side sid, int h, int hBone){
		bodyPartType = typ;
		side = sid;
		name = typ.ToString();
		hitPoints = new HitPoints (h, HitPoints.TypeOfHP.bodyPart);
		hitPoints.refName = (bodyPartType.ToString() + " hp"); //name of this
		bone = new Bone (hBone, ((BodyPartName)hBone).ToString(), this); //name of bone
		bone.parentBodyPart = this;
		bone.initializeEvents();
	}


	//deprecated
	string strHeadBone = "Skull";
	string strArmBone = "Humerus";
	string strLegBone = "Femur";

	string strHead = "Head";
	string strArm = "Arm";
	string strLeg = "Leg";

	string[] nameArrayBP = new string[3] {"Head" ,"Arm","Leg"};

	public BodyPart(){
		//constructs a type reference for type checking. Otherwise please dont use
	}

	public BodyPart(int typ, int sid){
		name = nameArrayBP[typ];

		switch (typ) {
		case 0:
			bodyPartType = (BodyPartType)typ; //skul
			side = (Side)sid; //left
			hitPoints = new HitPoints (skullHP / 2, HitPoints.TypeOfHP.bodyPart);
			hitPoints.refName = (strHeadBone + " hp");
			bone = new Bone (skullHP, (strHeadBone), this);
			break;

		case 1:

			bodyPartType = (BodyPartType)typ; //arm
			side = (Side)sid; //left
			hitPoints = new HitPoints (armHP, HitPoints.TypeOfHP.bodyPart);
			hitPoints.refName = ("Arm" + " hp");
			bone = new Bone (legHP, (strArmBone), this);
			break;

		case 2: 

			bodyPartType = (BodyPartType)typ; //leg
			side = (Side)sid; //left
			hitPoints = new HitPoints (legHP, HitPoints.TypeOfHP.bodyPart);
			hitPoints.refName = ("Leg " + " hp");
			bone = new Bone ((int)(legHP * boneHealthModifier), (strLegBone), this);
			break;
		}
		bone.parentBodyPart = this;
		hitPoints.parent = this;
	}

	//IHealth Implementation

	public void takeDamage(int hpLost){
		hitPoints.Hp -= hpLost;
		Debug.Log (name + " takes " + hpLost + " points of damage.");
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
			Debug.Log (string.Format ("Error, {0}'s {1} {2} is already destroyed", this.parentBody.parentEntity.name, this.side.ToString(),this.name));
			return;
		}
		isDestroyed = true;
		hitPoints.Hp = 0;
		hitPoints.locked = true;
		bone.destroyed ();
		lostLimbEvent.Invoke (this.parentBody.parentEntity, this);
		alertBodyEvent.Invoke();
	}

	public void destroyedPermanent(){
		destroyed ();
		this.parentBody.bodyPartsList.Remove (this);
		alertBodyEvent.Invoke ();
	}

	//IEventInitializer Implementation
	 public void initializeEvents(){
		lostLimbEvent = new LostLimbEvent();
		lostLimbEvent.AddListener (Announcer.AnnounceLimbLoss);

		alertBodyEvent = new UnityEvent ();
		alertBodyEvent.AddListener (parentBody.checkMovement);
		initialized = true;
		bone.initializeEvents ();
	}


}
