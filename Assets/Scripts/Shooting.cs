using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 5f; // tiros por segundo

    private float nextFireTime = 0f;

    void Update()
    {
        bool fireInput = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);

        if (fireInput && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    void Shoot()
    {
        if (firePoint == null || projectilePrefab == null)
        {
            Debug.LogWarning("FirePoint ou projectilePrefab não configurados no Shooting.");
            return;
        }

        // Direção = direção da CÂMERA
        Vector3 dir = Camera.main.transform.forward;

        // Instancia o projétil na frente da nave, sem parent
        GameObject projGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Passa a direção pro script Projectile
        Projectile proj = projGO.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Init(dir);
        }
    }
}
