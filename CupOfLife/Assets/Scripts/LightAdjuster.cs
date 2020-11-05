using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjuster : MonoBehaviour
{
    public GameManager gameManager; // Reference the Game Manager
    public float afternoon = 720;
    public float evening = 960;

    [Header("Light Reference")]
    public Light myLight;

    [Header("Range Variables")]
    public bool changeRange = false;
    public float rangeSpeed = 1.0f;
    public float maxRange = 10.0f;
    public bool repeatRange = false;

    [Header("Intensity Variables")]
    public bool changeIntensity = false;
    public float intensitySpeed = 1.0f;
    public float maxIntensity = 10.0f;
    public bool repeatIntensity = false;

    [Header("Colour Variables")]
    public bool changeColours = false;
    public float colourSpeed = 1.0f;
    public Color startColour;
    public Color midColour;
    public Color endColour;

    void Start()
    {
        myLight = GetComponent<Light>(); // Get the Light GameObject
        myLight.color = startColour; // Set the light's starting colour
    }

    void Update()
    {
        //if (changeRange)
        //{
        //    if (repeatRange)
        //    {
        //        myLight.range = Mathf.PingPong(Time.time * rangeSpeed, maxRange); // Pingpong goes up and back down repeatedly
        //    } else
        //    {
        //        myLight.range = Time.time * rangeSpeed; 
        //        if (myLight.range >= maxRange) changeRange = false; // If light's max range is reached, stop
        //    }
        //}

        //if (changeIntensity)
        //{
        //    if (repeatIntensity)
        //    {
        //        myLight.intensity = Mathf.PingPong(Time.time * intensitySpeed, maxIntensity); // Pingpong goes up and back down repeatedly
        //    }
        //    else
        //    {
        //        myLight.intensity = Time.time * intensitySpeed;
        //        if (myLight.intensity >= maxIntensity) changeIntensity = false; // If light's max intensity is reached, stop
        //    }
        //}
        
        // Afternoon 12:00pm
        if (gameManager.gameTime >= afternoon && gameManager.gameTime <= evening)
        {
            if (changeColours)
            {
                    float t = (gameManager.gameTime - afternoon) * colourSpeed;
                    myLight.color = Color.Lerp(startColour, midColour, t);
            }
        }
        
        // Evening 4:00pm
        if (gameManager.gameTime >= evening)
        {
            if (changeColours)
            {
                    float t = (gameManager.gameTime - evening) * colourSpeed;
                    myLight.color = Color.Lerp(midColour, endColour, t);
            }
        }
    }
}