using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // REFERENCE TO INGREDIENT'S RIGID BODY
    public Rigidbody ingredient;

    // WHERE THE INGREDIENT WILL DROP 
    public Transform spawn;

    // REFERENCE TO PLAYER GAMEOBJECT THAT WILL PUSH THE BUTTON
    public GameObject destination;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // INSTANTIATES NEW INGREDIENT
            Rigidbody ingredientInstance = Instantiate(ingredient, spawn.position, spawn.rotation) as Rigidbody;

            //DEPARENTS INGREDIENT FROM DISPENSER
            ingredient.transform.parent = null;
        }
    }
}
