using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Rigidbody ingredient;
    public Transform spawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody ingredientInstance = Instantiate(ingredient, spawn.position, spawn.rotation) as Rigidbody;
            ingredient.transform.parent = null;
        }
    }
}
