using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionResolver {

	static CameraDirection cd = Camera.main.GetComponent<CameraDirection> ();

	static  Vector3 UL = new Vector3(-1,0,1);
	static  Vector3 UR = new Vector3(1,0,1);
	static  Vector3 DL = new Vector3(-1,0,-1);
	static  Vector3 DR = new Vector3(1,0,-1);

	#region Ray adjustment stuff, not really important
	public static Vector3 RayDirection(SpriteController sc){
		Vector3 dirVec = new Vector3 ();
		Facing facing = sc.finalFacing;
		switch (cd.facing) {

		case (Facing.D):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = Vector3.back;
						break;
					}
				case(Facing.DL):{
						dirVec = DL;
						break;
					}
				case(Facing.L):{
						dirVec = Vector3.left;
						break;
					}
				case(Facing.UL):{
						dirVec = UL;
						break;
					}
				case(Facing.U):{
						dirVec = Vector3.forward;
						break;
					}
				case(Facing.UR):{
						dirVec = UR;
						break;
					}
				case(Facing.R):{
						dirVec = Vector3.right;
						break;
					}
				case(Facing.DR):{
						dirVec = DR;
						break;

					}
				}
				dirVec *= -1;
				break;
			}

		case (Facing.L):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = Vector3.right;
						break;
					}
				case(Facing.DL):{
						dirVec = DR;
						break;
					}
				case(Facing.L):{
						dirVec = Vector3.back;
						break;
					}
				case(Facing.UL):{
						dirVec = DL;
						break;

					}
				case(Facing.U):{
						dirVec = Vector3.left;
						break;
					}
				case(Facing.UR):{
						dirVec = UL;
						break;
					}
				case(Facing.R):{
						dirVec = Vector3.forward;
						break;
					}
				case(Facing.DR):{
						dirVec = UR;
						break;
					}
				}
				break;
			}
		case (Facing.U):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = Vector3.back;
						break;
					}
				case(Facing.DL):{
						dirVec = new Vector3 (-1, 0, -1);
						break;
					}
				case(Facing.L):{
						dirVec = Vector3.left;
						break;
					}
				case(Facing.UL):{
						dirVec = new Vector3 (-1, 0, 1);
						break;
					}
				case(Facing.U):{
						dirVec = Vector3.forward;
						break;
					}
				case(Facing.UR):{
						dirVec = new Vector3 (1, 0, 1);
						break;
					}
				case(Facing.R):{
						dirVec = Vector3.right;
						break;
					}
				case(Facing.DR):{
						dirVec = new Vector3 (-1, 0, 1);
						break;
					}
				}
				break;
			}
		case (Facing.R):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = Vector3.right;
						break;
					}
				case(Facing.DL):{
						//	dirVec = new Vector3 (-1, 0, -1);
						break;
					}
				case(Facing.L):{
						dirVec = Vector3.back;
						break;
					}
				case(Facing.UL):{
						//	dirVec = new Vector3 (-1, 0, 1);
						break;

					}
				case(Facing.U):{
						dirVec = Vector3.left;
						break;
					}
				case(Facing.UR):{
						//	dirVec = new Vector3 (1, 0, 1);
						break;
					}
				case(Facing.R):{
						dirVec = Vector3.forward;
						break;
					}
				case(Facing.DR):{
						//	dirVec = new Vector3 (-1, 0, 1);
						break;
					}
				}
				dirVec *= -1;
				break;
			}
		}
		return dirVec;
	}


	#endregion

	#region used to adjust the vector that movementController sends to spritecontroller based upon camera angle.
	public static Vector3 VectorProcessor(Vector3 dirVec){
		Debug.Log (dirVec);
		switch(cd.facing)
		{
		case (Facing.U):
			{
				//Nothing to be done, this is default
				break;
			}
		case (Facing.UR):
			{
				if (dirVec == Vector3.forward) {
					dirVec = DL;
				} else if (dirVec == UR) {
					dirVec = Vector3.left;
				}else if (dirVec == Vector3.right) {
					dirVec = UL;
				}else if (dirVec == DR) {
					dirVec = Vector3.forward;
				}else if (dirVec == Vector3.back) {
					dirVec = UR;
				} else if (dirVec == DL) {
					dirVec = Vector3.right;
				}else if (dirVec == Vector3.left) {
					dirVec = DR;
				} else if (dirVec == UL) {
					dirVec = Vector3.back;
				}
				dirVec *= -1;
				break;
			}
		case (Facing.R):
			{
				if (dirVec == Vector3.forward) {
					dirVec = Vector3.left;
				} else if (dirVec == UR) {
					dirVec = UL;
				}else if (dirVec == Vector3.right) {
					dirVec = Vector3.forward;
				}else if (dirVec == DR) {
					dirVec = UR;
				}else if (dirVec == Vector3.back) {
					dirVec = Vector3.right;
				} else if (dirVec == DL) {
					dirVec = DR;
				}else if (dirVec == Vector3.left) {
					dirVec = Vector3.back;
				} else if (dirVec == UL) {
					dirVec = DL;
				}
				dirVec *= -1;
				break;
			}
		case (Facing.DR): 
			{
				if (dirVec == Vector3.forward) {
					dirVec = UL;
				} else if (dirVec == UR) {
					dirVec = Vector3.forward;
				}else if (dirVec == Vector3.right) {
					dirVec = UR;
				}else if (dirVec == DR) {
					dirVec = Vector3.right;
				}else if (dirVec == Vector3.back) {
					dirVec = DR;
				} else if (dirVec == DL) {
					dirVec = Vector3.back;
				}else if (dirVec == Vector3.left) {
					dirVec = DL;
				} else if (dirVec == UL) {
					dirVec = Vector3.left;
				}
				dirVec *= -1;
				break;
			}
		case (Facing.D):
			{
				dirVec *= -1;
				break;
			}
		case (Facing.DL):
			{
				if (dirVec == Vector3.forward) {
					dirVec = DL;
				} else if (dirVec == UR) {
					dirVec = Vector3.left;
				}else if (dirVec == Vector3.right) {
					dirVec = UL;
				}else if (dirVec == DR) {
					dirVec = Vector3.forward;
				}else if (dirVec == Vector3.back) {
					dirVec = UR;
				} else if (dirVec == DL) {
					dirVec = Vector3.right;
				}else if (dirVec == Vector3.left) {
					dirVec = DR;
				} else if (dirVec == UL) {
					dirVec = Vector3.back;
				}
				break;
			}
		case (Facing.L):
			{
				if (dirVec == Vector3.forward) {
					dirVec = Vector3.left;
				} else if (dirVec == UR) {
					dirVec = UL;
				}else if (dirVec == Vector3.right) {
					dirVec = Vector3.forward;
				}else if (dirVec == DR) {
					dirVec = UR;
				}else if (dirVec == Vector3.back) {
					dirVec = Vector3.right;
				} else if (dirVec == DL) {
					dirVec = DR;
				}else if (dirVec == Vector3.left) {
					dirVec = Vector3.back;
				} else if (dirVec == UL) {
					dirVec = DL;
				}
				break;
			}
		case (Facing.UL): 
			{
				if (dirVec == Vector3.forward) {
					dirVec = UL;
				} else if (dirVec == UR) {
					dirVec = Vector3.forward;
				}else if (dirVec == Vector3.right) {
					dirVec = UR;
				}else if (dirVec == DR) {
					dirVec = Vector3.right;
				}else if (dirVec == Vector3.back) {
					dirVec = DR;
				} else if (dirVec == DL) {
					dirVec = Vector3.back;
				}else if (dirVec == Vector3.left) {
					dirVec = DL;
				} else if (dirVec == UL) {
					dirVec = Vector3.left;
				}
				break;
			}



		default:
			break;
		}

		return dirVec;

	}
	#endregion
}
