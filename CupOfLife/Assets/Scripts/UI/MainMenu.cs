using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "";

    public SceneFader sceneFader;

    [Header("UI's")]
    public GameObject mainMenuUI;
    public GameObject creditsUI;
    public GameObject optionsUI;

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Credits()
    {
        mainMenuUI.SetActive(!mainMenuUI.activeSelf);
        creditsUI.SetActive(!creditsUI.activeSelf);
    }

    public void CreditsBack()
    {
        mainMenuUI.SetActive(!mainMenuUI.activeSelf);
        creditsUI.SetActive(!creditsUI.activeSelf);
    }

    public void Options()
    {
        mainMenuUI.SetActive(!mainMenuUI.activeSelf);
        optionsUI.SetActive(!optionsUI.activeSelf);
    }

    public void OptionsBack()
    {
        mainMenuUI.SetActive(!mainMenuUI.activeSelf);
        optionsUI.SetActive(!optionsUI.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}