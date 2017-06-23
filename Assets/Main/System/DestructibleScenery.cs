using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleScenery : MonoBehaviour, IDestructible {


	[SerializeField] int maximumHP = 20;

	public HitPoints hitPoints;

	//Implement IDestructible
	public void IDestroy(){
		Destroy (gameObject);
	}

	public void ITakeDamage(int i){
		hitPoints.hurt (i);
	}

	public void IInitializeHPEvents(){
		hitPoints.initializeEvents ();
	}

	// Use this for initialization
	void Start () {
		hitPoints = new HitPoints (maximumHP, this);
		IInitializeHPEvents();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
