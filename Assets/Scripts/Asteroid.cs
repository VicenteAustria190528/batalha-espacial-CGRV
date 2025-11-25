using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Efeito de explosão")]
    public GameObject explosionPrefab;

    [Header("Tiro que destrói asteroide")]
    [Tooltip("Só projéteis com essa Tag vão explodir o asteroide.")]
    public string destroyingProjectileTag = "AsteroidProjectile";

    private void OnTriggerEnter(Collider other)
    {
        // Colisão com o player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Asteroide colidiu com a nave!");

            Explodir();

            if (GameManager.Instance != null)
                GameManager.Instance.DerrotaPorColisao();
        }

        // Colisão com projétil que pode destruir asteroide
        if (other.CompareTag(destroyingProjectileTag))
        {
            Debug.Log("Tiro que destrói asteroide acertou!");

            Destroy(other.gameObject); // destrói o tiro
            Explodir();                // explode e destrói o asteroide
        }
    }

    private void Explodir()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
