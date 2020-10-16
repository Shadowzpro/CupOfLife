using UnityEngine;

public class MakeTheCoffee : MonoBehaviour
{
	[Header("References")]
    private readonly CoffeeMachine coffeeMachine; // Reference to CoffeeMachine
    private readonly Coffee coffee;

    [Header("Public Variables")]
	public CoffeeMachine IngredientContainer;
    public Recipe recipe; // Reference the Recipe

	// Push a button on the Coffee Machine with your arm
	private void OnTriggerEnter(Collider arm)
	{
		Craft();
	}

    /// <summary>
    /// Go through each ingredient amount in the ingredients list and checking if the IngredientContainer contains the amount for the coffee recipe <br></br><br></br>
    /// <b>Returns</b> whether or not you can craft the coffee
    /// </summary>
    /// <param name="ingredientContainer"></param>
    public bool CanCraft()
    {
        foreach (Coffee ingredientAmount in recipe.ingredients)
        {
            // If 1 ingredient is not found, immediately return false because you can't craft the recipe
            if (coffeeMachine.IngredientCount(ingredientAmount.ingredient) < ingredientAmount.amount)
            {
                return false;
            }
        }
        return true; // If you reach here, you have enough ingredients to craft the recipe
    }

    /// <summary>
    /// Make the coffee
    /// </summary>
    /// <param name="ingredientContainer"></param>
    public void Craft()
    {
        // Check if we can make the coffee
        if (CanCraft())
        {
            // Look through all the ingredients in the coffee machine list and remove all of them
            for (int i = 0; i < coffeeMachine.ingredientSlots.Length; i++)
            {
                coffeeMachine.RemoveIngredient(coffee.ingredient);
            }

            // Spawn the coffee
            Instantiate(coffeeMachine.ResultCoffee1, coffeeMachine.spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            // Play sound effect or something
        }
    }
}