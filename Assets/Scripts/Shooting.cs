using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 5f; // tiros por segundo

    private float nextFireTime = 0f;

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        // Mouse esquerdo ou tecla Space
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

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
