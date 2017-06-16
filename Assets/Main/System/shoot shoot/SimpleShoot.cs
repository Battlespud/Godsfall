using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShoot : MonoBehaviour {

	public GameObject bullet;

	MovementController mc;

	GameObject wayfinder;


	// Use this for initialization
	void Start () {
		mc = gameObject.GetComponent<MovementController> ();
		wayfinder = Camera.main.gameObject.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			shoot ();
		}
	}

	void shoot(){
		GameObject locBullet = Instantiate (bullet, transform.position + wayfinder.transform.forward*.2f, wayfinder.transform.rotation);
		locBullet.GetComponent<Rigidbody> ().AddForce (locBullet.transform.forward * 15f);
		locBullet.GetComponent<BulletScript> ().shooter = gameObject;
	}

}
