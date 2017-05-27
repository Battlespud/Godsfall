using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour {
    [SerializeField] private string LoadLevel; // makes it visible in the inspector
    public string doorName;

    //public Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(LoadLevel);
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), SceneManager.GetSceneByName(LoadLevel));
            if (collision.CompareTag("Player") && this.name == doorName)
            {
                Instantiate(GameObject.FindGameObjectWithTag("Player"));
                //Instantiate(GameObject.FindGameObjectWithTag("Player")).transform.position = GameObject.Find("sp" + doorName).transform.position;
                GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("sp" + doorName).transform.position;
                Debug.Log("Spawning at " + doorName);
            }
        }
    }
    /*private void OnLevelWasLoaded()
    {
        player.position = GameObject.FindGameObjectWithTag("LeftSpawn").transform.position;
    }*/
    /*private void OnLevelWasLoaded(int level)
    {
        Instantiate(GameObject.FindGameObjectWithTag("Player"));
        GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("sp" + doorName).transform.position;
        /*if (doorName == "right")
        {
            Instantiate(GameObject.FindGameObjectWithTag("Player")).transform.position = GameObject.Find("sp" + doorName).transform.position;
        }
        if (doorName == "left")
        {
            Instantiate(GameObject.FindGameObjectWithTag("Player")).transform.position = GameObject.Find("sp" + doorName).transform.position;
        }*/
        //Debug.Log("Spawning at " + doorName);
    //}
}
