using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Referência")]
    public Transform player;          // arraste o Player aqui no Inspector

    [Header("Configuração de spawn")]
    public GameObject enemyPrefab;
    public float spawnInterval = 2.0f;    // tempo entre spawns
    public float spawnDistance = 60f;     // distância à frente do player

    [Tooltip("Posições X das pistas (lanes) dentro do corredor")]
    public float[] lanePositionsX = new float[] { -6f, 0f, 6f };

    private int currentLaneIndex = 0;     // qual lane será usada no próximo spawn

    private void Start()
    {
        // Se não foi setado no Inspector, tenta achar o Player pela Tag
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || player == null || lanePositionsX.Length == 0)
        {
            Debug.LogWarning("EnemySpawner configurado incorretamente.");
            return;
        }

        float baseY = player.position.y; // mesma altura da nave

        // Posição X da lane atual
        float laneX = lanePositionsX[currentLaneIndex];

        // Calcula posição do inimigo
        Vector3 pos = new Vector3(
            laneX,
            baseY,
            player.position.z + spawnDistance
        );

        // Usa a rotação do prefab (já deitado)
        Quaternion rot = enemyPrefab.transform.rotation;
        Instantiate(enemyPrefab, pos, rot);

        // Avança para a próxima lane (alternando)
        currentLaneIndex++;
        if (currentLaneIndex >= lanePositionsX.Length)
            currentLaneIndex = 0;
    }
}
