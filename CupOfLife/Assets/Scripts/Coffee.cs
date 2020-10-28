using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Coffee : MonoBehaviour
{
    [Header("Hand")]
    // MAX DISTANCE YOU CAN BE TO PICK UP AN ITEM
    public float pickUpDistance = 0.3f;

    // REFERENCE TO EMPTY GAME OBJECT ON CLAW
    public GameObject theDest;

    [Header("Coffee")]
    /// <summary>
    /// The Ingredients
    /// </summary>
    public List<Ingredient> ingredients;

    public Renderer dissolveShader;
    public bool isFadingIn = true;
    public bool isFadingOut = false;
    public float dissolveFloatProgress = 1;


    void Start()
    {
        theDest = GameObject.FindWithTag("Destination");
        DissolveIn();
    }

    public void DissolveIn() 
    {
        isFadingIn = true;
        dissolveShader = GetComponent<Renderer>();
        dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveMetal");
        dissolveFloatProgress = 1;
        dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);
    }

    public void DissolveOut()
    {
        isFadingOut = true;
        dissolveShader = GetComponent<Renderer>();
        dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveMetal");
        dissolveFloatProgress = -0.1f;
        dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);
    }



    private void Update()
    {
        Grab();
        Drop();

        //update dissolve
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
        /// BEGIN FADE OUT
        if (isFadingIn)
        {
            dissolveFloatProgress = dissolveFloatProgress + (0.5f * Time.deltaTime);
            if (dissolveFloatProgress >= 1)
            {
                dissolveFloatProgress = 1f;
                isFadingOut = false;
                dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);
            }
            else
            {
                dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);
            }
        }

        ///END FADE OUT


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