using System;
using System.Collections;
using System.Data;
using UnityEngine;

[System.Serializable]
public class Ingredient : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject theVoid;
    public GameObject spawnPoint;
    public Renderer dissolveShader;
    public Renderer dissolveShader2;
    public Renderer liquidShader;
    public bool isLiquid = false;
    public bool isFadingIn = true;
    public float dissolveFloatProgress = 1;
    public float liquidLevelProgress = 1;
    public GameObject internalLiquid;
    public GameObject internalGlass;
    public GameObject lidsAndSolids;

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
        if (ingredientName == "Milk")
        {
            gameObject.GetComponentInChildren<Wobble>().enabled = true;
            isLiquid = true;
        }
        else if (ingredientName == "GreenJuice")
        {
            gameObject.GetComponentInChildren<WobbleViscous>().enabled = true;
            isLiquid = true;
        }
        else if (ingredientName == "Oil")
        {
            gameObject.GetComponentInChildren<Wobble>().enabled = true;
            isLiquid = true;
        }

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
            if (ingredientName == "Milk")
            {
                dissolveShader2 = internalGlass.GetComponent<Renderer>();
                dissolveShader2.material.shader = Shader.Find("Shader Graphs/DissolveGlass");
                dissolveFloatProgress = 0;
                dissolveShader2.material.SetFloat("dissolveProgress", dissolveFloatProgress);

                dissolveShader = lidsAndSolids.GetComponent<Renderer>();
                dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveMetal");
                dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);

                liquidShader = internalLiquid.GetComponent<Renderer>();
                liquidShader.material.shader = Shader.Find("Unlit/SpecialFX/Liquid");

                liquidLevelProgress = 1;
                liquidShader.material.SetFloat("_FillAmount", liquidLevelProgress);

            }
            else if (ingredientName == "GreenJuice")
            {
                dissolveShader2 = internalGlass.GetComponent<Renderer>();
                dissolveShader2.material.shader = Shader.Find("Shader Graphs/DissolveGlass");
                dissolveFloatProgress = 0;
                dissolveShader2.material.SetFloat("dissolveProgress", dissolveFloatProgress);
                

                dissolveShader = lidsAndSolids.GetComponent<Renderer>();
                dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveMetal");
                dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);

                liquidShader = internalLiquid.GetComponent<Renderer>();
                liquidShader.material.shader = Shader.Find("Unlit/SpecialFX/LiquidViscous");

                liquidLevelProgress = 1;
                liquidShader.material.SetFloat("_FillAmount", liquidLevelProgress);
            }
            else if (ingredientName == "Oil") 
            {
                dissolveShader = lidsAndSolids.GetComponent<Renderer>();
                dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveMetal");
                dissolveFloatProgress = 0;
                dissolveShader.material.SetFloat("dissolveProgress", dissolveFloatProgress);


                dissolveShader2 = internalGlass.GetComponent<Renderer>();
                dissolveShader2.material.shader = Shader.Find("Shader Graphs/DissolveGlass");
                dissolveFloatProgress = 0;
                dissolveShader2.material.SetFloat("dissolveProgress", dissolveFloatProgress);


                liquidShader = internalLiquid.GetComponent<Renderer>();
                liquidShader.material.shader = Shader.Find("Unlit/SpecialFX/LiquidOil");

                liquidLevelProgress = 1;
                liquidShader.material.SetFloat("_FillAmount", liquidLevelProgress);

            }
        }
    }

    private void UpdateFade() 
    {
        ///BEGIN FADE IN

        if (isFadingIn)
        {
            if (!isLiquid)
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
            else //if (ingredientName == "Milk") 
            {
                    dissolveFloatProgress = dissolveFloatProgress + (0.5f * Time.deltaTime);
                    liquidLevelProgress = liquidLevelProgress - (0.25f * Time.deltaTime);

                    if (dissolveFloatProgress >= 1)
                    {
                        dissolveFloatProgress = 1f;
                        liquidLevelProgress = 0.5f;
                        isFadingIn = false;
                        dissolveShader.material.SetFloat("dissolveProgress", 1 - dissolveFloatProgress);
                        dissolveShader2.material.SetFloat("dissolveProgress", dissolveFloatProgress);
                        liquidShader.material.SetFloat("_FillAmount", liquidLevelProgress);
                    }
                    else
                    {
                        dissolveShader.material.SetFloat("dissolveProgress", 1-dissolveFloatProgress);
                        dissolveShader2.material.SetFloat("dissolveProgress", dissolveFloatProgress);
                        liquidShader.material.SetFloat("_FillAmount", liquidLevelProgress);
                    }

            }
        }
        /// END FADE IN.
    }

    private void Update()
    {
        UpdateFade();

        Grab();
        Drop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the ingredient touches the "void" at the bottom of the map, destroy it and spawn a new one at its spawnpoint

        if (collision.gameObject.CompareTag("Void"))
        {
            Destroy(gameObject);

            if (prefab.CompareTag("Sugar")) return;
            if (prefab.CompareTag("Cogs")) return;
            if (prefab.CompareTag("Eyeballs")) return;

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
        if (Input.GetMouseButtonDown(0) && theDest.transform.childCount < 1)
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
        if (Input.GetMouseButtonUp(0))
        {
            // ITEM IS NO LONGER A CHILD OF CLAW
            this.transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}