using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Tooltip("Tempo inicial da música em segundos")]
    public float startTime = 5f;   // quantos segundos pular no começo

    private AudioSource audioSource;

    private void Awake()
    {
        BackgroundMusic[] musics = FindObjectsOfType<BackgroundMusic>();
        if (musics.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && audioSource.clip != null)
        {
            startTime = Mathf.Clamp(startTime, 0f, audioSource.clip.length - 0.01f);

            audioSource.time = startTime;  // pula para X segundos
            audioSource.Play();            // começa tocando daí
        }
    }
}
