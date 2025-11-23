using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Velocidade (controlada pelo EnemySpawner)")]
    public float speed = 10f;

    [Header("Movimento lateral")]
    public float lateralAmplitude = 4f;
    public float lateralSpeed = 2f;

    [Header("Movimento vertical")]
    public float verticalAmplitude = 2f;
    public float verticalSpeed = 1.5f;

    private float initialY;
    private float initialX;

    // Movimentos diferentes por inimigo
    private float phaseOffsetX;
    private float phaseOffsetY;

    // dificuldade carregada
    private int difficulty;

    private void Start()
    {
        initialY = transform.position.y;
        initialX = transform.position.x;

        // pega a dificuldade
        difficulty = PlayerPrefs.GetInt("Difficulty", 1);

        // offsets aleatórios
        phaseOffsetX = Random.Range(0f, 10f);
        phaseOffsetY = Random.Range(0f, 10f);

        // DIFÍCIL = aumenta movimento
        if (difficulty == 2)
        {
            lateralAmplitude *= 1.4f;
            lateralSpeed *= 1.3f;
            verticalAmplitude *= 1.4f;
            verticalSpeed *= 1.3f;
        }

        // FÁCIL = completamente parado
        if (difficulty == 0)
        {
            speed = 0f;              // não anda pra frente
            lateralAmplitude = 0f;   // não mexe pros lados
            verticalAmplitude = 0f;  // não mexe verticalmente
        }
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        // Movimento pra frente (fica 0 no fácil)
        pos += Vector3.back * speed * Time.deltaTime;

        // Somente se NÃO for fácil
        if (difficulty != 0)
        {
            pos.x = initialX + Mathf.Sin((Time.time * lateralSpeed) + phaseOffsetX) * lateralAmplitude;
            pos.y = initialY + Mathf.Sin((Time.time * verticalSpeed) + phaseOffsetY) * verticalAmplitude;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.DerrotaPorColisao();
        }
    }
}
