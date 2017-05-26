﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : IEventInitializer {

	public BoolEvent canMoveEvent;

	//pointer to parent
	public Entity parentEntity;
	public List<BodyPart> bodyPartsList = new List<BodyPart>();



	MovementController movementController;
	public List<StatusEffect> statusEffectsList = new List<StatusEffect>();

	void setupHumanBody(){
		bodyPartsList.Add(new BodyPart(0,0));
		bodyPartsList.Add(new BodyPart(0,1));
		bodyPartsList.Add(new BodyPart(1,0));
		bodyPartsList.Add(new BodyPart(1,1));
		bodyPartsList.Add(new BodyPart(2,0));
		bodyPartsList.Add(new BodyPart(2,1));
	}

	public Body(Entity e){
		parentEntity = e;
		movementController = e.eGameObject.GetComponent<MovementController> ();
		setupHumanBody ();
		foreach(BodyPart b in bodyPartsList){
			b.parentBody = this;
			b.initializeEvents ();
		}
		initializeEvents ();
		Debug.Log (string.Format("Body setup completed for {0}", parentEntity.name));
	}

	public void dealRandomDamage(int d){
		BodyPart[] bArray = bodyPartsList.ToArray ();
		bArray [Random.Range (0, bodyPartsList.Count)].takeDamage (d);
		/*
		switch(Random.Range(0,2) > 0){
		case true:
			bArray [Random.Range (0, bodyPartsList.Count)].takeDamage (d);
			break;
		case false:
			bArray [Random.Range (0, bodyPartsList.Count)].bone.takeDamage (d);

			break;
		}
		*/
	}

	bool hasLostLeg(){
		int i = 0;
		foreach (BodyPart b in bodyPartsList) {
			if (b.bodyPartType == BodyPart.BodyPartType.Leg && b.isDestroyed) {
				i++;
			}
		}
		if (i > 0) {
			return true;
		}
		return false;
	}

	bool hasLostArm(){
		int i = 0;
		foreach (BodyPart b in bodyPartsList) {
			if (b.bodyPartType == BodyPart.BodyPartType.Arm && b.isDestroyed) {
				i++;
			}
		}
		if (i > 0) {
			return true;
		}
		return false;
	}

	bool hasBrokenLeg(){
		int i = 0;
		foreach (BodyPart b in bodyPartsList) {
			if (b.bodyPartType == BodyPart.BodyPartType.Leg && b.bone.isDestroyed) {
				i++;
			}
		}
		if (i > 0) {
			return true;
		}
		return false;
	}

	bool hasBrokenArm(){
		int i = 0;
		foreach (BodyPart b in bodyPartsList) {
			if (b.bodyPartType == BodyPart.BodyPartType.Arm && b.bone.isDestroyed) {
				i++;
			}
		}
		if (i > 0) {
			return true;
		}
		return false;
	}

	bool hasAtLeastTwoLegs(){
		int i = 0;
		foreach (BodyPart b in bodyPartsList) {
			if (b.bodyPartType == BodyPart.BodyPartType.Leg) {
				i++;
			}
		}
		if (i >= 2) {
			return true;
		}
		return false;
	}

	public void checkMovement(){ 
		if ((hasLostLeg () || hasBrokenLeg() || !hasAtLeastTwoLegs()) && parentEntity.isPlayer) {
			canMoveEvent.Invoke (false);
		}
	}

	public void removeStatusEffect(StatusEffect status){
		UnityEngine.Object.DestroyImmediate (status);
	}


	//implement IInitializeEvents
	public void initializeEvents(){
		if (parentEntity.isPlayer) {
			canMoveEvent = new BoolEvent ();
			canMoveEvent.AddListener (movementController.setBodyCanMove);
		}
	}
}
