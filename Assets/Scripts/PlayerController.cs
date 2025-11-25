using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Velocidade para frente")]
    public float minSpeed = 20f;
    public float maxSpeed = 60f;
    public float acceleration = 15f;
    public float currentSpeed = 25f;

    [Header("Movimento lateral/vertical")]
    public float lateralSpeed = 15f;
    public float minX = -10f;
    public float maxX = 10f;

    [Header("Limite vertical (apenas mínimo)")]
    [Tooltip("Altura mínima da nave (chão está em Y = 0). Não deixar menor que 0.")]
    public float minY = 0.5f;   // nunca abaixo do Floor (0)

    [Header("Rotação visual (pitch/roll)")]
    public float pitchSpeed = 45f;
    public float maxPitchAngle = 30f;
    public Transform shipModel;
    public float maxRollAngle = 25f;
    public float rollSpeed = 5f;

    private float currentPitch = 0f;
    private float currentRoll = 0f;

    [Header("Áudio")]
    public AudioClip acelerarClip; 
    [Range(0f, 1f)] public float acelerarVolume = 1f;

    public AudioClip frearClip;    
    [Range(0f, 1f)] public float frearVolume = 1f;

    private AudioSource audioSource; 

    private void Start()
    {
        if (minY < 0f)
            minY = 0f;

        Vector3 pos = transform.position;
        if (pos.y < minY)
        {
            pos.y = minY;
            transform.position = pos;
        }

        // Áudio
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float accelInput = 0f;

        bool acelerando = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool freiando   = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        if (acelerando)
            accelInput = 1f;
        else if (freiando)
            accelInput = -1f;

        bool acelerandoDown = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
        bool freiandoDown   = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);

        if (freiandoDown && !acelerandoDown)
        {
            if (audioSource != null && frearClip != null)
                audioSource.PlayOneShot(frearClip, frearVolume);
        }
        else if (acelerandoDown && !freiandoDown)
        {
            if (audioSource != null && acelerarClip != null)
                audioSource.PlayOneShot(acelerarClip, acelerarVolume);
        }

        currentSpeed += accelInput * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical"); 

        // Shift também sobe, Ctrl também desce
        if (acelerando)
        {
            vertical = 1f;    // sobe
        }
        else if (freiando)
        {
            vertical = -1f;   // desce
        }

        Vector3 position = transform.position;

        position += Vector3.forward * currentSpeed * Time.deltaTime;

        position += Vector3.right * horizontal * lateralSpeed * Time.deltaTime;
        position += Vector3.up    * vertical   * lateralSpeed * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, minX, maxX);

        if (position.y < minY)
            position.y = minY;

        transform.position = position;

        float pitchDelta = -vertical * pitchSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch + pitchDelta, -maxPitchAngle, maxPitchAngle);

        float targetRoll = -horizontal * maxRollAngle;
        currentRoll = Mathf.Lerp(currentRoll, targetRoll, Time.deltaTime * rollSpeed);

        AtualizarModeloVisual();
    }

    private void AtualizarModeloVisual()
    {
        if (shipModel == null)
            return;

        shipModel.localRotation = Quaternion.Euler(currentPitch, 0f, currentRoll);
    }

}
