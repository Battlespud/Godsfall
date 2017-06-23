using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeDirectionController : MonoBehaviour {

	//first we have to find which way would be up and save that angle.
	//or maybe i did that already with the direction resolver ray thing?

	public GameObject ObjectToSpawn;
	public GameObject SpawnedObject;

	public SpriteController sc;
	GameObject go;
	Vector3 last;

	// Use this for initialization
	void Start () {
		go = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = DirectionResolver.RayDirection (sc);
		if (pos != last) {
			if (SpawnedObject != null) {
				DestroyImmediate(SpawnedObject);
			}
			ObjectToSpawn.transform.position = pos;
			SpawnedObject = Instantiate (ObjectToSpawn, go.transform);
			SpawnedObject.transform.localPosition = pos*3;

		}
		last = pos;
	}
}
