using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionResolver {

	public static Vector3 RayDirection(SpriteController sc){
		Vector3 dirVec = new Vector3 ();
		Facing facing = sc.finalFacing;
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

		return dirVec;



	}

}
