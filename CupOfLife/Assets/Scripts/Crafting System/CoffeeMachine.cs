using UnityEngine;
using System.Collections.Generic;

public class CoffeeMachine : MonoBehaviour, IIngredientContainer
{
    [Header("Coffee Machine")]
    /// <summary>
    /// The Coffee Machine's current list of the ingredients it has
    /// </summary>
    public Ingredient[] ingredientSlots;

    [Range(1, 15)]
    public int maxCoffeeMachineSlots;

    [Header("References")]
	public List<Recipe> CraftingRecipes;
    private Recipe cr; // Reference to CraftingRecipe

    [Header("Ingredient Prefabs")]
    public Ingredient coffeeBeans;
    public Ingredient milk;
    public Ingredient sugar;
    public Ingredient oil;
    public Ingredient cogs;
    public Ingredient greenJuice;
    public Ingredient eyeBalls;

    [Header("Coffee Prefabs")]
    public GameObject ResultCoffee1;
    public GameObject ResultCoffee2;
    public GameObject ResultCoffee3;
    public GameObject ResultCoffee4;

    /// <summary>
    /// Where the coffee will come out of
    /// </summary>
    public GameObject spawnPoint;

    // Check to see whether or not an ingredient has been dropped into the coffee machine or not
    private void OnTriggerEnter(Collider ingredient)
    {
        if (ingredient.CompareTag("CoffeeBeans"))
        {
            Debug.Log("Detected Coffee Beans");
            AddIngredient(coffeeBeans);
        }

        else if (ingredient.CompareTag("Cogs"))
        {
            Debug.Log("Detected Cogs");
            AddIngredient(cogs);
        }

        else if (ingredient.CompareTag("EyeBalls"))
        {
            Debug.Log("Detected Eye Balls");
            AddIngredient(eyeBalls);
        }

        else if(ingredient.CompareTag("GreenJuice"))
        {
            Debug.Log("Detected Green Juice");
            AddIngredient(greenJuice);
        }

        else if (ingredient.CompareTag("Milk"))
        {
            Debug.Log("Detected Milk");
            AddIngredient(milk);
        }

        else if (ingredient.CompareTag("Oil"))
        {
            Debug.Log("Detected Oil");
            AddIngredient(oil);
        }

        else if (ingredient.CompareTag("Sugar"))
        {
            Debug.Log("Detected Sugar");
            AddIngredient(sugar);
        }

        Destroy(ingredient.gameObject, 1.0f); // Destroy the GameObject after 1 second
    }

    private void Update()
    {

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
                Debug.Log("Added" + ingredient);
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
            if (ingredientSlots[i].ingredientName == ingredient.ingredientName)
            {
                ingredientSlots[i].ingredientName = null;
                return true;
            }
        }
        return false;
    }
}