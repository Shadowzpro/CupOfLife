using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject settingsUI;
    [HideInInspector]
    public GameManager gameManager;

    public string menuSceneName = "";

    public SceneFader sceneFader;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (settingsUI.activeSelf) return;
        if (gameManager.gameIsOver == true) return;

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
            Time.timeScale = 0.000000001f;
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