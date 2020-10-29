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
    public bool isCoffeeFadingAway = false;
    public Renderer dissolveShader;
    public float coffeeDissolveProg;

    private void Update()
    {
        if (isCoffeeFadingAway)
        {
            if (!drink) 
            { isCoffeeFadingAway = false; }
            CoffeeFadeOut();
        }
        if (isCoffeeMade)
        {
            AssignCoffee();
        }
    }

    private void CoffeeFadeOut() 
    {
        //Update fader
        
        dissolveShader.material.SetColor("edgeColor", Color.red);
        
        coffeeDissolveProg = coffeeDissolveProg + (0.5f * Time.deltaTime);
        dissolveShader.material.SetFloat("dissolveProgress", coffeeDissolveProg);
        if (coffeeDissolveProg >= 1) 
        {
            coffeeDissolveProg = 1;
            dissolveShader.material.SetFloat("dissolveProgress", coffeeDissolveProg);
            isCoffeeFadingAway = false;
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
            
            // begin fadeing stuff
            isCoffeeFadingAway = true;
            dissolveShader = drink.GetComponent<Renderer>();
            dissolveShader.material.shader = Shader.Find("Shader Graphs/DissolveMetal");
            coffeeDissolveProg = 0;
            // end fading stuff

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