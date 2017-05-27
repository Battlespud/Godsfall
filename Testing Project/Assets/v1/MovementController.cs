using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
        {
            this.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            this.transform.position += new Vector3(-1 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 25f));
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -25f));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log(GameObject.FindGameObjectWithTag("Player").transform.position.ToString());
        }
    }
}
