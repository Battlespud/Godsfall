using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputCatcher {

	public MovementController movementController;


	//Keybinds
		//constants are always static anyway
	public const KeyCode ForwardKey = KeyCode.W;
	public const KeyCode BackKey = KeyCode.S;
	public const KeyCode LeftKey = KeyCode.A;
	public const KeyCode RightKey = KeyCode.D;

	public const KeyCode RollKey = KeyCode.Space;


	public bool LeftClick(){
		return Input.GetMouseButtonDown (0);
	}

	public bool RightClick(){
		return Input.GetMouseButtonDown (1);
	}

	public bool MiddleClick(){
		return Input.GetMouseButtonDown (2);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
