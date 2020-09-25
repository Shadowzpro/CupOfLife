using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingBench : MonoBehaviour
{
    //REFERENCE TO PARTICLE SYSTEM THAT MAY BE ATTACHED
    public ParticleSystem confetti;

    //FUNCTION RUNS WHEN SOMETHING TRIGGERS THE COLLIDER
    private void OnTriggerEnter(Collider other)
    {
        //CHECKS IF THE GAMEOBJECT IS HAS A DRINK TAG
        if (other.CompareTag("Drink"))
        {
            //IF GAMEOBJECT IS A DRINK
            //PLAY PARTICLE EFFECT
            confetti.Play();
        }

        //DELETES THE GAME OBJECT SINCE ORDER IS SERVED
        //STILL TODO
        //Destroy(confetti.gameObject, confetti.main.duration);
        //Destroy(gameObject);
    }
}
