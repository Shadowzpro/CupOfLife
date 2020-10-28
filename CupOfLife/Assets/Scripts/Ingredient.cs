using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Ingredient : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject theVoid;
    public GameObject spawnPoint;
    public Renderer dissolveShader;
    public Renderer liquidShader;
    public bool isLiquid = false;
    public bool isFadingIn = true;
    public float dissolveFloatProgress = 1;

    [Header("Hand")]
    // MAX DISTANCE YOU CAN BE TO PICK UP AN ITEM
    public float pickUpDistance = 0.3f;

    // REFERENCE TO EMPTY GAME OBJECT ON CLAW
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

    void Start()
    {
        gameObject.SetActive(true);
        theDest = GameObject.FindWithTag("Destination");

        DissolveIn();
    }

    void DissolveIn() 
    {
        isFadingIn = true;

        if (!isLiquid)
        {
            dissolveShader = GetComponent<Renderer>();
            dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveMetal");
            dissolveFloatProgress = 1;
            dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);
        }
        else 
        {
            dissolveShader = GetComponent<Renderer>();
            dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveMetal");
            dissolveFloatProgress = 1;
            dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);
        }
      
    }

    private void Update()
    {
        ///BEGIN FADE IN

        if (isFadingIn) 
        {
            dissolveFloatProgress = dissolveFloatProgress - (0.5f * Time.deltaTime);
            if (dissolveFloatProgress <= 0)
            {
                dissolveFloatProgress = -0.1f;
                isFadingIn = false;
                dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);
            }
            else 
            {
                dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);
            }
        }

        /// END FADE IN.

        Grab();
        Drop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the ingredient touches the "void" at the bottom of the map, destroy it and spawn a new one at its spawnpoint

        if (collision.gameObject.CompareTag("Void"))
        {
            Destroy(gameObject);
            prefab = (GameObject)Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
            prefab.GetComponent<Ingredient>().enabled = true;
        }
    }

    /// <summary>
    /// Grab the item
    /// </summary>
    public void Grab()
    {
        float distance = (theDest.transform.position - transform.position).magnitude;
        if (Input.GetKeyDown(KeyCode.Space) && theDest.transform.childCount < 1)
        {
            if (distance < pickUpDistance)
            {
                // GRAVITY DISABLED ON THIS ITEM
                GetComponent<Rigidbody>().isKinematic = true;
                // ITEMS POSITION IS SET TO EMPTY GAMEOBJECTS POSITION
                this.transform.position = theDest.transform.position;
                // ITEM IS SET AS A CHILD OF CLAW
                this.transform.parent = theDest.transform;
            }
        }
    }

    /// <summary>
    /// Drop the item
    /// </summary>
    public void Drop()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // ITEM IS NO LONGER A CHILD OF CLAW
            this.transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}