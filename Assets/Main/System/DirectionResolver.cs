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
				break;
			}
		case (Facing.L):
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


		}


		return dirVec;



	}

}
