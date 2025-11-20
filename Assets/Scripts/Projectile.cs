using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 80f;      // bem mais rápido que a nave
    public float lifeTime = 3f;

    private Vector3 direction;     // direção fixa do tiro

    // Chamado logo depois de instanciar
    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // anda sempre na mesma direção
        transform.position += direction * speed * Time.deltaTime;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
