using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLightController : MonoBehaviour {


	public GameObject Sun; //the main light source in the scene
	public Light characterLight;
	Camera cam;
	CameraDirection dir;

	public int debugindex;

	const int maxDif = 4; //8 options on base 0 circular

	public float maxIntensity = 3f;
	public float intensityPercent = 0f;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		dir = cam.GetComponent<CameraDirection> ();
		characterLight = gameObject.GetComponent<Light> ();
		Sun = GameObject.FindGameObjectWithTag ("Sun");
		RegisterLightDirection ();
		}

	public Facing facing;

	void RegisterLightDirection(){
		float rX = Sun.transform.rotation.x;
		float x = Mathf.Abs(rX);

		if(x < 22.5f)
		{
			facing = Facing.U;
		}
		else if(x < 67.5f)
		{
			facing = rX < 0 ? Facing.UL : Facing.UR;
		}
		else if(x < 112.5f)
		{
			facing = rX < 0 ? Facing.L : Facing.R;
		}
		else if(x < 157.5f)
		{
			facing = rX < 0 ? Facing.DL : Facing.DR;
		}
		else
		{
			facing = Facing.D;
		}
		facing = DirectionResolver.FlipFacing (facing);
	}

	void Update () {


		if (dir.facing == facing) {
			debugindex = 0;
			intensityPercent = 0f;

		} else if (dir.facing == facing + 1 || dir.facing == facing - 1) {
			debugindex = 1;
			intensityPercent = .15f;

		}
		else if(dir.facing == facing + 2 || dir.facing == facing - 2) {
			debugindex = 2;
			intensityPercent = .75f;

		}
		else if(dir.facing == facing + 3 || dir.facing == facing - 3) {
			debugindex = 3;
			intensityPercent = .85f;
		}
		else if(dir.facing == facing + 4 || dir.facing == facing - 4 ) {
			debugindex = 4;
			intensityPercent = 1f;
		}
		//intensityPercent = debugindex / maxDif;
		characterLight.intensity = maxIntensity*intensityPercent;
	}
}
