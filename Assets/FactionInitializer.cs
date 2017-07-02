using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionInitializer : MonoBehaviour {


	// 0 = default,
	// 1 = guards,
	// 2 = bandits
	// 3 = pirates



	// Use this for initialization
	void Awake () {
		FactionMatrix.SetRelationship (0, 1, Diplo.FRIEND);
		FactionMatrix.SetRelationship (0, 2, Diplo.PEACE);
		FactionMatrix.SetRelationship (0, 3, Diplo.WAR);
		FactionMatrix.SetRelationship (0, 0, Diplo.ALLY);

		FactionMatrix.SetRelationship (1, 2, Diplo.WAR);
		FactionMatrix.SetRelationship (1, 3, Diplo.WAR);

		FactionMatrix.SetRelationship (2, 3, Diplo.ALLY);
		FactionMatrix.hasChanged = true;
		Destroy (this.gameObject);
	}
	

}
