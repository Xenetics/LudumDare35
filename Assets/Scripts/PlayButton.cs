using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour
{

    public void Play()
    {
        GameManager.Instance.SetState(GameManager.GameStates.PreGame);
    }
}
