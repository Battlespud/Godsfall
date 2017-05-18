using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : IHealth {

	public  delegate void boneBroke(BrokenBoneEventArgs e);
	public static event boneBroke onBoneBroke;

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
		onBoneBroke.Invoke (new BrokenBoneEventArgs (this.parentBodyPart.parentBody.parentEntity, this));
	}

	public Bone(int h, string nam){
		hitPoints = new HitPoints (h, HitPoints.TypeOfHP.bodyPart);
		name = nam;
	}

	public BodyPart parentBodyPart;

}
