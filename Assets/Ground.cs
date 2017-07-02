using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    public MovementController mc;
	// Use this for initialization
	void Start () {
		mc = gameObject.GetComponentInParent<MovementController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        mc.isGravityGrounded = true;
        Debug.Log("is grounded");
    }

    public void OnTriggerExit(Collider other)
    {
        mc.isGravityGrounded = false;
        Debug.Log("is not groundedeededed");
    }
}
