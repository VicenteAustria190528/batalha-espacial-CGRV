using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuração do projétil")]
    public float speed = 80f;      // bem mais rápido que a nave
    public float lifeTime = 3f;    // tempo até sumir sozinho

    private Vector3 direction;     // direção fixa do tiro
    private bool alreadyHit = false; // evita contar mais de 1 kill

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

        // Se bater no jogador, ignora
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Enemy"))
        {
            alreadyHit = true;  // garante que só conta uma vez

            // destrói o inimigo
            Destroy(other.gameObject);

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
