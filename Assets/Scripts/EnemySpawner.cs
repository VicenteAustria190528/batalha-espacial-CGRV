using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referência")]
    public Transform player;

    [Header("Configuração de spawn")]
    public GameObject enemyPrefab;

    [Tooltip("Tempo mínimo entre spawns")]
    public float minSpawnInterval = 0.4f;

    [Tooltip("Tempo máximo entre spawns")]
    public float maxSpawnInterval = 1.2f;

    [Tooltip("Distância média na frente do player")]
    public float spawnDistance = 60f;

    [Tooltip("Variação aleatória na distância Z")]
    public float randomZOffset = 8f;

    [Tooltip("Posições base em X das 'lanes'")]
    public float[] lanePositionsX = new float[] { -6f, 0f, 6f };

    [Tooltip("Variação aleatória em X em torno da lane")]
    public float randomXOffset = 1.5f;

    [Header("Quantidade por dificuldade")]
    public int enemiesToSpawnEasy = 60;
    public int enemiesToSpawnMedium = 80;
    public int enemiesToSpawnHard = 110;

    private int enemiesToSpawn;
    private float timer = 0f;
    private float currentSpawnInterval;
    private float enemySpeed = 5f;

    private void Start()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty", 1);

        switch (difficulty)
        {
            case 0: // Fácil
                enemySpeed = 5f;
                enemiesToSpawn = enemiesToSpawnEasy;
                minSpawnInterval = 0.9f;
                maxSpawnInterval = 1.4f;
                break;

            case 1: // Médio
                enemySpeed = 7f;
                enemiesToSpawn = enemiesToSpawnMedium;
                minSpawnInterval = 0.6f;
                maxSpawnInterval = 1.1f;
                break;

            case 2: // Difícil
                enemySpeed = 10f;
                enemiesToSpawn = enemiesToSpawnHard;
                minSpawnInterval = 0.35f;
                maxSpawnInterval = 0.8f;
                break;
        }

        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

        Debug.Log("Dificuldade atual: " + difficulty);
        Debug.Log($"Intervalo: {minSpawnInterval} - {maxSpawnInterval}, Velocidade: {enemySpeed}, Quantidade: {enemiesToSpawn}");

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    private void Update()
    {
        if (enemiesToSpawn <= 0) return;
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
            enemiesToSpawn--;

            currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || lanePositionsX.Length == 0)
        {
            Debug.LogWarning("EnemySpawner configurado incorretamente.");
            return;
        }

        float baseY = player.position.y;

        int laneIndex = Random.Range(0, lanePositionsX.Length);
        float laneX = lanePositionsX[laneIndex];

        float finalX = laneX + Random.Range(-randomXOffset, randomXOffset);
        float finalZ = player.position.z + spawnDistance + Random.Range(-randomZOffset, randomZOffset);

        Vector3 pos = new Vector3(finalX, baseY, finalZ);

        Quaternion rot = enemyPrefab.transform.rotation;
        GameObject enemy = Instantiate(enemyPrefab, pos, rot);

        EnemyController controller = enemy.GetComponent<EnemyController>();
        if (controller != null)
        {
            controller.speed = enemySpeed;
        }
    }
}
