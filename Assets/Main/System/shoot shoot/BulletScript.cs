using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {


	int counter;
	public GameObject shooter;

	const int mCounter = 60;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		counter++;
		if (counter > mCounter) {
			Destroy ();
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.GetComponent <Actor>() !=null && col.gameObject != shooter) {
			col.gameObject.GetComponent<Actor> ().body.dealRandomDamage (2);
			Destroy ();
		}
		if (col.gameObject.CompareTag ("Enviro")) {
			Destroy ();
		}

	}


	void Destroy(){
		Destroy (this.gameObject);
	}
}
