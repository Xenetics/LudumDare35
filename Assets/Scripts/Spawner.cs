using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private static Spawner instance = null;
    public static Spawner Instance { get { return instance; } }

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

    [System.Serializable]
    public struct wave
    {
        public float waveTime;
        public int variations;
        public float spawnRate;
        public float changerSpawnRate;
        public int maxChangers;
        public int difficulty;
    }

    public bool spawn = false;
    public MeshRenderer mesh;
    [Header("Enemy values")]
    public int variations = 1;
    public float spawnRate = 1.0f;
    private float spawnTimer;
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();
    [Header("ShapeChanger values")]
    public float shapeChangeSpawnRate = 1.0f;
    private float changerSpawnTimer;
    public int maxChangers = 4;
    public GameObject changerPrefab;
    public List<GameObject> changerList = new List<GameObject>();
    [Header("General values")]
    public int difficulty = 1;
    public float waveTime;
    public float defaultWaveTime = 31;
    public List<wave> waves = new List<wave>();

	void Start ()
    {
        spawnTimer = spawnRate;
        changerSpawnTimer = shapeChangeSpawnRate;
	}
	
	void Update ()
    {
        SpawnShapeChangers();
        SpawnEnemies();
	}

    public void SetWave()
    {
        if (GameManager.Instance.wave < waves.Count)
        {
            waveTime = waves[GameManager.Instance.wave].waveTime;
            variations = waves[GameManager.Instance.wave].variations;
            spawnRate = waves[GameManager.Instance.wave].spawnRate;
            shapeChangeSpawnRate = waves[GameManager.Instance.wave].changerSpawnRate;
            maxChangers = waves[GameManager.Instance.wave].maxChangers;
            difficulty = waves[GameManager.Instance.wave].difficulty;
        }
        else
        {
            waveTime = defaultWaveTime;
            difficulty++;
        }
    }

    public void SpawnShapeChangers()
    {
        if (spawn)
        {
            changerSpawnTimer -= Time.deltaTime;
            if (changerSpawnTimer <= 0)
            {
                changerSpawnTimer = shapeChangeSpawnRate;
                if(changerList.Count < maxChangers)
                { 
                    float minX = gameObject.transform.position.x - mesh.bounds.size.x * 0.5f;
                    float minY = gameObject.transform.position.y - gameObject.transform.localScale.y * mesh.bounds.size.y * 0.5f;
                    Vector3 newVec = new Vector3(Random.Range(minX, -minX), Random.Range(minY, -minY), gameObject.transform.position.z - 2);
                    GameObject newChanger = Instantiate(changerPrefab) as GameObject;
                    newChanger.transform.position = newVec;
                    changerList.Add(newChanger);
                }
            }
        }
    }
    
    public void SpawnEnemies()
    {
        if (spawn)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                spawnTimer = spawnRate;
                for (int i = 0; i < difficulty; ++i)
                {
                    float minX = gameObject.transform.position.x - mesh.bounds.size.x * 0.5f;
                    float minY = gameObject.transform.position.y - gameObject.transform.localScale.y * mesh.bounds.size.y * 0.5f;
                    Vector3 newVec = new Vector3(Random.Range(minX, -minX), Random.Range(minY, -minY), gameObject.transform.position.z - 2);
                    if(Vector3.Distance(newVec, GameManager.Instance.player.transform.position) < 10)
                    {
                        return;
                    }
                    GameObject newEnemy = Instantiate(enemyPrefab) as GameObject;
                    newEnemy.transform.position = newVec;
                    enemyList.Add(newEnemy);
                }
            }
        }
    }

    public void Reset()
    {
        changerSpawnTimer = shapeChangeSpawnRate;
        spawnTimer = spawnRate;
    }
}
