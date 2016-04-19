using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text scoreText;
	void Start ()
    {
        scoreText.text = GameManager.Instance.highScore.ToString();
	}
}
