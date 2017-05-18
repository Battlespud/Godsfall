using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

	public  delegate void died(DeathEventArgs args);
	public static event died onDied;

	public string name;

	public int blood;
	public int mBlood;
	public int bleedRate = 0; // bleed per second


	public enum Status{
		Dead=0,
		Normal=1,
		Stunned=2



	};

	public Status status;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (blood <= 0) {
			die ();
		}
	}

	void die(){
		onDied.Invoke(new DeathEventArgs(this));
	}

}
