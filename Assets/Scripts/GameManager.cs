using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public enum Shape { Circle, Diamond, Heagon, Octagon, Square, Star, Triangle, COUNT }
    public List<Sprite> sprites = new List<Sprite>();
    public List<Color> colors = new List<Color>();
    public PlayerController player;

    public enum GameStates { Intro, Menu, PreGame, Playing, WaveTransition, Paused, GameOver }
    public GameStates currentState = GameStates.Intro;

    public int wave = 0;
    public int score = 0;

    public int highScore = 0;

    public float timeTillPlay = 5.0f;
    private float pregameTimer;



    private float backToMenuTimer;
    public float gameOverTime = 5;

    void Start ()
    {
        pregameTimer = timeTillPlay;
        backToMenuTimer = gameOverTime;
        
	}
	
	void Update ()
    {
        HandleState();
	}

    public void SetState(GameStates newState)
    {
        ExitState();
        currentState = newState;
        EnterState();
    }

    private void HandleState()
    {
        switch(currentState)
        {
            case GameStates.Intro:
                break;
            case GameStates.Menu:
                break;
            case GameStates.PreGame:
                pregameTimer -= Time.deltaTime;
                GameUI.Instance.timerText.text = ((int)pregameTimer).ToString();
                if (pregameTimer <= 0)
                {
                    SetState(GameStates.Playing);
                    GameUI.Instance.timerArea.SetActive(false);
                }
                break;
            case GameStates.Playing:
                Spawner.Instance.waveTime -= Time.deltaTime;
                GameUI.Instance.waveTimer.text = ((int)Spawner.Instance.waveTime).ToString();
                if(Spawner.Instance.waveTime <= 0)
                {
                    wave++;
                    Spawner.Instance.SetWave();
                }
                GameUI.Instance.scoreText.text = score.ToString();

                if (player.novaFired)
                {
                    GameUI.Instance.novaText.text = ((int)player.novaTimer).ToString();
                }
                else
                {
                    GameUI.Instance.novaText.text = "Nova Ready!";
                }
                break;
            case GameStates.WaveTransition:
                break;
            case GameStates.Paused:
                break;
            case GameStates.GameOver:
                backToMenuTimer -= Time.deltaTime;
                if(backToMenuTimer <= 0)
                {
                    backToMenuTimer = gameOverTime;
                    SetState(GameStates.Menu);
                }
                break;
        }
    }

    private void EnterState()
    {
        switch (currentState)
        {
            case GameStates.Intro:
                break;
            case GameStates.Menu:
                SceneManager.LoadScene("Menu");
                break;
            case GameStates.PreGame:
                SceneManager.LoadScene("Game");
                Reset();
                break;
            case GameStates.Playing:
                Spawner.Instance.SetWave();
                Spawner.Instance.spawn = true;
                break;
            case GameStates.WaveTransition:
                break;
            case GameStates.Paused:
                break;
            case GameStates.GameOver:
                GameUI.Instance.gameOverArea.SetActive(true);
                Spawner.Instance.spawn = false;
                if(score > highScore)
                {
                    highScore = score;
                }
                break;
        }
    }

    private void ExitState()
    {
        switch (currentState)
        {
            case GameStates.Intro:
                break;
            case GameStates.Menu:
                break;
            case GameStates.PreGame:
                break;
            case GameStates.Playing:
                break;
            case GameStates.WaveTransition:
                break;
            case GameStates.Paused:
                break;
            case GameStates.GameOver:
                break;
        }
    }

    public void Reset()
    {
        wave = 0;
        score = 0;
        pregameTimer = timeTillPlay;
        backToMenuTimer = gameOverTime;
    }
}
