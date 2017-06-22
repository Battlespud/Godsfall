using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceModule : MonoBehaviour {

	GameObject go;
	MovementController movementController;

	Axis axis; //todo

	private Vector3 guidanceVec;
	private Vector3 correctionVec;
	public bool guidanceActive = false;
	public float guideScale = .5f;
	public float guideStep = .25f;

	[SerializeField]bool xBeingCorrected = false;
	[SerializeField]bool yBeingCorrected = false;
	[SerializeField]bool zBeingCorrected = false;

	// Use this for initialization
	void Start () {
		go = gameObject;
		movementController = gameObject.GetComponent<MovementController> ();
		UpdateGuidance ();
	}







	// Update is called once per frame
	void Update () {
		Vector3 toMove = new Vector3 (0f, 0f, 0f);
		courseCorrection ();
		movementController.npcInputToMove (toMove);
	}


	void courseCorrection(){
		xBeingCorrected = false;yBeingCorrected = false;zBeingCorrected = false;
		correctionVec = new Vector3 (0f,0f,0f);
		if (guidanceActive) {
			switch (axis) {
			case Axis.X:
				if (Mathf.Abs(transform.position.y - guidanceVec.y) > guideScale) {
					guidanceFixY ();
				}
				if (Mathf.Abs(transform.position.z - guidanceVec.z) > guideScale) {
					guidanceFixZ ();
				}
				break;

			case Axis.Y:
				if (Mathf.Abs(transform.position.x - guidanceVec.x) > guideScale) {
					guidanceFixX ();
				}
				if (Mathf.Abs(transform.position.z - guidanceVec.z) > guideScale) {
					guidanceFixZ ();
				}
				break;

			case Axis.Z:
				if (Mathf.Abs(transform.position.x - guidanceVec.x) > guideScale) {
					guidanceFixX ();
				}
				if (Mathf.Abs(transform.position.y - guidanceVec.y) > guideScale) {
					guidanceFixY ();
				}
				break;

			}
		}
	}

	void guidanceFixX(){
		//too +x
		if (transform.position.x > guidanceVec.x) {
			correctionVec.x -= guideStep;
		}
		//too -x
		if (transform.position.x < guidanceVec.x) {
			correctionVec.x += guideStep;
		}
		xBeingCorrected = true;
	}

	void guidanceFixY(){
		//too +y
		if (transform.position.y > guidanceVec.y) {
			//	correctionVec.y -= guideStep;
		}
		//too -y
		if (transform.position.y < guidanceVec.y) {
			//	correctionVec.y += guideStep;
		}
		yBeingCorrected = true;
	}

	void guidanceFixZ(){
		//too +z
		if (transform.position.z > guidanceVec.z) {
			correctionVec.z -= guideStep;
		}
		//too -z
		if (transform.position.z < guidanceVec.z) {
			correctionVec.z -= guideStep;
		}
		zBeingCorrected = true;
	}


	void UpdateGuidance(){
		guidanceVec = go.transform.position;
	}


}
