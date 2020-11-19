using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public GameObject settingsUI;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsUI.activeSelf)
            {
                settingsUI.SetActive(!settingsUI.activeSelf);
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                Time.timeScale = 1f;
            }
        }
    }
}