using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour{
	[Tooltip("Are troops arrayed behind of or in front of the captain?")]
	public bool CaptainLeadsFromTheFront = false; 
	[Tooltip("How many soldiers in this formation, DynamicSize is recommended instead.")]
	public int troops; //number of troops
	[Tooltip("How many distinct rows this formation. Minimum of 1")]
	public int ranks; 
	[Tooltip("Horizontal distance between troops. 1.5f is recommended.")]
	public float xSpread; //how far soldiers are from each other
	[Tooltip("Distance between ranks. 3f is recommended.")]
	public float zSpread; //how far soldiers are from each other
	[Tooltip("Only line is currently supported")]
	public FormationTypes formationType;
	[Tooltip("Axis of the formation")]
	public Axis axis; //axis along which to form formation if applicable
	[Tooltip("X and positive would look forward, X and negative would look back.  Z and Positive would look Right, Z and negative would look Left.")]
	public bool HeadingPositive = true;

	[Tooltip("'troops' will be ignored and formation will be sized based on how many formationslaves registered. Recommended.")]
	public bool DynamicSize = true; 

	//Constructor.  Deprecated and not supported
	public Formation(FormationTypes typ, int i, float spr, Axis ax)
	{
		troops = i;
		formationType = typ;
		xSpread = spr; //a gap of 1.5f with a closeEnoughFloat of .3f provides a solid spear wall that cant be walked through, and seems pretty stable.
		axis = ax;
	}
	public Formation(FormationTypes typ, int i, float spr)
	{
		troops = i;
		formationType = typ;
		xSpread = spr; //a gap of 1.5f with a closeEnoughFloat of .3f provides a solid spear wall that cant be walked through, and seems pretty stable.
	}

	public void SetTroops(int i){
		troops = i;
	}
}
