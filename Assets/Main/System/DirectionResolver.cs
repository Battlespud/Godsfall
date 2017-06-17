using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionResolver {

	static CameraDirection cd = Camera.main.GetComponent<CameraDirection> ();

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


	public static Vector3 VectorProcessor(Vector3 dirVec){
		switch(cd.facing)
		{
		case (Facing.U):
			{
				//Nothing to be done, this is default
				break;
			}
		case (Facing.D):
			{

				dirVec *= -1;
				break;
			}
		case (Facing.L):
			{
				if (dirVec == Vector3.forward) {
					dirVec = Vector3.left;
				}
				else if (dirVec == Vector3.back) {
					dirVec = Vector3.right;
				}
				else if (dirVec == Vector3.left) {
					dirVec = Vector3.back;
				}
				else if (dirVec == Vector3.right) {
					dirVec = Vector3.forward;
				}
				break;
			}
		case (Facing.R):
			{
				if (dirVec == Vector3.forward) {
					dirVec = Vector3.left;
				}
				else if (dirVec == Vector3.back) {
					dirVec = Vector3.right;
				}
				else if (dirVec == Vector3.left) {
					dirVec = Vector3.back;
				}
				else if (dirVec == Vector3.right) {
					dirVec = Vector3.forward;
				}
				dirVec *= -1;
				break;
			}

		default:
			break;
		}

		return dirVec;

	}

}
