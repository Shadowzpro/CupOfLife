using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject settingsUI;

    public string menuSceneName = "";

    public SceneFader sceneFader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Settings()
    {
        ui.SetActive(!ui.activeSelf);
        settingsUI.SetActive(!settingsUI.activeSelf);
    }

    public void SettingsBack()
    {
        ui.SetActive(!ui.activeSelf);
        settingsUI.SetActive(!settingsUI.activeSelf);
    }

    public void Restart()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}