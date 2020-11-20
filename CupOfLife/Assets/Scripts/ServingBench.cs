using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServingBench : MonoBehaviour
{
    //REFERENCE TO PARTICLE SYSTEM THAT MAY BE ATTACHED
    //public ParticleSystem confetti;
    [HideInInspector]
    public GameObject drink;
    public float secondsToDestroy = 4;
    public float customerLeaving = 3.125f;
    private Rigidbody drinkRigidBody;
    public Text TipJar;
    [HideInInspector]
    public int ordersComplete = 0; // <--- Was static so if something doesn't work might be this???
    [HideInInspector]
    public int ordersFailed = 0;   // <--- Was static so if something doesn't work might be this???
    private IEnumerator coroutine;
    [HideInInspector]
    public bool isCoffeeMade = false;
    [HideInInspector]
    public bool isCoffeeFadingAway = false;
    [HideInInspector]
    public Renderer dissolveShader;
    [HideInInspector]
    public float coffeeDissolveProg;
    public Customer currentCustomer;
    private bool correctCoffee;
    public GameObject[] coinPile;

    private void Update()
    {
        if (isCoffeeFadingAway)
        {
            if (!drink) 
            { isCoffeeFadingAway = false; }
            CoffeeFadeOut();
        }
        if (isCoffeeMade)
        {
            AssignCoffee();
        }

        if (ordersComplete == 2)
        {
            coinPile[0].SetActive(true);
        }
        if (ordersComplete == 4)
        {
            coinPile[1].SetActive(true);
            coinPile[0].SetActive(false);
        }
        if (ordersComplete == 6)
        {
            coinPile[2].SetActive(true);
            coinPile[1].SetActive(false);
        }
        if (ordersComplete == 8)
        {
            coinPile[3].SetActive(true);
            coinPile[2].SetActive(false);
        }
        if (ordersComplete == 10)
        {
            coinPile[4].SetActive(true);
            coinPile[3].SetActive(false);
        }
    }

    private void CoffeeFadeOut() 
    {
        //Update fader
        
        dissolveShader.material.SetColor("edgeColor", Color.red);
        
        coffeeDissolveProg = coffeeDissolveProg + (0.5f * Time.deltaTime);
        dissolveShader.material.SetFloat("dissolveProgress", coffeeDissolveProg);
        if (coffeeDissolveProg >= 1) 
        {
            coffeeDissolveProg = 1;
            dissolveShader.material.SetFloat("dissolveProgress", coffeeDissolveProg);
            isCoffeeFadingAway = false;
        }
    }

    private void AssignCoffee()
    {
        drink = GameObject.FindWithTag("Coffee");
        drinkRigidBody = drink.GetComponent<Rigidbody>();
        coroutine = UpdateText(secondsToDestroy);
    }

    //FUNCTION RUNS WHEN SOMETHING TRIGGERS THE COLLIDER
    private void OnTriggerEnter(Collider other)
    {
        //CHECKS IF THE GAMEOBJECT IS HAS A DRINK TAG
        if (other.CompareTag("Coffee"))
        {
            //foreach (Ingredient orderIngredient in currentCustomer.ingredientsRequired)
            //{
            //    for (int i = 0; i < drink.GetComponent<Coffee>().ingredients.Length; i++)//count
            //    {
            //        Debug.Log("comparing " + orderIngredient.tag + " and " + drink.GetComponent<Coffee>().ingredients[i].tag);
            //        if (orderIngredient.tag == drink.GetComponent<Coffee>().ingredients[i].tag)
            //        {
            //            Debug.Log(orderIngredient.tag + " matches " + drink.GetComponent<Coffee>().ingredients[i].tag);//go to next orderIngredient
            //        }
            //        else
            //        {
            //            Debug.Log(orderIngredient.tag + " does not match " + drink.GetComponent<Coffee>().ingredients[i].tag);//go to next orderIngredient
            //        }
            //    }
            //}
            Debug.Log("start check");
            bool[] visited = new bool[drink.GetComponent<Coffee>().ingredients.Length];
            if (currentCustomer.ingredientsRequired.Length == drink.GetComponent<Coffee>().ingredients.Length)
            {
                for (int i = 0; i < currentCustomer.ingredientsRequired.Length; i++)
                {
                    for (int j = 0; j < drink.GetComponent<Coffee>().ingredients.Length; j++)//
                    {
                        //ContainsIngredient(currentCustomer.ingredientsRequired[i]);
                        //Debug.Log("comparing " + currentCustomer.ingredientsRequired[i].tag + " and " + drink.GetComponent<Coffee>().ingredients[j].tag);

                        if (drink.GetComponent<Coffee>().ingredients[j] == null)
                        {
                            //drink.GetComponent<Coffee>().ingredients[j] = new Ingredient();
                            Debug.Log("missing ingredient");
                            break;
                        }
                        else if (currentCustomer.ingredientsRequired[i].tag == drink.GetComponent<Coffee>().ingredients[j].tag && visited[j] == false)
                        {
                            visited[j] = true;
                            Debug.Log(currentCustomer.ingredientsRequired[i].tag + " matches " + drink.GetComponent<Coffee>().ingredients[j].tag);
                            break;
                        }
                        else
                        {
                            Debug.Log(currentCustomer.ingredientsRequired[i].tag + " does not match " + drink.GetComponent<Coffee>().ingredients[j].tag);
                        }
                    }
                }
            }

            foreach (bool checkedIngredient in visited)
            {
                if (checkedIngredient == true)
                {
                    correctCoffee = true;
                    Debug.Log("coffee is correct");
                }
                else
                {
                    correctCoffee = false;
                    Debug.Log("coffee is incorrect");
                    break;
                }
            }
            

            //IF GAMEOBJECT IS A DRINK

            // begin fadeing stuff
            isCoffeeFadingAway = true;
            dissolveShader = drink.GetComponent<Renderer>();
            dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveCup");
            coffeeDissolveProg = 0;
            // end fading stuff
            
            Destroy(drinkRigidBody);
            Destroy(drink, secondsToDestroy);
            Debug.Log("Destroyed");
            isCoffeeMade = false;
            StartCoroutine(coroutine);
        }

        //DELETES THE GAME OBJECT SINCE ORDER IS SERVED
        //STILL TODO
        //Destroy(confetti.gameObject, confetti.main.duration);
        //Destroy(gameObject);
    }

    private IEnumerator UpdateText(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log(correctCoffee);
            if (correctCoffee == true)
            {//switch statements
                if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[0])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[0];//human1 happy
                    ordersComplete++;
                    TipJar.text = "" + ordersComplete;
                    //PLAY PARTICLE EFFECT
                    //confetti.Play();
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[1])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[2];//human2 happy
                    ordersComplete++;
                    TipJar.text = "" + ordersComplete;
                    //PLAY PARTICLE EFFECT
                    //confetti.Play();
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[2])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[4];
                    ordersComplete++;
                    TipJar.text = "" + ordersComplete;
                    //PLAY PARTICLE EFFECT
                    //confetti.Play();
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[3])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[6];
                    ordersComplete++;
                    TipJar.text = "" + ordersComplete;
                    //PLAY PARTICLE EFFECT
                    //confetti.Play();
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[4])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[8];
                    ordersComplete++;
                    TipJar.text = "" + ordersComplete;
                    //PLAY PARTICLE EFFECT
                    //confetti.Play();
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[5])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[10];
                    ordersComplete++;
                    TipJar.text = "" + ordersComplete;
                    //PLAY PARTICLE EFFECT
                    //confetti.Play();
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
            }
            else //switch statements
            {
                if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[0])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[1];
                    ordersFailed++;
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[1])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[3];
                    ordersFailed++;
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[2])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[5];
                    ordersFailed++;
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[3])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[7];
                    ordersFailed++;
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[4])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[9];
                    ordersFailed++;
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
                else if (currentCustomer.GetComponent<Renderer>().sharedMaterial == currentCustomer.customerSprite[5])
                {
                    currentCustomer.GetComponent<Renderer>().material = currentCustomer.customerReactionSprite[11];
                    ordersFailed++;
                    yield return new WaitForSeconds(customerLeaving);
                    CustomerFinished();
                }
            }
            currentCustomer.docket.text = "";
            
            StopCoroutine(coroutine);
            
            Debug.Log("This worked?");
        }
    }

    void CustomerFinished()
    {
        currentCustomer.GetComponent<MeshRenderer>().enabled = false;
        
    }
    //public bool ContainsIngredient(Ingredient ingredient)
    //{
    //    for (int i = 0; i < drink.GetComponent<Coffee>().ingredients.Count; i++) // change list to array?
    //    {
    //        if (drink.GetComponent<Coffee>().ingredients[i].ingredientName == ingredient.ingredientName)
    //        {
    //            Debug.Log("matches?");
    //            return true;
    //        }
    //    }
    //    Debug.Log("does not match");
    //    return false;
    //}
}