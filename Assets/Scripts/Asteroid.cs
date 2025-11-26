using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Efeito de explosão")]
    public GameObject explosionPrefab;

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
