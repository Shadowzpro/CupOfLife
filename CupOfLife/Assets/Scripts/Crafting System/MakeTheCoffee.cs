using UnityEngine;

public class MakeTheCoffee : MonoBehaviour
{
	[Header("References")]
    public CoffeeMachine coffeeMachine; // Reference to CoffeeMachine
    public ServingBench SB;

    [Header("Public Variables")]
	public CoffeeMachine IngredientContainer;
    public Coffee coffeePrefab; // The empty coffee cup

    // Push a button on the Coffee Machine with your arm
    private void OnTriggerEnter(Collider collider)
	{
        Debug.Log("Button Pushed");

        if (HowManyGameObjects("Coffee") >= 1) return;

        if (collider.CompareTag("Player"))
        {
            Debug.Log("Button Pushed by Arm");
            //Craft();
            CoffeeMaker();
            EmptyCoffeeMachine();
            CleanCoffeePrefab();
        }
    }

    /// <summary>
    /// Go through each ingredient amount in the ingredients list and checking if the IngredientContainer contains the amount for the coffee recipe <br></br><br></br>
    /// <b>Returns</b> whether or not you can craft the coffee
    /// </summary>
    public bool CanCraft()
    {
        //foreach (Coffee ingredientAmount in recipe.ingredients)
        //{
        //    // If 1 ingredient is not found, immediately return false because you can't craft the recipe
        //    if (coffeeMachine.IngredientCount(ingredientAmount.ingredients) < ingredientAmount.amount)
        //    {
        //        return false;
        //    }
        //}
        return true; // If you reach here, you have enough ingredients to craft the recipe
    }

    /// <summary>
    /// Check to see how many GameObjects are currently active within the scene with a specific tag
    /// </summary>
    /// <param name="tag">The Tag</param>
    /// <returns>The number of GameObjects active</returns>
    public int HowManyGameObjects(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        int howMany = objects.Length; // get the array length
        return howMany;
    }

    public void ServeOrder()
    {
        
    }

    public void CoffeeMaker()
    {
        for (int i = 0; i < coffeeMachine.ingredientSlots.Length; i++)
        {
            coffeePrefab.ingredients.Add(coffeeMachine.ingredientSlots[i]);
            Debug.Log("Ingredient Added to Coffee");
        }
        Instantiate(coffeePrefab, coffeeMachine.coffeeSpawnPoint.transform.position, Quaternion.identity);
        SB.isCoffeeMade = true;
        if (SB.isCoffeeMade == true)
        {
            Debug.Log("YES!!!");
        }
    }

    /// <summary>
    /// Empty the Coffee Machine
    /// </summary>
    public void EmptyCoffeeMachine()
    {
        for (int i = 0; i < coffeeMachine.ingredientSlots.Length; i++)
        {
            coffeeMachine.RemoveIngredient(coffeeMachine.ingredientSlots[i]);
            Debug.Log("Removed Ingredient from Coffee Machine");
        }
    }

    public void CleanCoffeePrefab()
    {
        for (int i = 0; i < 200; i++)
        {
            coffeePrefab.ingredients.Remove(coffeePrefab.ingredients[i]);
        }
    }

    /// <summary>
    /// Make the coffee
    /// </summary>
    public void Craft()
    {
        // Check if we can make the coffee
        if (CanCraft())
        {
            // Look through all the ingredients in the coffee machine list and remove all of them
            for (int i = 0; i < coffeeMachine.ingredientSlots.Length; i++)
            {
                //coffeeMachine.RemoveIngredient(coffee.ingredients[i]);
            }

            // Spawn the coffee
            //Instantiate(coffeeMachine.ResultCoffee1, coffeeMachine.spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            // Play sound effect or something
        }
    }
}