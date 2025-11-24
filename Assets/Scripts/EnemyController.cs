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

    // limite de altura (evita atravessar o Floor)
    public float minY = 0.5f;

    [Header("Áudio")]
    public AudioClip deathClip;                  // som da explosão/morte do inimigo
    [Range(0f, 1f)] public float deathVolume = 1f;

    private float initialY;
    private float initialX;

    private float phaseOffsetX;
    private float phaseOffsetY;

    private int difficulty;

    private void Start()
    {
        initialY = transform.position.y;
        initialX = transform.position.x;

        difficulty = PlayerPrefs.GetInt("Difficulty", 1);

        phaseOffsetX = Random.Range(0f, 10f);
        phaseOffsetY = Random.Range(0f, 10f);

        if (difficulty == 2)
        {
            lateralAmplitude *= 1.4f;
            lateralSpeed *= 1.3f;
            verticalAmplitude *= 1.4f;
            verticalSpeed *= 1.3f;
        }

        if (difficulty == 0)
        {
            speed = 0f;
            lateralAmplitude = 0f;
            verticalAmplitude = 0f;
        }
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        pos += Vector3.back * speed * Time.deltaTime;

        if (difficulty != 0)
        {
            pos.x = initialX + Mathf.Sin((Time.time * lateralSpeed) + phaseOffsetX) * lateralAmplitude;
            pos.y = initialY + Mathf.Sin((Time.time * verticalSpeed) + phaseOffsetY) * verticalAmplitude;
        }

        // impede o inimigo de ficar abaixo do chão
        pos.y = Mathf.Max(pos.y, minY);

        transform.position = pos;
    }

    // Chamado quando o inimigo é destruído pelo projétil
    public void Morrer()
    {
        if (deathClip != null)
        {
            AudioSource.PlayClipAtPoint(deathClip, transform.position, deathVolume);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.DerrotaPorColisao();
            // aqui você decide se quer ou não explodir o inimigo também
            // Morrer();
        }
    }
}
