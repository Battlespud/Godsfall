using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is a monobehaviour that implements IHealth
public class BodyPart :  IHealth {

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
		Skull=0,
		Humerus=1,
		Femur=2

	};

	public enum Side{
		LEFT=0,
		RIGHT=1
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
		bone = new Bone (hBone, ((BodyPartName)hBone).ToString());
	}


	//deprecated
	string strHeadBone = "Skull";
	string strArmBone = "Humerus";
	string strLegBone = "Femur";

}
