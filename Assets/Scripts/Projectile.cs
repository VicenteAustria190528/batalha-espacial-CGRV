using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 60f;
    public float lifeTime = 3f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        // Destroi o projétil após alguns segundos
        Destroy(gameObject, lifeTime);
    }

    // Mais tarde vamos usar isso pra detectar inimigos
    private void OnTriggerEnter(Collider other)
    {
        // Exemplo futuro:
        // if (other.CompareTag("Enemy"))
        // {
        //     Destroy(other.gameObject);
        //     Destroy(gameObject);
        // }
    }
}
