using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject titlePanel;

    public GameObject creditsPanel;
    
    [Header("Main Menu")]
    public GameObject mainMenuPanel;
    public Button gameStartButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button exitButton;
    public Button mainMenuButton;
    
    [Header("Options")]
    public GameObject optionsPanel;
    
    [Header("Intro")]
    public GameObject introPanel;
    public GameObject intro1;
    public GameObject intro2;
    public GameObject intro3;
    public GameObject intro4;
    
    // TODO: Animation?
    public Animator introAnimator;
    
    [Header("Next Scene")]
    public string scene;

    private enum Panels { Title, MainMenu, Options, Intro, Credits, C1, C2, C3, C4 }

    private Panels _State;
    
    // Start is called before the first frame update
    void Start()
    {
        SwitchToPanel(Panels.Title);
        intro1.SetActive(false);
        intro2.SetActive(false);
        intro3.SetActive(false);
        intro4.SetActive(false);
        gameStartButton.onClick.AddListener(Intro);
        optionsButton.onClick.AddListener(Options);
        creditsButton.onClick.AddListener(Credits);
        exitButton.onClick.AddListener(Exit);
        mainMenuButton.onClick.AddListener(MainMenu);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (_State==Panels.Title)
            {
                SwitchToPanel(Panels.MainMenu);
            } else if (_State == Panels.Intro || _State == Panels.C1 || _State == Panels.C2 || _State == Panels.C3 || _State == Panels.C4)
            {
                nextIntro();
            }
        }
    }

    private void SwitchToPanel(Panels panel)
    {
        switch (panel)
        {
            case Panels.Title:
                _State = Panels.Title;
                titlePanel.SetActive(true);
                mainMenuPanel.SetActive(false);
                optionsPanel.SetActive(false);
                creditsPanel.SetActive(false);
                introPanel.SetActive(false);
                break;
            case Panels.MainMenu:
                _State = Panels.MainMenu;
                titlePanel.SetActive(false);
                mainMenuPanel.SetActive(true);
                optionsPanel.SetActive(false);
                creditsPanel.SetActive(false);
                introPanel.SetActive(false);
                break;
            case Panels.Options:
                _State = Panels.Options;
                titlePanel.SetActive(false);
                mainMenuPanel.SetActive(false);
                optionsPanel.SetActive(true);
                introPanel.SetActive(false);
                break;
            case Panels.Intro:
                _State = Panels.C1;
                titlePanel.SetActive(false);
                mainMenuPanel.SetActive(false);
                optionsPanel.SetActive(false);
                creditsPanel.SetActive(false);
                introPanel.SetActive(true);
                break;
            case Panels.Credits:
                _State = Panels.Credits;
                titlePanel.SetActive(false);
                mainMenuPanel.SetActive(false);
                optionsPanel.SetActive(false);
                creditsPanel.SetActive(true);
                introPanel.SetActive(false);
                break;
        }
    }

    private void nextIntro()
    {
        Debug.Log("next");
        if (_State == Panels.Intro)
        {
            titlePanel.SetActive(false);
            mainMenuPanel.SetActive(false);
            creditsPanel.SetActive(false);
            intro1.SetActive(true);
            _State = Panels.C1;
        } else if (_State == Panels.C1)
        {
            intro1.SetActive(false);
            intro2.SetActive(true);
            _State = Panels.C2;
        }
        else if (_State == Panels.C2)
        {
            intro2.SetActive(false);
            intro3.SetActive(true);
            _State = Panels.C3;
        }
        else if (_State == Panels.C3)
        {
            intro3.SetActive(false);
            intro4.SetActive(true);
            _State = Panels.C4;
        }
        else if (_State == Panels.C4)
        {
            StartCoroutine(LoadGameScene());
        }
    }
    
    private void Intro()
    {
        SwitchToPanel(Panels.Intro);
        intro1.SetActive(true);
    }

    public void MainMenu()
    {
        SwitchToPanel(Panels.MainMenu);
    }
    
    private void Options()
    { 
        SwitchToPanel(Panels.Options);
    }
    
    private void Credits()
    {
        SwitchToPanel(Panels.Credits);
    }
    
    public void Exit()
    { 
        Application.Quit();
    }
    
    // Load the game scene asynchronously
    private IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            // TODO: Load indicator?
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            yield return null;
        }
        Debug.Log("Loading complete");
        // yield return new WaitUntil(() => !introAnimator.GetCurrentAnimatorStateInfo(0).IsName("IntroAnimation"));

    }
}
