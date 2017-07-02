using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Diplo{
	WAR=0,
	PEACE,
	FRIEND,
	ALLY=3
};

public static class FactionMatrix {
	//records diplomatic relations between all groups as a 2d array

	public static bool hasChanged = false;

	static Diplo[,] RelationshipMatrix = new Diplo[Faction.MaxFactions,Faction.MaxFactions];

	public static void SetRelationship(int a, int b, Diplo relationship)
	{
		if (a > Faction.MaxFactions - 1 || a < 0 || b < 0 || b > Faction.MaxFactions - 1) {
			Debug.Log ("Invalid faction ids were entered, no action taken");
		} else {
			RelationshipMatrix [a, b] = relationship;
			RelationshipMatrix [b, a] = relationship;
			hasChanged = true;
		}
	}

	public static Diplo GetRelationship(int a, int b)
	{
		Diplo e;
		if (a > Faction.MaxFactions - 1 || a < 0 || b < 0 || b > Faction.MaxFactions - 1) {
			Debug.Log ("Invalid faction ids were entered, result will be incorrect");
			e = Diplo.WAR;
		} else {
			Diplo d = RelationshipMatrix[a,b];
			Diplo r = RelationshipMatrix[b,a];
			e = d;
			if (d != r) {
				Debug.Log ("The relationships are different, something went wrong, result will be incorrect");
			}

		}
		return e;
	}



}
