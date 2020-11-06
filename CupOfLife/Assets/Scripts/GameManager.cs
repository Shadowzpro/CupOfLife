using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //REFERENCE TO TIMER GAMEOBJECT
    public Text timerText;

    //GAME TIME STARTS AT 9AM
    public float gameTime = 9 * 60;
    public float timeMultiplier = 1.6f;
    
    
    public float GameTime
    {
        get
        {
            return gameTime;
        }
    }

    // Start is called before the first frame update

    //DISABLES TIMER AT VERY START FOR MENU PURPOSES
    //WILL NEED TO CHANGE THIS WITH GAMESTATES
    void Start()
    {
        timerText.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //SETS TIMER TO ENABLED ON NEXT FRAME
        timerText.gameObject.SetActive(true);
        
        //GAMETIME GETS INCREMENTED BY 1.6 MINUTES EVERY SECOND
        //THIS MAKES SURE "8 HOURS" PASSES IN 5 MINUTES
        gameTime += Time.deltaTime * timeMultiplier;
        int minutes = Mathf.RoundToInt(gameTime);

        //SETS CLOCK TO 1PM ONWARDS
        if (gameTime > (13 * 60) - 0.5)
        {
            timerText.text = string.Format(" {0:D1}:{1:D2}", ((minutes / 60) % 12), (minutes % 60)) + "pm";
        }
        //SETS CLOCK TO 12PM - 12:59PM
        else if (gameTime > (12 * 60) - 0.5)
        {
            timerText.text = string.Format("{0:D2}:{1:D2}", (minutes / 60), (minutes % 60)) + "pm";
        }
        else
        {
            //SETS CLOCK TO 10AM - 11:59AM
            if (gameTime < 10 * 60)
            {
                timerText.text = string.Format(" {0:D1}:{1:D2}", (minutes / 60), (minutes % 60)) + "am";
            }
            //SETS CLOCK TO 9AM - 9:59AM
            else
            {
                timerText.text = string.Format("{0:D2}:{1:D2}", (minutes / 60), (minutes % 60)) + "am";
            }
        }
        if (gameTime == 1020)
        {
            //time up
        }
    }
}
