using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

	public GameObject thisGameObject;
	public SpriteController sc;
	const KeyCode attackKey = KeyCode.F;

	public IWeapon Weapon;

	// Use this for initialization
	void Start () {
		thisGameObject = this.gameObject;
		sc = GetComponentInChildren<SpriteController> ();
		Weapon = GetComponentInChildren<IWeapon> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (attackKey) && gameObject.GetComponent<MovementController>().isPlayer) {
			tryAttack ();
		}
	}



	public void tryAttack(){
		Weapon.Attack ();
		const float offset = .25f;
		RaycastHit rayHit; 
		Vector3 pos = transform.position;
		Vector3 left = new Vector3 (pos.x - offset, pos.y, pos.z);
		Vector3 right = new Vector3 (pos.x + offset, pos.y, pos.z);
		Ray rayL = new Ray (left, DirectionResolver.RayDirection (sc));
		Ray rayR = new Ray (right, DirectionResolver.RayDirection (sc));
			Debug.DrawRay (left, DirectionResolver.RayDirection (sc) * 10f, Color.blue, 4f);
			Debug.DrawRay (right, DirectionResolver.RayDirection (sc) * 10f, Color.blue, 4f);
				
		if (Physics.Raycast (rayL, out rayHit, 10f)) {
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
		} else if((Physics.Raycast (rayR, out rayHit, 10f))) {
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
