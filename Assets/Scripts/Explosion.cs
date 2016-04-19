using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public float explosionTime = 0.5f;
    public float destroyTimer = 0.5f;
    public ParticleSystem particles;
    public Color colorOfParticle;
    public PointEffector2D force;

    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
        particles.startColor = colorOfParticle;
    }

	void Update ()
    {
        explosionTime -= Time.deltaTime;
        destroyTimer -= Time.deltaTime;
        if (explosionTime < 0)
        {
            force.forceMagnitude = 0;
            force.forceVariation = 0;
        }
        
        if (destroyTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
