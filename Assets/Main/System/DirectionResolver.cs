using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionResolver {

	static CameraDirection cd = Camera.main.GetComponent<CameraDirection> ();
	static Vector3 U = Vector3.forward;
	static Vector3 D = Vector3.back;
	static Vector3 L = Vector3.left;
	static Vector3 R = Vector3.right;

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
						dirVec = D;
						break;
					}
				case(Facing.DL):{
						dirVec = DL;
						break;
					}
				case(Facing.L):{
						dirVec = L;
						break;
					}
				case(Facing.UL):{
						dirVec = UL;
						break;
					}
				case(Facing.U):{
						//something screwy here
						dirVec = U;
						break;
					}
				case(Facing.UR):{
						dirVec = UR;
						break;
					}
				case(Facing.R):{
						dirVec = R;
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
		case (Facing.DL):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = UR;
						break;
					}
				case(Facing.DL):{
						dirVec = R;
						break;
					}
				case(Facing.L):{
						dirVec = DR;
						break;
					}
				case(Facing.UL):{
						dirVec = D;
						break;
					}
				case(Facing.U):{
						//something screwy here
						dirVec = DL;
						break;
					}
				case(Facing.UR):{
						dirVec = L;
						break;
					}
				case(Facing.R):{
						dirVec = UL;
						break;
					}
				case(Facing.DR):{
						dirVec = U;
						break;

					}
				}
				//dirVec *= -1;
				break;
			}
		case (Facing.L):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = R;
						break;
					}
				case(Facing.DL):{
						dirVec = DR;
						break;
					}
				case(Facing.L):{
						dirVec = D;
						break;
					}
				case(Facing.UL):{
						dirVec = DL;
						break;

					}
				case(Facing.U):{
						dirVec = L;
						break;
					}
				case(Facing.UR):{
						dirVec = UL;
						break;
					}
				case(Facing.R):{
						dirVec = U;
						break;
					}
				case(Facing.DR):{
						dirVec = UR;
						break;
					}
				}
				break;
			}
		
		case (Facing.UL):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = UL;
						break;
					}
				case(Facing.DL):{
						dirVec = U;
						break;
					}
				case(Facing.L):{
						dirVec = UR;
						break;
					}
				case(Facing.UL):{
						dirVec = R;
						break;

					}
				case(Facing.U):{
						dirVec = DR;
						break;
					}
				case(Facing.UR):{
						dirVec = D;
						break;
					}
				case(Facing.R):{
						dirVec = DL;
						break;
					}
				case(Facing.DR):{
						dirVec = L;
						break;
					}
				}
				dirVec *= -1;
				break;
			}
		case (Facing.U):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = D;
						break;
					}
				case(Facing.DL):{
						dirVec = DL;
						break;
					}
				case(Facing.L):{
						dirVec = L;
						break;
					}
				case(Facing.UL):{
						dirVec = UL;
						break;
					}
				case(Facing.U):{
						dirVec = U;
						break;
					}
				case(Facing.UR):{
						dirVec = UR;
						break;
					}
				case(Facing.R):{
						dirVec = R;
						break;
					}
				case(Facing.DR):{
						dirVec = DR;
						break;
					}
				}
				break;
			}
		case (Facing.UR):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = UR;
						break;
					}
				case(Facing.DL):{
						dirVec = R;
						break;
					}
				case(Facing.L):{
						dirVec = DR;
						break;
					}
				case(Facing.UL):{
						dirVec = D;
						break;
					}
				case(Facing.U):{
						dirVec = DL;
						break;
					}
				case(Facing.UR):{
						dirVec = L;
						break;
					}
				case(Facing.R):{
						dirVec = UL;
						break;
					}
				case(Facing.DR):{
						dirVec = U;
						break;

					}
				}
				dirVec *= -1;
				break;
			}
		case (Facing.R):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = R;
						break;
					}
				case(Facing.DL):{
						dirVec = DR;
						break;
					}
				case(Facing.L):{
						dirVec = D;
						break;
					}
				case(Facing.UL):{
						dirVec = DL;
						break;

					}
				case(Facing.U):{
						dirVec = L;
						break;
					}
				case(Facing.UR):{
						dirVec = UL;
						break;
					}
				case(Facing.R):{
						dirVec = U;
						break;
					}
				case(Facing.DR):{
						dirVec = UR;
						break;
					}
				}
				dirVec *= -1;
				break;
			}
		case (Facing.DR):
			{
				switch (facing) {
				case(Facing.D):{
						dirVec = UL;
						break;
					}
				case(Facing.DL):{
						dirVec = U;
						break;
					}
				case(Facing.L):{
						dirVec = UR;
						break;
					}
				case(Facing.UL):{
						dirVec = R;
						break;

					}
				case(Facing.U):{
						dirVec = DR;
						break;
					}
				case(Facing.UR):{
						dirVec = D;
						break;
					}
				case(Facing.R):{
						dirVec = DL;
						break;
					}
				case(Facing.DR):{
						dirVec = L;
						break;
					}
				}
				//dirVec *= -1;
				break;
			}
		}

		//this should be where the sprite is facing. i think.
		return dirVec;
	}


	#endregion

	#region used to adjust the vector that movementController sends to spritecontroller based upon camera angle.
	public static Vector3 VectorProcessor(Vector3 dirVec){
//		Debug.Log (dirVec);
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
