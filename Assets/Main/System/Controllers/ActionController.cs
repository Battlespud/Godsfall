using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

	public GameObject thisGameObject;
	public SpriteController sc;
	const KeyCode attackKey = KeyCode.F;

	// Use this for initialization
	void Start () {
		thisGameObject = this.gameObject;
		sc = GetComponentInChildren<SpriteController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (attackKey)) {
			tryAttack ();
		}
	}



	public void tryAttack(){
		RaycastHit rayHit; 
		Ray ray;
		//something screwy here
			ray = new Ray (transform.position, DirectionResolver.RayDirection (sc));
			Debug.DrawRay (transform.position, DirectionResolver.RayDirection (sc) * 10f, Color.blue, 2f);
		if (Physics.Raycast (ray, out rayHit, 10f)) {
			if (rayHit.collider.gameObject.GetComponent<Actor> ()) {
				GameObject hitGo = rayHit.collider.gameObject;
				Actor hitActor = hitGo.GetComponent<Actor> ();
				Debug.Log ("Attack hits " + hitActor.name);
				doAttack (hitActor);
			} else if (rayHit.collider.gameObject.GetComponent (typeof(IInteractableC)) != null) {
				Debug.Log ("Found the interactable interface!");
				IInteractableC genericClass = (IInteractableC)rayHit.collider.gameObject.GetComponent (typeof(IInteractableC));
				genericClass.Interact ();
			} else {
				Debug.Log ("Hit Nothing!");
			}
		}
	}

	public static void doAttack(Actor actor){
		actor.body.dealRandomDamage (100);
	}

}
