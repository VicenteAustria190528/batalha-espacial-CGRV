using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuração do projétil")]
    public float speed = 80f;      
    public float lifeTime = 3f;   

    private Vector3 direction;      
    private bool alreadyHit = false; 

    [Header("Áudio (fallback)")]
    public AudioClip enemyExplosionClip;          
    [Range(0f, 1f)] public float enemyExplosionVolume = 1f;

    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) return;

        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Enemy"))
        {
            alreadyHit = true;

            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.Morrer();
            }
            else
            {
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

            Destroy(gameObject);
        }
    }
}
