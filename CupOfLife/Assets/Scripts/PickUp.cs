using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //REFERENCE TO EMPTY GAME OBJECT ON CLAW
    public Transform theDest;

    //
    private void Update()
    {
        Grab();
        Drop();
    }

    //FUNCTION TO GRAB THIS ITEM
    void Grab()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //COLLIDER DISABLED ON THIS ITEM
            GetComponent<Collider>().enabled = false;
            //GRAVITY DISABLED ON THIS ITEM
            GetComponent<Rigidbody>().useGravity = false;
            //ITEMS POSITION IS SET TO EMPTY GAMEOBJECTS POSITION
            this.transform.position = theDest.position;
            //ITEM IS SET AS A CHILD OF CLAW
            this.transform.parent = GameObject.Find("Destination").transform;
        }
    }

    //FUNCTION TO DROP THIS ITEM
    void Drop()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //ITEM IS NO LONGER A CHILD OF CLAW
            this.transform.parent = null;
            //GRAVITY IS ENABLED ON THIS ITEM
            GetComponent<Rigidbody>().useGravity = true;
            //COLLIDER IS ENABLED ON THIS ITEM
            GetComponent<Collider>().enabled = true;
        }
    }
}
