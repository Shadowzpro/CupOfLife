using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFinished : MonoBehaviour
{
    public string menuSceneName = "";

    public SceneFader sceneFader;

    public void Continue()
    {
        
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}