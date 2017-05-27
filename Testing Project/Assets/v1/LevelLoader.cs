using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Transform player;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("test" + collider.name);

        if (collider.name == "right")
        {
            SceneManager.UnloadSceneAsync(0);
            SceneManager.LoadScene(1);
            player.position = GameObject.Find("spawn point left").transform.position; //spawn point left because youre coming from the right, into the left //this does work, glitchy, but funamdentally works
        }
        if (collider.name == "left")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //player.position = GameObject.Find("spawn point left").transform.position; // same ^
        }
        //Invoke("LoadNext", 1f);
    }

    private void LoadNext()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Debug.Log("loaded");
    }
}
