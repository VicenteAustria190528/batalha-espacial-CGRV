using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuração do projétil")]
    public float speed = 80f;      // bem mais rápido que a nave
    public float lifeTime = 3f;    // tempo até sumir sozinho

    private Vector3 direction;       // direção fixa do tiro
    private bool alreadyHit = false; // evita contar mais de 1 hit

    [Header("Áudio (fallback)")]
    public AudioClip enemyExplosionClip;              // som genérico de explosão
    [Range(0f, 1f)] public float enemyExplosionVolume = 1f;

    // Chamado logo depois de instanciar (pelo script de tiro)
    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        // Destroi o projétil depois de um tempo se não acertar nada
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Anda sempre na mesma direção
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // já bateu em algo antes? então ignora
        if (alreadyHit) return;

        // Se bater no jogador, ignora (quem cuida disso é outro script)
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Enemy"))
        {
            alreadyHit = true;

            // tenta usar o EnemyController primeiro
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.Morrer(); // ele toca o próprio som e se destrói
            }
            else
            {
                // fallback: usa som genérico do projétil, se configurado
                if (enemyExplosionClip != null)
                {
                    AudioSource.PlayClipAtPoint(
                        enemyExplosionClip,
                        other.transform.position,
                        enemyExplosionVolume
                    );
                }

                Destroy(other.gameObject);
            }

            // Conta kill no GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegistrarInimigoDestruido();
            }

            // destrói o projétil
            Destroy(gameObject);
        }
        else
        {
            // Qualquer outra coisa (chão, parede, etc.) destrói só o projétil
            Destroy(gameObject);
        }
    }
}
