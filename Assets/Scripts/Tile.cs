using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color colorToBe;
    public float timeToChange = 1;
    private float changeTimer;
    private System.Random rnd;

    void Start ()
    {
        rnd = new System.Random((int)System.DateTime.Now.Ticks + (int)(transform.position.x + transform.position.y));
        changeTimer = timeToChange/* + (((float)rnd.Next(0, 100)) / 100)*/;
        int rando = rnd.Next(0, GameManager.Instance.colors.Count);
        sprite.color = GameManager.Instance.colors[rando];
    }
	
	void Update ()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0)
        {
            changeTimer = timeToChange/* + (((float)rnd.Next(0, 100)) / 100)*/;
            int rando = rnd.Next(0, GameManager.Instance.colors.Count);
            sprite.color = GameManager.Instance.colors[rando];
        }
    }
}
