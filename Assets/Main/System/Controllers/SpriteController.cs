using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// so what we want to do here is make it so that our sprite acts like a 3d model.
//this means dynamically swapping sprites not only based on movement (animations) but also based on camera angle
//this is called 2.5d, like ragnarok style.one big benefit is that they wont look out of place around 3d objects.


public enum Facing{
	U,
	UR,
	R,
	DR,
	D,
	DL,
	L,
	UL
};

//must be mono to be searchable with get component
public class SpriteController : MonoBehaviour{

	public Camera cam;
	public CameraDirection cameraDirection;

	public GameObject parentGo;

	Transform spriteTransform;
	Transform _t;

	const int NumberOfSprites = 8;
	//technically only 5 are needed if we make use of sprite.flipx, but this way allows some characters to have
	//unique sides and saves a few lines of code

	public Sprite U;
	public Sprite UR;

	public Sprite R;
	public Sprite DR;

	public Sprite D;
	public Sprite DL;

	public Sprite L;
	public Sprite UL;

	Facing facing; 
	public Facing finalFacing;

	Sprite[] bodySpritesArray;
	SpriteRenderer sr;



	// Use this for initialization
	void Start () {
		facing = Facing.D;
		bodySpritesArray = new Sprite[NumberOfSprites]{ U,UR,R,DR,D,DL,L,UL };
		sr = gameObject.GetComponent<SpriteRenderer> ();
		sr.sprite = bodySpritesArray[(int) facing];
		_t = transform;
		spriteTransform = sr.transform;
		cam = Camera.main;
		cameraDirection = cam.GetComponent<CameraDirection> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		int offset = facing - cameraDirection.facing;
		if (offset < 0)
			offset += 8; //wrap around
		finalFacing = (Facing)offset;
		if (finalFacing == Facing.R || finalFacing == Facing.DR || finalFacing == Facing.UR) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}


		sr.sprite = bodySpritesArray [(int)finalFacing];

		Vector3 targetPoint = new Vector3(cam.transform.position.x, _t.position.y, cam.transform.position.z) - transform.position;
	//	Debug.Log (targetPoint);
		transform.rotation=Quaternion.LookRotation(cam.transform.forward);
		//transform.rotation=Quaternion.LookRotation(-targetPoint, Vector3.up);

	}

	public void UpdateSprite(Vector3 vec){
		int offset = facing - cameraDirection.facing;
		if (offset < 0)
			offset += 8; //wrap around
		finalFacing = (Facing)offset;
		if (finalFacing == Facing.R || finalFacing == Facing.DR || finalFacing == Facing.UR) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}
		if(vec.x != 0)
		{
			if (vec.x > 0)
				facing = Facing.R;
			else
				facing = Facing.L;
		}
		if(vec.z != 0)
		{
			if (vec.z > 0)
				facing = Facing.U;
			else
				facing = Facing.D;
		}


		sr.sprite = bodySpritesArray [(int)finalFacing];

	}

	/*
	public void UpdateSprite(Vector3 vec){
		if(vec.x != 0)
		{
			if (vec.x > 0)
				facing = Facing.R;
			else
				facing = Facing.L;
		}
		if(vec.z != 0)
		{
			if (vec.z > 0)
				facing = Facing.U;
			else
				facing = Facing.D;
		}
		int offset = (int)facing - (int)cameraDirection.facing+2;
			if (offset < 0)
				offset += 8; //wrap around
		if (facing == Facing.R || facing == Facing.DR || facing == Facing.UR) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}

		sr.sprite = bodySpritesArray [offset];

	}
*/

}
