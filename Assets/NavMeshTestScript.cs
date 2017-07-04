using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTestScript : MonoBehaviour {

	public bool MoveToNewObject = false;
	public GameObject goalObject;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (MoveToNewObject)
			agent.destination = goalObject.transform.position;
	}
}
