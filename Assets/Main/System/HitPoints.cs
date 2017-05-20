using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitPoints : IEventInitializer{

	public UnityEvent hpZeroEvent;

	public string refName;

	public object parent;
	public Bone parentBone;
	public BodyPart parentBodyPart;

	public bool locked = false;
	public bool initialized = false;

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

	//NEVER call this directly or you will break everything after initialization
	private int hp;

	public int Hp {
		get {
			return hp;
		}
		set {
			if (!locked) {
				hp = value;
				if (hp < 0) {
					hp = 0;
				}
				//TODO ADD EVENT
			}
		}
	}

	public void modifyHP(int i)
	{
		hp += i;
		validateHP ();
	}

	public int mHp;

	public bool outOfHP(){
		return (hp == 0);
	}

	private void validateHP(){
		if (hp > mHp)
			hp = mHp;
		if (hp < 0)
			hp = 0;
		if (outOfHP ()) {
			locked = true;
			hpZeroEvent.Invoke ();
		}
	}

	public HitPoints(int mh, TypeOfHP typ){
		typeOfHP = typ;
		mHp = mh;
		hp = mHp;
		initialized = true;
	}

	//implement IEventInitializer
	public void initializeEvents()
	{
		hpZeroEvent = new UnityEvent();

		if(parent.GetType() == typeof(BodyPart))
		{
			hpZeroEvent.AddListener (parentBodyPart.destroyed);
		}
		if(parent.GetType() == typeof(Bone))
		{
			hpZeroEvent.AddListener (parentBone.destroyed);
		}
	}

}
