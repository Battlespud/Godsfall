using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {

	//static stuff
	public const int MaxFactions = 3;
	static string faction0 = "Default";
	static string faction1 = "Guards";
	static string faction2 = "Bandits";
	public static int[] FactionIDs = new int[MaxFactions];
	public static string[] FactionNames = new string[MaxFactions]{faction0,faction1,faction2 };

	//instanced
	public int FactionID;
	public string FactionName;

	// Use this for initialization
	void Start () {
		FactionName = FactionNames [FactionID];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
