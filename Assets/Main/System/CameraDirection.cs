using UnityEngine;
using System.Collections;


	public class CameraDirection : MonoBehaviour
	{
		private CameraController _camera;
		public Facing facing = Facing.D;

		
		public virtual Vector2 Angle { get { return _camera.CurrentRotation; } }

		public virtual void Awake()
		{
			_camera = GetComponent<CameraController>();
		}

		public virtual void LateUpdate()
		{
			float rX = _camera.CurrentRotation.x;
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
		}
	}
