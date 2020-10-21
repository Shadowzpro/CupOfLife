using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //MAX DISTANCE YOU CAN BE TO PICK UP AN ITEM
    public float pickUpDistance = 0.3f;
    //REFERENCE TO EMPTY GAME OBJECT ON CLAW
    public GameObject theDest;

    void Start()
    {
        theDest = GameObject.FindWithTag("Destination");
    }
    //
    private void Update()
    {
        Grab();
        Drop();
    }

    //FUNCTION TO GRAB THIS ITEM
    void Grab()
    {
        float distance = (theDest.transform.position - transform.position).magnitude;
        if (Input.GetKeyDown(KeyCode.Space) && theDest.transform.childCount < 1) 
        {
            if (distance < pickUpDistance)
            {
                //if (GetComponent<Rigidbody>() != null)
                //{
                    //COLLIDER DISABLED ON THIS ITEM
                    //GetComponent<Collider>().enabled = false;
                    //GRAVITY DISABLED ON THIS ITEM
                    GetComponent<Rigidbody>().isKinematic = true;
                    //GetComponent<Rigidbody>().useGravity = false;
                    //ITEMS POSITION IS SET TO EMPTY GAMEOBJECTS POSITION
                    this.transform.position = theDest.transform.position;
                    //ITEM IS SET AS A CHILD OF CLAW
                    this.transform.parent = theDest.transform;
                //}
            }
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
            //GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            //COLLIDER IS ENABLED ON THIS ITEM
            //GetComponent<Collider>().enabled = true;
        }
    }
}
