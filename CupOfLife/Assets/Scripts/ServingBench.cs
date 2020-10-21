using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServingBench : MonoBehaviour
{
    //REFERENCE TO PARTICLE SYSTEM THAT MAY BE ATTACHED
    //public ParticleSystem confetti;
    public GameObject drink;
    float secondsToDestroy = 4;
    private Rigidbody drinkRigidBody;
    public Text TipJar;
    public static int ordersComplete = 0;
    private IEnumerator coroutine;

    void Start()
    {
        drink = GameObject.FindWithTag("Drink");
        drinkRigidBody = drink.GetComponent<Rigidbody>();
        coroutine = UpdateText(secondsToDestroy);
    }

    //void Awake()
    //{
    //    
    //}

    //FUNCTION RUNS WHEN SOMETHING TRIGGERS THE COLLIDER
    private void OnTriggerEnter(Collider other)
    {
        //CHECKS IF THE GAMEOBJECT IS HAS A DRINK TAG
        if (other.CompareTag("Drink"))
        {
            //IF GAMEOBJECT IS A DRINK
            Destroy(drinkRigidBody);
            Destroy(drink, secondsToDestroy);
            StartCoroutine(coroutine);
            
            
            //PLAY PARTICLE EFFECT
            //confetti.Play();
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
            StopCoroutine(coroutine);
            Debug.Log("This worked?");
        }
    }
}
