using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//x = level, y = door
	public  Vector3 savedID = new Vector2();
	public  List<GameObject> doorList = new List<GameObject>();
	public  GameObject targetDoor;
	public  GameObject player;

	public void Awake(){
		DontDestroyOnLoad (transform.gameObject);

	}

	public  void setSavedID(Vector3 i)
	{
		savedID = i;
		Debug.Log(string.Format("Door ID {0} is now saved.", savedID));
		loadNextScene();
	}

	private  void loadNextScene()
	{
		SceneManager.LoadScene((int)savedID.y);
		registerDoors();
	}

	private  void registerDoors()
	{
		doorList.Clear();
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("door"))
		{
			doorList.Add(obj);
		}
		SelectDoor();
	}

	private  void SelectDoor()
	{
		foreach (GameObject obj in doorList)
		{
			if (obj.GetComponent<DoorID>().GetID().z == savedID.z)
			{
				targetDoor = obj;
			}
		}
		doorList.Clear ();
		LoadPlayer ();
	}

	private  void LoadPlayer()
	{
		float f = targetDoor.transform.position.x;
		float offset = 2.75f;

		bool thisCannotContinue = f < 0;
		switch(thisCannotContinue) 
		{
		case true:
			player.transform.position = new Vector3(f + offset, targetDoor.transform.position.y, 0f);
			break;

		case false:
			player.transform.position = new Vector3(f + offset* -1, targetDoor.transform.position.y, 0f);
			break; 

		default:
			Debug.Log("We're all fucked.jpg");
			break;
		}
		targetDoor = null;
	}
}
