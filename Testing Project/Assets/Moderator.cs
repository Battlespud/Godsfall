using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Moderator
{

    //save data, load scene, register doors, initialize player

    //x = level, y = door
    public static Vector2 savedID = new Vector2();
    public static List<GameObject> doorList = new List<GameObject>();
    public static GameObject targetDoor;
    public static GameObject player;


    public static void setSavedID(Vector2 i)
    {
        savedID = i;
        Debug.Log(String.Format("Door ID {0} is now saved.", savedID));

        loadNextScene();
    }

    private static void loadNextScene()
    {
        SceneManager.LoadScene((int)savedID.x);
        registerDoors();
    }

    private static void registerDoors()
    {
        doorList.Clear();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("door"))
        {
            doorList.Add(obj);
        }
        SelectDoor();
    }

    private static void SelectDoor()
    {
        foreach (GameObject obj in doorList)
        {
            if (obj.GetComponent<DoorID>().GetID() == savedID)
            {
                targetDoor = obj;
            }
        }
    }

    private static void LoadPlayer()
    {
        float f = targetDoor.transform.position.x;
        float offset = 5;
        player = GameObject.Instantiate(GameObject.FindGameObjectWithTag("Player"));
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
    }
}
