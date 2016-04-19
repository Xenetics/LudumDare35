using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public GameManager.Shape currentShape = GameManager.Shape.Circle;
    public SpriteRenderer renderer;
    public int speed = 100;
    public int spinSpeed = 100;
    private float hSpeed;
    private float vSpeed;
    private System.Random rnd;
    public GameObject target;

	void Start ()
    {
        target = GameObject.Find("Player");
        rnd = new System.Random((int)System.DateTime.Now.Ticks);
        currentShape = (GameManager.Shape)rnd.Next(0, Spawner.Instance.variations);
        renderer.sprite = GameManager.Instance.sprites[(int)currentShape];
        renderer.color = GameManager.Instance.colors[(int)currentShape];
	}

	void Update ()
    {
        Seek();
        Rotate();
	}

    private void Seek()
    {
        Vector3 targetPos = target.transform.position - transform.position;
        hSpeed = Vector3.Normalize(targetPos).x * speed * Time.deltaTime;
        vSpeed = Vector3.Normalize(targetPos).y * speed * Time.deltaTime;

        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(hSpeed, vSpeed));
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }

    public void Hit()
    { 
}
}
