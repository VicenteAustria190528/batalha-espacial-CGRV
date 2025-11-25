using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject asteroidPrefab;

    [Header("Configuração de spawn")]
    public float spawnInterval = 1.2f;
    public float spawnDistance = 60f;
    public int maxAsteroids = 50;
    public float[] lanePositionsX = { -6f, 0f, 6f };

    private float timer = 0f;
    private int spawned = 0;

    private void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        if (player == null)
            Debug.LogError("[AsteroidSpawner] Player não configurado ou sem tag Player.");
        if (asteroidPrefab == null)
            Debug.LogError("[AsteroidSpawner] Asteroid Prefab não configurado.");
    }

    private void Update()
    {
        if (player == null || asteroidPrefab == null) return;
        if (spawned >= maxAsteroids) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        if (lanePositionsX == null || lanePositionsX.Length == 0)
        {
            Debug.LogError("[AsteroidSpawner] LanePositionsX vazio.");
            return;
        }

        float laneX = lanePositionsX[Random.Range(0, lanePositionsX.Length)];

        Vector3 pos = new Vector3(
            laneX,
            player.position.y,
            player.position.z + spawnDistance
        );

        Instantiate(asteroidPrefab, pos, asteroidPrefab.transform.rotation);
        spawned++;
    }
}
