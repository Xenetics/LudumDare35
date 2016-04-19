using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private static GameUI instance = null;
    public static GameUI Instance { get { return instance; } }

    void Awake()
    {
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
    public Text scoreText;
    public GameObject timerArea;
    public Text timerText;
    public Text waveTimer;
    public Text novaText;
    public GameObject gameOverArea;
}
