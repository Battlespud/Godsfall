using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

	public const int spearDamage = 1;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<Entity> () && !col.gameObject.isStatic) {
		//	col.gameObject.GetComponent<Entity> ().body.dealRandomDamage (spearDamage);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
