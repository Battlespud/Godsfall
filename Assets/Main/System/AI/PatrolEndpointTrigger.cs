using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEndpointTrigger : MonoBehaviour {



	void OnTriggerEnter(Collider other){
		if (other.GetComponent<PatrolBetweenEndpointsBehavior> () != null) {
			other.GetComponent<PatrolBetweenEndpointsBehavior> ().TurnAround ();
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
