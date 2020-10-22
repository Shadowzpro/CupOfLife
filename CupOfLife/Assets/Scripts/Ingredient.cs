using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Ingredient : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject theVoid;
    public GameObject spawnPoint;

    [Header("Hand")]
    //MAX DISTANCE YOU CAN BE TO PICK UP AN ITEM
    public float pickUpDistance = 0.3f;

    //REFERENCE TO EMPTY GAME OBJECT ON CLAW
    public GameObject theDest;

    [Header("Ingredient")]
    /// <summary>
    /// The prefab
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// The name of the ingredient
    /// </summary>
    public string ingredientName;

    public bool isLiquid = false;

    void Start()
    {
        theDest = GameObject.FindWithTag("Destination");
    }

    private void Update()
    {
        Grab();
        Drop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the ingredient touches the "void" at the bottom of the map, destroy it and spawn a new one at its spawnpoint

        if (collision.gameObject.CompareTag("Void"))
        {
            Destroy(gameObject);
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
        }
    }

    //FUNCTION TO GRAB THIS ITEM
    void Grab()
    {
        float distance = (theDest.transform.position - transform.position).magnitude;
        if (Input.GetKeyDown(KeyCode.Space) && theDest.transform.childCount < 1)
        {
            if (distance < pickUpDistance)
            {
                //GRAVITY DISABLED ON THIS ITEM
                GetComponent<Rigidbody>().isKinematic = true;
                //ITEMS POSITION IS SET TO EMPTY GAMEOBJECTS POSITION
                this.transform.position = theDest.transform.position;
                //ITEM IS SET AS A CHILD OF CLAW
                this.transform.parent = theDest.transform;
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
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}