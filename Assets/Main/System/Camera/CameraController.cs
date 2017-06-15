using UnityEngine;
using System.Collections;


	public class CameraController : MonoBehaviour
	{
		public CameraModeManager manager;
		public Transform target;
		public float angleY = 35;
		public float rotationSmoothing = 10;
		public float rotationSensitivity = 7;
		public float distance = 10;
	public float verticalOffset = 12; //for overhead
	public Camera cam;

		private Vector3 _angle = new Vector3();
		private Quaternion _oldRotation = new Quaternion();

		private Transform _t;

		public Vector2 CurrentRotation { get { return _angle; } }

		void Start()
		{
			_t = transform;
			_oldRotation = _t.rotation;
			_angle.y = angleY;
		}

	private void checkZoomInput(){
		var zoom = Input.GetAxis ("Mouse ScrollWheel");
		cam.fieldOfView += -20*zoom;
		if (cam.fieldOfView < 18)
			cam.fieldOfView = 18;
		if (cam.fieldOfView > 75)
			cam.fieldOfView = 75;
	}

		void Update()
		{
		if(target && Input.GetMouseButton(1) && manager.cameraMode == CameraMode.NORMAL)
			{
				_angle.x += Input.GetAxis("Mouse X") * rotationSensitivity;
				RobitTools.ClampAngle(ref _angle);
			}
		checkZoomInput ();
		}

		void LateUpdate()
		{
			if(target)
			{

			switch(manager.cameraMode){
			case(CameraMode.NORMAL):
				{
					Normal ();
					break;
				}

			case(CameraMode.SIDESCROLLER):
				{
					SideScroller ();
					break;
				}

			case(CameraMode.OVERHEAD):
				{
					OverHead ();
					break;
				}
	}

			}
		}


	void Normal(){
		Quaternion angleRotation = Quaternion.Euler (_angle.y, _angle.x, 0);
		Quaternion currentRotation = Quaternion.Lerp (_oldRotation, angleRotation, Time.deltaTime * rotationSmoothing);

		_oldRotation = currentRotation;

		_t.position = target.position - currentRotation * Vector3.forward * distance;
		_t.LookAt (target.position, Vector3.up);
	}

	void SideScroller(){
		Quaternion angleRotation = Quaternion.Euler (_angle.y, _angle.x, 0);
		Quaternion currentRotation = Quaternion.Lerp (_oldRotation, angleRotation, Time.deltaTime * rotationSmoothing);

		_t.position = target.position + new Vector3 (verticalOffset, 0f, 0f);
		_t.LookAt (target.position, Vector3.up);

	}

	void OverHead(){
		Quaternion angleRotation = Quaternion.Euler (_angle.y, _angle.x, 0);
		Quaternion currentRotation = Quaternion.Lerp (_oldRotation, angleRotation, Time.deltaTime * rotationSmoothing);

		_t.position = target.position + new Vector3 (0f, verticalOffset, 0f);
		_t.LookAt (target.position, Vector3.up);

	}


	}
