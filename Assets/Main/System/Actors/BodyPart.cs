using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BodyPart :  IHealth {

	//just rando numbers
	const int skullHP = 10;
	const int armHP = 15;
	const int legHP = 15;


	public void takeDamage(int hpLost){
		hitPoints.Hp -= hpLost;
	}

	public enum BodyPartType{
		HEAD=0,
		ARM=1,
		LEG=2

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



	public void destroyed(){
		isDestroyed = true;
		hitPoints.locked = true;
	}

	public BodyPart(BodyPartType typ, Side sid, int h, int hBone){
		bodyPartType = typ;
		side = sid;
		hitPoints = new HitPoints (h, HitPoints.TypeOfHP.bodyPart);
		hitPoints.refName = (bodyPartType.ToString() + " hp"); //name of this
		bone = new Bone (hBone, ((BodyPartName)hBone).ToString()); //name of bone

	}


	//deprecated
	string strHeadBone = "Skull";
	string strArmBone = "Humerus";
	string strLegBone = "Femur";



	public BodyPart(int typ, int sid){
		switch (typ) {

		case 0:
			bodyPartType = (BodyPartType)typ; //skul
			side = (Side)sid; //left
				hitPoints = new HitPoints (skullHP/2, HitPoints.TypeOfHP.bodyPart);
				hitPoints.refName = (strHeadBone + " hp");
				bone = new Bone (skullHP, (strHeadBone));

			break;

		case 1:

			bodyPartType = (BodyPartType)typ; //arm
			side = (Side)sid; //left
					hitPoints = new HitPoints (armHP, HitPoints.TypeOfHP.bodyPart);
			hitPoints.refName = ("Arm" + " hp");
			bone = new Bone (legHP, (strArmBone));


			break;

		case 2: 

			bodyPartType = (BodyPartType)typ; //leg
			side =(Side) sid; //left
			hitPoints = new HitPoints (legHP, HitPoints.TypeOfHP.bodyPart);
				hitPoints.refName = ("Leg " + " hp");
			bone = new Bone (legHP, (strLegBone));
			break;


		}

		bone.parentBodyPart = this;

	}


}
