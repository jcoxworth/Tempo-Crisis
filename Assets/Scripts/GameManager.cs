using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _access;
    public enum GameScene { menu, play}
    public GameScene currentGameScene = GameScene.menu;
    public int currentLevel = 0;
    public bool hasStarted = false;
    public bool isPaused = true;
    public bool isDead = false;
    public float currentLevelTime = 0f;
    public string currentLevelTime_Formatted = "0";

    public Player player;
    public delegate void OnMenu();
    public OnMenu onMenu;
    public delegate void OnStartLevel();
    public OnStartLevel onStartLevel;

    public delegate void OnPauseLevel();
    public OnStartLevel onPauseLevel;
    public delegate void OnResumeLevel();
    public OnResumeLevel onResumeLevel;

    public delegate void OnFinishLevel();
    public OnFinishLevel onFinishLevel;

    public delegate void OnNextLevel();
    public OnNextLevel onNextLevel;

    public delegate void OnRestartLevel();
    public OnRestartLevel onRestartLevel;

    public delegate void OnDieInLevel();
    public OnDieInLevel onDieInLevel;

    public GameObject menu;
    public GameObject[] levels;

    // Start is called before the first frame update
    private void Awake()
    {
        _access = this;
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        LoadGameState();

    }
    void LoadGameState()
    {
        switch (currentGameScene)
        {
            case GameScene.menu:
                onMenu?.Invoke();
                isPaused = false;
                menu.SetActive(true);
                break;
            case GameScene.play:
                menu.SetActive(false);
                levels[currentLevel].SetActive(true);
                RestartLevel();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        ElapseTime();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!hasStarted)
                return;
            if (!isPaused)
                PauseLevel();
            else
                ResumeLevel();
        }
    }
    private void ElapseTime()
    {
        if (isPaused)
        {

        }
        else
        {
            currentLevelTime += Time.deltaTime;
            currentLevelTime_Formatted = currentLevelTime.ToString("00.00");
        }


    }
    public void RestartLevel()
    {
        Debug.Log("restart level");

        hasStarted = true;
        player.transform.position = Vector3.zero;
        currentLevelTime = 0f;
        isPaused = true;
        isDead = false;
        hasStarted = false;
        onRestartLevel?.Invoke();

    }
    public void NextLevel()
    {
        Debug.Log("next level");
        isPaused = true;
        isDead = false;
        hasStarted = false;
        currentLevelTime = 0f;
        player.transform.position = Vector3.zero;
        hasStarted = true;
        levels[currentLevel].SetActive(false);
        currentLevel++;
       // onNextLevel?.Invoke();

        if (currentLevel >= levels.Length)
        {
            //Loop The Game?
            currentLevel = 0;
            levels[currentLevel].SetActive(true);
        }
        else
        {
            //Next level
            levels[currentLevel].SetActive(true);
        }
        onRestartLevel?.Invoke();

    }

    public void PauseLevel()
    {
        Debug.Log("pause level");

        onPauseLevel?.Invoke();
        isPaused = true;
    }
    public void ResumeLevel()
    {
        Debug.Log("reseume level");

        onResumeLevel?.Invoke();
        isPaused = false;
    }
    public void StartGamePlay()
    {
        currentGameScene = GameScene.play;
        LoadGameState();
    }
    public void StartLevel()
    {
        Debug.Log("start level");

        hasStarted = true;
        onStartLevel?.Invoke();
        isPaused = false;
    }
    public void DieInLevel()
    {
        currentLevel = 0;
        isDead = true;
        onDieInLevel?.Invoke();
    }
    public void FinishLevel()
    {
        onFinishLevel?.Invoke();
        isPaused = true;
        
    }
    public void QuitLevel()
    {
        Application.Quit();
    }

    private int cycle8 = 0;
    public int Cycle8()
    {
        cycle8++;
        if (cycle8 > 7)
            cycle8 = 0;
        return cycle8;
    }
}
