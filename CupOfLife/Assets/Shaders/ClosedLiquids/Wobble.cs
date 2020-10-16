using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    Renderer rend; //pulls in the renderer.
    Vector3 lastPos; //used against currrent transform to determin velocity.
    Vector3 velocity; //current displacement over deltatime. used in ADDING wobble.
    Vector3 lastRot;   //similar to last pos, but last rotation
    Vector3 angularVelocity; //similar to velocity: Angle change over deltatime.
    public float MaxWobble = 0.003f; //Furtherst angle in wobble. Increase to make more watery, decrease to make more jelly.
    public float WobbleSpeed = 1f; //Viscosity adjustment. Increase to move faster
    public float Recovery = 3f; //Viscosity adjustment. Increase to recover faster.
    float wobbleAmountX; //how far off the x we are wobbled.
    float wobbleAmountZ; //how far off the y we are wobbled.
    float wobbleAmountToAddX; //used to lerp it.
    float wobbleAmountToAddZ; //used to lerp it.
    float pulse; // sorta like the metranome time that slows. Weird maths ahead!
    float time = 100.5f; ///Used to record delta time.

    /// <summary>
    /// This entire script is used to maintain momentum in liquids when inside glass.
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>(); //pulls the renderer in.
        //Pretty self explanatory.
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //update time.
        DecreaseWobble(); //Decrease the goal angles over time
        SinWobble(); //move towards the goal angles and sin wave to purposely over and undershoot.
        //Now send to shader:
        rend.material.SetFloat("_WobbleX", wobbleAmountX);
        rend.material.SetFloat("_WobbleZ", wobbleAmountZ);

        //Begin velocity stuff to actually move the liquid side to side.
        //Calculate velocity in terms of displacement over time.
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;


        // LIMIT THE SPEED OF WOBBLE. FOR THE LOVE OF GOD, DONT FORGET THIS.
        //Clamps between max and min wobbles. Stops the liquid going nuts. 
        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        //Finally, save this as the last position and rotation for next pass.
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;


    }

    void DecreaseWobble()
    {
        //Lerps the current addition amount of x and y respectively, to 0 over time.
        //Ie: as time approaches infinity, wobble to add approaches 0.
        //recovery can be increased to make this resolve faster,
        //and have the liquid settle faster.
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (Recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * (Recovery));
    }

    void SinWobble()
    {
        //Makes a sin wave of the decreasing wobble. 
        //This is where the wobble actually IS, compared to where it's trying to get to.
        pulse = 2 * Mathf.PI * WobbleSpeed; //determines the rate at of movement
        //Pulse is hard to explain, but imagine its the slowing metronome pace.
        //I'd love to visualise this sin wave as a graph sometime. But it was hard to do, and hard to work out.
        //Anyways, once we have pulse:
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);
        //This makes it part of the pace, and uses the sin wave to work it all out.
        //Hard maths to explain.

    }

}
