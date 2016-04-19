using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public GameManager.Shape currentShape = GameManager.Shape.Circle;
    private bool isAlive = true;
    public SpriteRenderer sprite;
    private bool attacking = false;
    public float speed = 150.0f;
    public float fireRate = 1.0f;
    public int novaShots = 30;
    public float novaShotTime = 30.0f;
    public float novaTimer;
    public bool novaFired = false;
    private float shotTimer;
    public int radiusForBulletSpawn = 5;
    private float hSpeed;
    private float vSpeed;
    public GameObject bulletPrefab;
    private System.Random rnd;
    public GameObject explosion;
    private bool sploded = false;

    void Start()
    {
        GameManager.Instance.player = GameObject.Find("Player").GetComponent<PlayerController>();
        shotTimer = fireRate;
        novaTimer = novaShotTime;
        rnd = new System.Random((int)System.DateTime.Now.Ticks);
        currentShape = (GameManager.Shape)rnd.Next(0, Spawner.Instance.variations);
        sprite.sprite = GameManager.Instance.sprites[(int)currentShape];
        sprite.color = GameManager.Instance.colors[(int)currentShape];
    }

    void Update ()
    {
        Controls();
	}

    private void Controls()
    {
        if (GameManager.Instance.currentState == GameManager.GameStates.Playing)
        {
            Vector3 shotDirection = transform.forward;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                shotDirection = Vector3.Normalize(hit.point - transform.position);
            }

            if (Input.GetButton("Fire"))
            {
                shotTimer -= Time.deltaTime;
                if (shotTimer <= 0)
                {
                    SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "shoot");
                    shotTimer = fireRate;
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + (shotDirection * radiusForBulletSpawn), transform.rotation) as GameObject;
                    bullet.GetComponent<Bullet>().initVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
                    bullet.GetComponent<Bullet>().direction = shotDirection;
                    bullet.GetComponent<Bullet>().shapeToKill = currentShape;

                }
            }

            if (novaFired)
            {
                novaTimer -= Time.deltaTime;
                if (novaTimer <= 0)
                {
                    novaTimer = novaShotTime;
                    novaFired = false;
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (!novaFired)
                {
                    SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "nova");
                    novaFired = true;
                    for (int i = 0; i < novaShots; ++i)
                    {
                        shotDirection = Vector3.Normalize(new Vector3(rnd.Next(-10, 11), rnd.Next(-10, 11), 0));
                        GameObject bullet = Instantiate(bulletPrefab, transform.position + (shotDirection * radiusForBulletSpawn), transform.rotation) as GameObject;
                        bullet.GetComponent<Bullet>().initVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
                        bullet.GetComponent<Bullet>().direction = shotDirection;
                        bullet.GetComponent<Bullet>().shapeToKill = currentShape;
                    }
                }
            }

            hSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            vSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(hSpeed, vSpeed);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("ShapeChanger"))
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "collect");
            currentShape = col.gameObject.GetComponent<ShapeChanger>().shape;
            sprite.sprite = GameManager.Instance.sprites[(int)currentShape];
            sprite.color = GameManager.Instance.colors[(int)currentShape];
            Spawner.Instance.changerList.Remove(col.gameObject);
            Destroy(col.gameObject);
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy") && col.gameObject.GetComponent<Enemy>().currentShape != currentShape)
        {
            if (!sploded)
            {
                sploded = true;
                GameManager.Instance.SetState(GameManager.GameStates.GameOver);
                GameObject splode = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
                splode.GetComponent<Explosion>().colorOfParticle = sprite.color;
                sprite.color = new Color(0, 0, 0, 0);
            }
        }
    }
}
