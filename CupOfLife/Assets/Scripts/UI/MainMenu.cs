﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "";
    public Text roundsText;
    public SceneFader sceneFader;
    private GlobalControl globalControl;
    private AudioSource mainMenuAudio;

    public Text randomTextMessage;
    public string[] randomMessages;

    [Header("UI's")]
    public GameObject mainMenuUI;
    public GameObject creditsUI;
    public GameObject optionsUI;

    void OnEnable()
    {
        globalControl = FindObjectOfType<GlobalControl>();
        StartCoroutine(AnimateText());
        mainMenuAudio = GetComponent<AudioSource>();
        mainMenuAudio.Play();

        int rnd = Random.Range(0, 14);

        randomTextMessage.text = randomMessages[rnd].ToString();
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);

        while (round < globalControl.day)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(.05f);
        }
    }

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
        mainMenuAudio.Stop();
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