using UnityEngine;

public class SimpleAsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;   
    public float spawnInterval = 1f;    // Tempo entre spawns
    public int maxAsteroids = 100;      // Total
    public float distanceAhead = 60f;   // Distância na frente da nave
    public float minX = -6f;            // Limite esquerdo
    public float maxX = 6f;             // Limite direito

    private float timer = 0f;
    private int spawned = 0;

    void Update()
    {
        if (spawned >= maxAsteroids) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            float x = Random.Range(minX, maxX);

            Vector3 pos = new Vector3(
                x,
                transform.position.y,
                transform.position.z + distanceAhead
            );

            Instantiate(asteroidPrefab, pos, Quaternion.identity);
            spawned++;
        }
    }
}
