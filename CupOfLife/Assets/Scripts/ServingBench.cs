using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServingBench : MonoBehaviour
{
    //REFERENCE TO PARTICLE SYSTEM THAT MAY BE ATTACHED
    public ParticleSystem confetti;
    public GameObject drink;
    public float secondsToDestroy = 4;
    private Rigidbody drinkRigidBody;
    public Text TipJar;
    public static int ordersComplete = 0;
    private IEnumerator coroutine;
    public bool isCoffeeMade = false;

    private void Update()
    {
        if (isCoffeeMade)
        {
            AssignCoffee();
        }
    }

    private void AssignCoffee()
    {
        drink = GameObject.FindWithTag("Coffee");
        drinkRigidBody = drink.GetComponent<Rigidbody>();
        //coroutine = UpdateText(secondsToDestroy);
    }

    //FUNCTION RUNS WHEN SOMETHING TRIGGERS THE COLLIDER
    private void OnTriggerEnter(Collider other)
    {
        //CHECKS IF THE GAMEOBJECT IS HAS A DRINK TAG
        if (other.CompareTag("Coffee"))
        {
            //IF GAMEOBJECT IS A DRINK
            Destroy(drinkRigidBody);
            Destroy(drink, secondsToDestroy);
            Debug.Log("Destroyed");
            isCoffeeMade = false;
            //StartCoroutine(coroutine);
        }

        //DELETES THE GAME OBJECT SINCE ORDER IS SERVED
        //STILL TODO
        //Destroy(confetti.gameObject, confetti.main.duration);
        //Destroy(gameObject);
    }

    private IEnumerator UpdateText(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            ordersComplete++;
            TipJar.text = "" + ordersComplete;
            //PLAY PARTICLE EFFECT
            confetti.Play();
            StopCoroutine(coroutine);
            Debug.Log("This worked?");
        }
    }
}