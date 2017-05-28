using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorID : MonoBehaviour {
	public GameManager gameManager;
	public float levelFrom;
    public float levelTo;
	public float door;

	public void Awake(){
		gameManager = GameObject.FindGameObjectWithTag ("ScriptContainer").GetComponent<GameManager>();
	}

    public Vector3 GetID()
    {
		return new Vector3(levelFrom,levelTo, door);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{	
			Debug.Log ("Entered Door");
			gameManager.setSavedID (GetID ());
		}
	}

}
