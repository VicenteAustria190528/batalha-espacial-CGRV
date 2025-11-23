using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referência")]
    public Transform player;

    [Header("Configuração de spawn")]
    public GameObject enemyPrefab;

    public float spawnInterval = 1.5f;
    public float spawnDistance = 60f;

    public float[] lanePositionsX = new float[] { -6f, 0f, 6f };

    private int enemiesToSpawn;
    private int currentLaneIndex = 0;

    private float timer = 0f;
    private float enemySpeed = 5f;

    private void Start()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty", 1);

        switch (difficulty)
        {
            case 0: // Fácil
            
                spawnInterval = 1f;
                enemySpeed = 5f;
                enemiesToSpawn = 40;
                break;

            case 1: // Médio
                spawnInterval = 1.5f;
                enemySpeed = 6f;
                enemiesToSpawn = 40;
                break;

            case 2: // Difícil
                spawnInterval = 0.9f;
                enemySpeed = 10f;
                enemiesToSpawn = 50;
                break;
        }

        Debug.Log("Dificuldade atual: " + difficulty);
        Debug.Log($"SpawnInterval: {spawnInterval}, EnemySpeed: {enemySpeed}, Quantidade: {enemiesToSpawn}");

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

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
            enemiesToSpawn--;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || player == null || lanePositionsX.Length == 0)
        {
            Debug.LogWarning("EnemySpawner configurado incorretamente.");
            return;
        }

        float baseY = player.position.y;

        float laneX = lanePositionsX[currentLaneIndex];

        Vector3 pos = new Vector3(
            laneX,
            baseY,
            player.position.z + spawnDistance
        );

        Quaternion rot = enemyPrefab.transform.rotation;
        GameObject enemy = Instantiate(enemyPrefab, pos, rot);

        // APLICAR velocidade no EnemyController
        EnemyController controller = enemy.GetComponent<EnemyController>();
        if (controller != null)
        {
            controller.speed = enemySpeed;   
        }

        currentLaneIndex++;
        if (currentLaneIndex >= lanePositionsX.Length)
            currentLaneIndex = 0;
    }
}
