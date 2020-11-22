using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public int day;
    [HideInInspector]
    public int dayReached = 1;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            dayReached = PlayerPrefs.GetInt("dayReached", dayReached);
            Debug.Log("Day: " + day);
            Debug.Log("dayReached: " + dayReached);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseDay()
    {
        day++;
        PlayerPrefs.SetInt("dayReached", dayReached++);
    }

    public void ResetDays()
    {
        day = 1;
        PlayerPrefs.SetInt("dayReached", 1);
    }
}