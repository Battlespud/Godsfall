using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour {


	[SerializeField]bool TurnAround = false;
	[SerializeField]bool PositiveHeading = true;
	[SerializeField]Axis axis = Axis.X;


	void OnTriggerEnter(Collider other){
		if (other.GetComponent<PatrolBehaviour> () != null) {
			PatrolBehaviour patrolBehaviour = other.GetComponent<PatrolBehaviour> ();
			if (TurnAround) {
				patrolBehaviour.TurnAround ();
			}
			else {
				int heading = -1;
				if (PositiveHeading) {
					heading = 1;
				}

				patrolBehaviour.DirectSet (heading, axis);


			}
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
