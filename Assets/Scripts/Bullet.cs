using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 100;
    private float hSpeed;
    private float vSpeed;
    public Vector3 initVelocity;
    public Vector3 direction;
    public GameManager.Shape shapeToKill;
    public GameObject explosion;
	
	void Start ()
    {
        hSpeed = direction.x * speed;
        vSpeed = direction.y * speed;

        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(initVelocity.x + hSpeed, initVelocity.y + vSpeed));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") && col.gameObject.GetComponent<Enemy>().currentShape == shapeToKill)
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "enemyDeath");
            Spawner.Instance.enemyList.Remove(col.gameObject);
            GameObject splode = Instantiate(explosion, col.transform.position, Quaternion.identity) as GameObject;
            splode.GetComponent<Explosion>().colorOfParticle = col.gameObject.GetComponent<Enemy>().renderer.color;
            GameManager.Instance.score += (int)(col.gameObject.GetComponent<Enemy>().currentShape) + 1;
            Destroy(col.gameObject);
        }

        if (col.gameObject.layer != LayerMask.NameToLayer("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
