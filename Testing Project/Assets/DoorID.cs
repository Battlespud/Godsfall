using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorID : MonoBehaviour {
    public float level;
    public float door;

    public Vector2 GetID()
    {
        return new Vector2(level, door);
    }
}
