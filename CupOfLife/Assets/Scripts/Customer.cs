using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public float customerSpawnTime = 3.125f;
    private IEnumerator customer;
    private IEnumerator order;
    public GameObject speechBubble;
    public Text dialogue;
    public Text docket;
    private List<int> ingredientAmounts;
    private string[] ingredients = { "Bag(s) of Coffee Beans", "Bottle(s) of Milk", "Sugar Cube(s)", "Can(s) of Oil", "Cog(s)", "Vile(s) of Green Juice", "EyeBall(s)" };

    // Start is called before the first frame update
    void Start()
    {
        customer = FirstCustomer(customerSpawnTime);
        order = DialogueBox(0);
        StartCoroutine(customer);
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
                StopCoroutine(customer);
            }
            //yield return false;
        }
    }

    void NewCustomer()
    {
        GetComponent<MeshRenderer>().enabled = true;
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
            ingredientAmounts = new List<int> { Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3) };
            //human customer
            dialogue.text = "One cup with, " + ingredientAmounts[0] + " " + ingredients[0] + ", " + ingredientAmounts[1] + " " + ingredients[1] + ", " + ingredientAmounts[2] + " " + ingredients[2] + " please.";
            docket.text = ingredientAmounts[0] + " x " + ingredients[0] + '\n' + ingredientAmounts[1] + " x " + ingredients[1] + '\n' + ingredientAmounts[2] + " x " + ingredients[2];
            yield return new WaitForSeconds(6.25f);
            speechBubble.gameObject.SetActive(false);
            StopCoroutine(order);
        }
    }
}
