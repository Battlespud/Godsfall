using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {


	int counter;
	public GameObject shooter;

	const int mCounter = 180;

	const int bulletDamage = 5;

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
			col.gameObject.GetComponent<Actor> ().body.dealRandomDamage (bulletDamage);
			Destroy ();
		}


		if (col.gameObject.GetComponent<IDestructible> () != null) {
			Debug.Log ("IDestructible");
			col.gameObject.GetComponent<IDestructible> ().ITakeDamage (bulletDamage);
		}


		if (col.gameObject.CompareTag ("Enviro")) {
			Destroy ();
		}


	}


	void Destroy(){
		Destroy (this.gameObject);
	}
}
