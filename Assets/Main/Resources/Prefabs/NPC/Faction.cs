using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour, IFaction {

	//static stuff
	public const int MaxFactions = 4;
	static string faction0 = "Default";
	static string faction1 = "Guards";
	static string faction2 = "Bandits";
	static string faction3 = "Pirates";
	public static int[] FactionIDs = new int[MaxFactions];
	public static string[] FactionNames = new string[MaxFactions]{faction0,faction1,faction2,faction3 };






	//instanced

	public List<IFaction> dependents = new List<IFaction>(); //anything that needs to change based on this
	public int FactionID;
	public string FactionName;

	// Use this for initialization
	void Start () {
		ChangeFaction (FactionID);
	}

	public void ChangeFaction(int i){
		FactionID = i;
		FactionName = FactionNames [FactionID];
		if (dependents != null) {
			foreach (IFaction dependent in dependents) {
				dependent.ChangeFaction (i);
			}
		}
	}


	void Update () {
		
	}
}
