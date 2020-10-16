using UnityEngine;

[System.Serializable]
public class Coffee : MonoBehaviour
{
    /// <summary>
    /// The Ingredient
    /// </summary>
    public Ingredient ingredient;

    /// <summary>
    /// The Amount of that ingredient to make the coffee <br></br>
    /// Range from 1 - 10
    /// </summary>
    [Range(1, 10)]
    public int amount;
}