using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Efeito de explosão")]
    public GameObject explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // Colisão com o player (derrota)
        if (other.CompareTag("Player"))
        {
            Debug.Log("Asteroide colidiu com a nave!");

            // Instancia explosão na posição do asteroide
            Explodir();

            GameManager.Instance.DerrotaPorColisao();
            Destroy(gameObject);
        }

        // Colisão com projétil (destrói tiro e asteroide)
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("Tiro acertou o asteroide!");

            Destroy(other.gameObject);   // destrói o tiro
            Explodir();
            Destroy(gameObject);         // destrói o asteroide
        }
    }

    private void Explodir()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
