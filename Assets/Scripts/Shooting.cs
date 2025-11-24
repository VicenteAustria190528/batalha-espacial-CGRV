using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float fireRate = 5f; // tiros por segundo

    private float nextFireTime = 0f;

    // ------- ÁUDIO DO TIRO -------
    [Header("Áudio do tiro")]
    public AudioClip fireClip;                 // som da arma (arma_laser)
    [Range(0f, 1f)] public float fireVolume = 1f;

    private AudioSource audioSource;           // vai usar o mesmo AudioSource da nave
    // -----------------------------

    private void Awake()
    {
        // pega o AudioSource que já está na nave (o mesmo do motor / efeitos)
        audioSource = GetComponent<AudioSource>();
    }

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

        // ----- TOCAR SOM DO TIRO -----
        if (audioSource != null && fireClip != null)
        {
            audioSource.PlayOneShot(fireClip, fireVolume);
        }
    }
}
