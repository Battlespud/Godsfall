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

		transform.rotation=Quaternion.LookRotation(cam.transform.forward); //Rotates the object so that the image pane is always at the same angle to the camera, doesnt affect which sprites are displayed.  Works perfectly, dont touch it, this is not whats breaking whatever you just broke.

	}

	public void UpdateSprite(Vector3 vec, bool useVectorProcessor){
		//the vectorprocessor will break npc sprites because they don't use relativized vectors.
		if (useVectorProcessor) vec = DirectionResolver.VectorProcessor (vec);
		int offset = facing -  cameraDirection.facing;
		if (offset < 0)
			offset += 8; //wrap around
		finalFacing = (Facing)offset;
	
		if (finalFacing == Facing.R || finalFacing == Facing.DR || finalFacing == Facing.UR) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}
		//vec = Vector3.Cross (vec, cam.transform.GetChild (0).transform.forward);
		bool xGreaterNonZero = false;
		bool zGreaterNonZero = false;
		bool xLessNonZero = false;
		bool zLessNonZero = false;

		if(vec.x != 0)
		{
			if (vec.x > 0) {
				facing = Facing.R;
				xGreaterNonZero = true;
			} else {
				facing = Facing.L;
				xLessNonZero = true;
			}
		}
		if(vec.z != 0)
		{
			if (vec.z > 0) {
				facing = Facing.U;
				zGreaterNonZero = true;
			} else {
				facing = Facing.D;
				zLessNonZero = true;
			}
		}

		if (xGreaterNonZero && zGreaterNonZero) {
			facing = Facing.UR;
		}
		else if (xGreaterNonZero && zLessNonZero) {
			facing = Facing.DR;
		}
		else if (xLessNonZero && zGreaterNonZero) {
			facing = Facing.UL;
		}
		else if (xLessNonZero && zLessNonZero) {
			facing = Facing.DL;
		}

		sr.sprite = bodySpritesArray [(int)finalFacing];

	}

}
