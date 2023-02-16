using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIGameManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject startScreen, dieScreen, finishScreen, pauseScreen, quitScreen;
    public TMP_Text time;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        gameManager.onMenu += HideAllScreens;
        gameManager.onRestartLevel += ShowStartScreen;
        gameManager.onNextLevel += ShowStartScreen;
        gameManager.onPauseLevel += ShowPauseScreen;
        gameManager.onFinishLevel += ShowFinishScreen;
        gameManager.onDieInLevel += ShowDieScreen;
        gameManager.onStartLevel += HideAllScreens;

        gameManager.onResumeLevel += HideAllScreens;

       // startScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameManager.hasStarted)
                ShowQuitScreen();
        }
    }
    
    private void ShowStartScreen()
    {
        HideAllScreens();
        startScreen.SetActive(true);
    }
    private void ShowDieScreen()
    {
        HideAllScreens();
        dieScreen.SetActive(true);
    }
    private void ShowFinishScreen()
    {
        HideAllScreens();
        finishScreen.SetActive(true);
    }
    private void ShowPauseScreen()
    {
        HideAllScreens();
        pauseScreen.SetActive(true);
    }
    public void ShowQuitScreen()
    {
        quitScreen.SetActive(true);
    }
    private void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }
    public void HideQuitScreen()
    {
        quitScreen.SetActive(false);
    }
    private void HideAllScreens()
    {
        startScreen.SetActive(false);
        finishScreen.SetActive(false);
        dieScreen.SetActive(false);
        pauseScreen.SetActive(false);
        quitScreen.SetActive(false);
    }
    private void UpdateTime()
    {
        time.text = gameManager.currentLevelTime_Formatted.ToString();
    }
}
