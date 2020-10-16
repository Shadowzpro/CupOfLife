public interface IIngredientContainer
{
    // All are automaticlly public
    bool AddIngredient(Ingredient ingredient);
    bool ContainsIngredient(Ingredient ingredient);
    int IngredientCount(Ingredient ingredient);
    bool IsFull();
    bool RemoveIngredient(Ingredient ingredient);
}