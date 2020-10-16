using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    /// <summary>
    /// What are all the ingredients needed to make the coffee?
    /// </summary>
    public List<Coffee> ingredients;

    /// <summary>
    /// The coffee it'll make
    /// </summary>
    public GameObject result;
}