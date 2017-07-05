using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTestScript : MonoBehaviour {
	[Tooltip("Actor will path to this object, assuming it touches a navmesh.  Just a debug script")]
	public GameObject goalObject;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (goalObject!= null)
			agent.destination = goalObject.transform.position;
	}
}
