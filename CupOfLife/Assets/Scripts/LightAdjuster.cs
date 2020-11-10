using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjuster : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager; // Reference the Game Manager
    [HideInInspector]
    public float afternoon = 720;   // 12:00pm
    [HideInInspector]
    public float evening = 960;     // 4:00pm

    [Header("Light Reference")]
    public Light myLight;

    [Header("Range Variables")]
    public bool changeRange = false;
    public float rangeSpeed = 1.0f;
    public float morningRange;
    public float afternoonRange;
    public float eveningRange;

    [Header("Intensity Variables")]
    public bool changeIntensity = false;
    public float intensitySpeed = 1.0f;
    public float morningIntensity;
    public float afternoonIntensity;
    public float eveningIntensity;

    [Header("Colour Variables")]
    public bool changeColours = false;
    public float colourSpeed = 1.0f;
    public Color startColour;
    public Color midColour;
    public Color endColour;

    void Start()
    {
        myLight = GetComponent<Light>();      // Get the Light GameObject
        myLight.color = startColour;          // Set the light's starting colour
        morningIntensity = myLight.intensity; // Set the morningIntensity to the Intensity that's already set
        morningRange = myLight.range;         // Set the morningRange to the range that's already set
    }

    void Update()
    {        
        // Afternoon 12:00pm
        if (gameManager.gameTime >= afternoon && gameManager.gameTime <= evening)
        {
            if (changeColours)
            {
                float t = (gameManager.gameTime - afternoon) * colourSpeed;
                myLight.color = Color.Lerp(startColour, midColour, t);
            }
            if (changeIntensity)
            {
                float t = (gameManager.gameTime - afternoon) * intensitySpeed;
                myLight.intensity = Mathf.Lerp(morningIntensity, afternoonIntensity, t);
            }
            if (changeRange)
            {
                float t = (gameManager.gameTime - afternoon) * rangeSpeed;
                myLight.range = Mathf.Lerp(morningRange, afternoonRange, t);
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
            if (changeIntensity)
            {
                float t = (gameManager.gameTime - evening) * intensitySpeed;
                myLight.intensity = Mathf.Lerp(afternoonIntensity, eveningIntensity, t);
            }
            if (changeRange)
            {
                float t = (gameManager.gameTime - evening) * rangeSpeed;
                myLight.range = Mathf.Lerp(afternoonRange, eveningRange, t);
            }
        }
    }
}