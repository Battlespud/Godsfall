using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

	GameObject thisGameObject;
	const KeyCode attackKey = KeyCode.F;

	// Use this for initialization
	void Start () {
		thisGameObject = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (attackKey)) {
			tryAttack ();
		}
	}



	public void tryAttack(){
		RaycastHit rayHit; 
		Ray ray = new Ray (transform.position, transform.TransformDirection(Vector3.forward));
		Debug.DrawRay (transform.position, transform.TransformDirection (Vector3.forward*10f), Color.blue, 2f);
		if (Physics.Raycast (ray, out rayHit, 10f)) {
			if (rayHit.collider.gameObject.GetComponent<Actor> ()) {
				GameObject hitGo = rayHit.collider.gameObject;
				Actor hitActor = hitGo.GetComponent<Actor> ();
				Debug.Log ("Attack hits " + hitActor.name);
				doAttack (hitActor);
			}
		}
	}

	public static void doAttack(Actor actor){
		actor.body.dealRandomDamage (100);
	}

}
