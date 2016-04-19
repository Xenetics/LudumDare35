using UnityEngine;
using System.Collections;

public class ShapeChanger : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameManager.Shape shape;
    private System.Random rnd;
    public float timeToChange = 1;
    private float changeTimer;
    public float shrinkSpeed = 2;
    public Vector3 flashScale = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 originScale;

    void Start ()
    {
        originScale = transform.localScale;
        changeTimer = timeToChange;
        rnd = new System.Random((int)System.DateTime.Now.Ticks);
        shape = (GameManager.Shape)rnd.Next(0, Spawner.Instance.variations);
        sprite.sprite = GameManager.Instance.sprites[(int)shape];
        sprite.color = GameManager.Instance.colors[(int)shape];
    }

    void Update()
    {
        changeTimer -= Time.deltaTime;
        if(changeTimer <= 0)
        {
            changeTimer = timeToChange;
            shape = (GameManager.Shape)rnd.Next(0, Spawner.Instance.variations);
            sprite.sprite = GameManager.Instance.sprites[(int)shape];
            sprite.color = GameManager.Instance.colors[(int)shape];
            transform.localScale = flashScale;
        }

        if(transform.localScale.x > originScale.x)
        {
            Vector3 currentScale = Vector3.Lerp(transform.localScale, originScale, shrinkSpeed * Time.deltaTime);
            transform.localScale = currentScale;
        }
    }
}
