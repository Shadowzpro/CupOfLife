using UnityEngine;
using System.Collections.Generic;

public class CoffeeMachine : MonoBehaviour, IIngredientContainer
{
    [Header("Coffee Machine")]
    /// <summary>
    /// The Coffee Machine's current list of the ingredients it has
    /// </summary>
    public Ingredient[] ingredientSlots;

    /// <summary>
    /// Where the coffee will come out of
    /// </summary>
    public GameObject coffeeSpawnPoint;

    [Range(1, 15)]
    public int maxCoffeeMachineSlots;

    [Header("Ingredient Prefabs")]
    public Ingredient coffeeBeans;
    public Ingredient milk;
    public Ingredient sugar;
    public Ingredient oil;
    public Ingredient cogs;
    public Ingredient greenJuice;
    public Ingredient eyeBalls;

    // Check to see whether or not an ingredient has been dropped into the coffee machine or not
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CoffeeBeans"))
        {
            Debug.Log("Detected Coffee Beans");
            AddIngredient(coffeeBeans);
            Destroy(collider.gameObject, 0.1f);
            Instantiate(coffeeBeans, coffeeBeans.spawnPoint.transform.position, Quaternion.identity);
        }

        else if (collider.CompareTag("Cogs"))
        {
            Debug.Log("Detected Cogs");
            AddIngredient(cogs);
            Destroy(collider.gameObject, 0.1f);
            Instantiate(cogs, cogs.spawnPoint.transform.position, Quaternion.identity);
        }

        else if (collider.CompareTag("Eyeballs"))
        {
            Debug.Log("Detected Eyeballs");
            AddIngredient(eyeBalls);
            Destroy(collider.gameObject, 0.1f);
            Instantiate(eyeBalls, eyeBalls.spawnPoint.transform.position, Quaternion.identity);
        }

        else if(collider.CompareTag("GreenJuice"))
        {
            Debug.Log("Detected Green Juice");
            AddIngredient(greenJuice);
            Destroy(collider.gameObject, 0.1f);
            Instantiate(greenJuice, greenJuice.spawnPoint.transform.position, Quaternion.identity);
        }

        else if (collider.CompareTag("Milk"))
        {
            Debug.Log("Detected Milk");
            AddIngredient(milk);
            Destroy(collider.gameObject, 0.1f);
            Instantiate(milk, milk.spawnPoint.transform.position, Quaternion.identity);
        }

        else if (collider.CompareTag("Oil"))
        {
            Debug.Log("Detected Oil");
            AddIngredient(oil);
            Destroy(collider.gameObject, 0.1f);
            Instantiate(oil, oil.spawnPoint.transform.position, Quaternion.identity);
        }

        else if (collider.CompareTag("Sugar"))
        {
            Debug.Log("Detected Sugar");
            AddIngredient(sugar);
            Destroy(collider.gameObject, 0.1f);
            Instantiate(sugar, sugar.spawnPoint.transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Add an ingredient to the Coffee Machine
    /// </summary>
    /// <param name="ingredient"></param>
    public bool AddIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < maxCoffeeMachineSlots; i++)
        {
            if (ingredientSlots[i] == null)
            {
                Debug.Log("Added " + ingredient);
                ingredientSlots[i] = ingredient;
                return true;
            }
        }
        Debug.Log("No Ingredient Added");
        return false;
    }

    /// <summary>
    /// Does the Coffee Machine contain the ingredient?
    /// </summary>
    /// <param name="ingredient"></param>
    public bool ContainsIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].ingredientName == ingredient.ingredientName)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// How many ingredients are there?
    /// </summary>
    /// <param name="ingredient"></param>
    public int IngredientCount(Ingredient ingredient)
    {
        int number = 0;
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].ingredientName == ingredient.ingredientName)
            {
                number++;
            }
        }
        return number;
    }

    /// <summary>
    /// Is the Coffee Machine full?
    /// </summary>
    public bool IsFull()
    {
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i].ingredientName == null)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Remove an ingredient from the Coffee Machine
    /// </summary>
    /// <param name="ingredient"></param>
    public bool RemoveIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (ingredientSlots[i] == ingredient)
            {
                ingredientSlots[i] = null;
                return true;
            }
        }
        return false;
    }
}