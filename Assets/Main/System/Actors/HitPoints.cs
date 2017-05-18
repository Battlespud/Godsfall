using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour {

	public string refName;

	public delegate void hpChanged();
	public static event hpChanged onHpChanged;

	public bool locked = false;

	public enum TypeOfHP{
		bodyPart,
		Bone,
		Other
	};

	public TypeOfHP typeOfHP;

	void OnEnable(){
		//register event
	}

	void OnDisable(){
		//deregister event to prevent memory leak
	}

	private int hp;

	public int Hp {
		get {
			return hp;
		}
		set {
			if (!locked) {
				hp = value;
				//TODO ADD EVENT
			}
		}
	}

	public int mHp;



	public HitPoints(int mh, TypeOfHP typ){
		typeOfHP = typ;
		mHp = mh;
		hp = mHp;
	}


}
