using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Configuração")]
    public float spawnInterval = 3f;   // tempo entre spawns
    public float spawnRadius = 5f;     // espalhamento lateral
    public float minZOffset = 20f;     // distância mínima à frente
    public float maxZOffset = 80f;     // distância máxima à frente

    void Start()
    {
        // Chama SpawnEnemy a cada X segundos, para sempre
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("EnemySpawner: enemyPrefab não configurado!");
            return;
        }

        Vector3 offset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),   // X
            Random.Range(1f, 2.5f),                    // Y
            Random.Range(minZOffset, maxZOffset)       // Z
        );

        Vector3 pos = transform.position + offset;
        Quaternion rot = Quaternion.LookRotation(Vector3.back); // olhando pra você

        Instantiate(enemyPrefab, pos, rot);
    }
}
