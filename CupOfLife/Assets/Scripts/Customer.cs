using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public float customerSpawnTime = 3.125f;
    public IEnumerator customer;
    private IEnumerator order;
    public GameObject speechBubble;
    public Text dialogue;
    public Text docket;
    private List<int> ingredientAmounts;
    public Ingredient[] orderIngredients; //= { "Bag(s) of Coffee Beans", "Bottle(s) of Milk", "Sugar Cube(s)", "Can(s) of Oil", "Cog(s)", "Vile(s) of Green Juice", "EyeBall(s)" }; // ALL INGREDIENTS
    public Ingredient[] ingredientsRequired;
    public int ingredientAmountMin = 0;
    public int ingredientAmountMax = 3;
    public Material[] customerSprite;//
    public Material[] customerReactionSprite;
    public CoffeeMachine coffeeMachine;//

    // Start is called before the first frame update
    void Start()
    {
        customer = FirstCustomer(customerSpawnTime);
        order = DialogueBox(0);
        StartCoroutine(customer);
        Ingredient[] ingredientsRequired = new Ingredient[6];//probably not needed
        //Texture[] customerSprite = new Texture[6];//
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dialogue.text);
    }
    private bool NoCustomers()
    {
        return GetComponent<MeshRenderer>().enabled == false;
    }

    //kinda borked
    private IEnumerator FirstCustomer(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (NoCustomers())
            {
                Debug.Log("New Customer Spawned");
                NewCustomer();
                //StopCoroutine(customer);
            }
            else //SHOULD REACH THIS once after customer is spawned
            {
                Debug.Log("There is currently a customer");
                customerSpawnTime = 6.25f;
                //StopCoroutine(customer);
            }
            //yield return false;
        }
    }

    void NewCustomer()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Renderer>().material = customerSprite[Random.Range(0, customerSprite.Length)];//
        StartCoroutine(order);
        // order here?
        // wait for drink?
        // drink is served?
        // react?
        // leaves (meshrenderer)
        // startcoroutine again?
    }

    private IEnumerator DialogueBox(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(1.25f);
            speechBubble.gameObject.SetActive(true);
            ingredientAmounts = new List<int> { Random.Range(ingredientAmountMin, ingredientAmountMax), Random.Range(ingredientAmountMin, ingredientAmountMax), Random.Range(ingredientAmountMin, ingredientAmountMax) };
            string ingredientOne;
            string ingredientTwo;
            string ingredientThree;

            if (ingredientAmounts[0] != 0)
            {
                if (ingredientAmounts[1] != 0 || ingredientAmounts[2] != 0)
                {
                    ingredientOne = ingredientAmounts[0] + " " + orderIngredients[0].tag + ", ";
                }
                else
                {
                    ingredientOne = ingredientAmounts[0] + " " + orderIngredients[0].tag;
                }
            }
            else
            {
                ingredientOne = null;
            }

            //human customer
            if (GetComponent<Renderer>().sharedMaterial == customerSprite[0] || GetComponent<Renderer>().sharedMaterial == customerSprite[1])
            {
                if (ingredientAmounts[1] != 0)
                {
                    if (ingredientAmounts[2] != 0)
                    {
                        ingredientTwo = ingredientAmounts[1] + " " + orderIngredients[1].tag + ", ";
                    }
                    else
                    {
                        ingredientTwo = ingredientAmounts[1] + " " + orderIngredients[1].tag;
                    }
                }
                else
                {
                    ingredientTwo = null;
                }
                if (ingredientAmounts[2] != 0)
                {
                    ingredientThree = ingredientAmounts[2] + " " + orderIngredients[2].tag;
                }
                else
                {
                    ingredientThree = null;
                }

                dialogue.text = "One cup with: " + ingredientOne + ingredientTwo + ingredientThree + " please.";
                
                ingredientsRequired = new Ingredient[ingredientAmounts[0] + ingredientAmounts[1] + ingredientAmounts[2]];//COMPARE THIS ARRAY
                coffeeMachine.ingredientSlots = new Ingredient[ingredientsRequired.Length];//
                
                int counter = 0;//cleaner
                
                for (int i = 0; i < ingredientAmounts[0]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[0];
                    counter++;
                }
                
                for (int i = 0; i < ingredientAmounts[1]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[1];
                    counter++;
                }
                
                for (int i = 0; i < ingredientAmounts[2]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[2];
                    counter++;
                }
                
                docket.text = ingredientAmounts[0] + " x " + orderIngredients[0].tag + '\n' + ingredientAmounts[1] + " x " + orderIngredients[1].tag + '\n' + ingredientAmounts[2] + " x " + orderIngredients[2].tag;
            }
            else if (GetComponent<Renderer>().sharedMaterial == customerSprite[2] || GetComponent<Renderer>().sharedMaterial == customerSprite[3])//robot
            {
                if (ingredientAmounts[1] != 0)
                {
                    if (ingredientAmounts[2] != 0)
                    {
                        ingredientTwo = ingredientAmounts[1] + " " + orderIngredients[3].tag + ", ";
                    }
                    else
                    {
                        ingredientTwo = ingredientAmounts[1] + " " + orderIngredients[3].tag;
                    }
                }
                else
                {
                    ingredientTwo = null;
                }
                if (ingredientAmounts[2] != 0)
                {
                    ingredientThree = ingredientAmounts[2] + " " + orderIngredients[4].tag;
                }
                else
                {
                    ingredientThree = null;
                }

                dialogue.text = "One cup with: " + ingredientOne + ingredientTwo + ingredientThree + " please.";

                ingredientsRequired = new Ingredient[ingredientAmounts[0] + ingredientAmounts[1] + ingredientAmounts[2]];//COMPARE THIS ARRAY
                coffeeMachine.ingredientSlots = new Ingredient[ingredientsRequired.Length];//

                int counter = 0;//cleaner

                for (int i = 0; i < ingredientAmounts[0]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[0];
                    counter++;
                }

                for (int i = 0; i < ingredientAmounts[1]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[3];
                    counter++;
                }

                for (int i = 0; i < ingredientAmounts[2]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[4];
                    counter++;
                }

                docket.text = ingredientAmounts[0] + " x " + orderIngredients[0].tag + '\n' + ingredientAmounts[1] + " x " + orderIngredients[3].tag + '\n' + ingredientAmounts[2] + " x " + orderIngredients[4].tag;
            }
            else if (GetComponent<Renderer>().sharedMaterial == customerSprite[4] || GetComponent<Renderer>().sharedMaterial == customerSprite[5])//alien
            {
                if (ingredientAmounts[1] != 0)
                {
                    if (ingredientAmounts[2] != 0)
                    {
                        ingredientTwo = ingredientAmounts[1] + " " + orderIngredients[5].tag + ", ";
                    }
                    else
                    {
                        ingredientTwo = ingredientAmounts[1] + " " + orderIngredients[5].tag;
                    }
                }
                else
                {
                    ingredientTwo = null;
                }
                if (ingredientAmounts[2] != 0)
                {
                    ingredientThree = ingredientAmounts[2] + " " + orderIngredients[6].tag;
                }
                else
                {
                    ingredientThree = null;
                }

                dialogue.text = "One cup with: " + ingredientOne + ingredientTwo + ingredientThree + " please.";

                ingredientsRequired = new Ingredient[ingredientAmounts[0] + ingredientAmounts[1] + ingredientAmounts[2]];//COMPARE THIS ARRAY
                coffeeMachine.ingredientSlots = new Ingredient[ingredientsRequired.Length];//

                int counter = 0;//cleaner

                for (int i = 0; i < ingredientAmounts[0]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[0];
                    counter++;
                }

                for (int i = 0; i < ingredientAmounts[1]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[5];
                    counter++;
                }

                for (int i = 0; i < ingredientAmounts[2]; i++)
                {
                    ingredientsRequired[counter] = orderIngredients[6];
                    counter++;
                }

                docket.text = ingredientAmounts[0] + " x " + orderIngredients[0].tag + '\n' + ingredientAmounts[1] + " x " + orderIngredients[5].tag + '\n' + ingredientAmounts[2] + " x " + orderIngredients[6].tag;
            }
            yield return new WaitForSeconds(6.25f);
            speechBubble.gameObject.SetActive(false);
            StopCoroutine(order);
        }
    }
}