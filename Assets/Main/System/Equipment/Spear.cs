﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour, IWeapon {


	//TODO add IWeapon interface

	 const int spearDamage = 5;
	 const int spearKnockback = 3;
	Vector3 basePosition;

	// Use this for initialization
	void Start () {
		basePosition = transform.localPosition;
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<Entity> () && !col.gameObject.isStatic) {
			Entity e = col.gameObject.GetComponent<Entity> ();
			e.body.dealRandomDamage (spearDamage);
			e.movementController.AddImpact (transform.up, spearKnockback);
			Attack ();
		}
	}


	public void Attack(){
		transform.localPosition = transform.localPosition + (transform.up * .55f);
		Invoke ("PullThrust", 2f); //ToDo find a better way to do this, Invoke is expensive
	}

	void PullThrust(){
		transform.localPosition = basePosition;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
