using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Moderator
{

    //save data, load scene, register doors, initialize player

    //x = level, y = door
    public static Vector2 savedID = new Vector2();
    public static List<GameObject> doorsList = new List<GameObject>();
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
        doorList.clear();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("door"))
        {
            doorsList.add(obj);
        }
        SelectDoor();
    }

    private static void SelectDoor()
    {
        foreach (GameObject obj in doorsList)
        {
            if (obj.GetComponent<DoorID>().GetID() == savedID)
            {
                targetDoor = obj;
            }
        }
    }

    private static void LoadPlayer()
    {
        private static float f = obj.transform.position.x;
        private static float offset = 5;
        player = Instantiate(GameObject.FindGameObjectWithTag("Player"));
    
        switch(f) 
        {
            case (f<0):
                player.transform.position = new Vector3(targetDoor.transform.position.x + offset, targetDoor.transform.position.y, 0f);
                break;
            
            case (f>0):
                player.transform.position = new Vector3(targetDoor.transform.position.x + offset* -1, targetDoor.transform.position.y, 0f);
                break; 
            default:
            Debug.Log("We're all fucked.jpg");
                break;
        }
    }
}
}