using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToShop : MonoBehaviour, IEventInitializer
{

    public float lockoutTimer = 1f; //so we dont instantly trigger the teleport when we touch appear on top of the other trigger

	public Camera cam;
	public Camera playerCam;

	public bool inShop = false;

    public bool useLayersMethod = false;

    public const float yShopLayer = 100f;

    public GoToShop targetTrigger;
    public Vector3 targetLoc;

    //ours
    public Vector3 Loc;

    void Start()
    {
		playerCam = Camera.main;
        Loc = gameObject.transform.position;
		targetLoc = targetTrigger.gameObject.transform.position;
		if (cam != null) {
			cam.gameObject.SetActive (false);
		}
    }

    // Update is called once per frame
    void Update()
    {

    }

	//to avoid this being called on environment, all environment objects should be tagged as static
    public void OnTriggerEnter(Collider col)
    {
		if (!col.gameObject.isStatic) {
			Debug.Log("Triggered reeeee");

			switch (useLayersMethod) {
			case true:
				moveToShopLayers (col);
				break;

			case false:
				moveToShopDefault (col);
				break;
			}
		}
    }


    private void moveToShopLayers(Collider col)
    {
        try
        {
			col.gameObject.GetComponent<MovementController>().teleport(new Vector3(Loc.x, yShopLayer, Loc.z));
        }
        catch
        {
            Debug.Log("Invalid object triggering");
        }
    }

    private void moveToShopDefault(Collider col)
    {
       // try
        {
			if(inShop && col.gameObject.GetComponent<MovementController>().isPlayer){
				cam.gameObject.SetActive(false);
				playerCam.gameObject.SetActive(true);
			}
			targetTrigger.recieve(col);
            targetTrigger.gameObject.SetActive(false);
            col.gameObject.GetComponent<MovementController>().teleport(targetLoc);
            Invoke("EnableOther", lockoutTimer);
        }
     //   catch
        {
        }
    }

    void EnableOther()
    {
        targetTrigger.gameObject.SetActive(true);
    }

    public void initializeEvents()
    {
		//todo
    }

	public void recieve(Collider col){
		if (inShop && col.gameObject.GetComponent<MovementController>().isPlayer) {			
			cam.gameObject.SetActive (true);
			playerCam.gameObject.SetActive (false);



		}
	}
  

}