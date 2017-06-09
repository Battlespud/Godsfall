using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToShop : MonoBehaviour, IEventInitializer
{

    public int lockoutTimer = 3; //so we dont instantly trigger the teleport when we touch appear on top of the other trigger

    public bool useLayersMethod = false;

    public const float yShopLayer = 100f;

    public GoToShop targetTrigger;
    public Vector3 targetLoc;

    //ours
    public Vector3 Loc;

    void Start()
    {
        Loc = gameObject.transform.position;
        targetLoc = targetTrigger.Loc;

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerEnter(Collision col)
    {
        Debug.Log("Triggered reeeee");
        switch (useLayersMethod)
        {
            case true:
                moveToShopLayers(col);
                break;

            case false:
                moveToShopDefault(col);
                break;
        }
    }


    private void moveToShopLayers(Collision col)
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

    private void moveToShopDefault(Collision col)
    {
        try
        {
            targetTrigger.gameObject.SetActive(false);
            col.gameObject.GetComponent<MovementController>().teleport(targetLoc);
            Invoke("EnableOther", lockoutTimer);
        }
        catch
        {
            Debug.Log("Invalid object triggering");
        }
    }
    void EnableOther()
    {
        targetTrigger.gameObject.SetActive(true);
    }

    public void initializeEvents()
    {

    }
    void teleport()
    {

    }

}