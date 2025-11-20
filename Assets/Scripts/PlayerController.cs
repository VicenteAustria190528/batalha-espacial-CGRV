using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Velocidade")]
    public float minSpeed = 10f;
    public float maxSpeed = 40f;
    public float acceleration = 10f;
    public float currentSpeed = 15f;

    [Header("Rotação")]
    public float rotationSpeed = 60f;

    [Header("Limites de Altura")]
    public float minY = 1.2f;   // Altura mínima (acima do piso)
    public float maxY = 3f;     // Altura máxima

    [Header("Limites de Pitch (ângulo X)")]
    public float minPitch = -5f;   // quanto pode olhar para baixo
    public float maxPitch = 25f;   // quanto pode olhar para cima

    void Start()
    {
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    void Update()
    {
        HandleSpeedControl();
        HandleRotation();
        MoveForward();
        ClampHeight();
    }

    void HandleSpeedControl()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            currentSpeed += acceleration * Time.deltaTime;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            currentSpeed -= acceleration * Time.deltaTime;

        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    void HandleRotation()
    {
        float yawInput = Input.GetAxis("Horizontal"); // esquerda/direita
        float pitchInput = Input.GetAxis("Vertical"); // cima/baixo

        // Pega rotação atual em Euler
        Vector3 euler = transform.eulerAngles;

        // Converte o pitch para -180..180
        float pitch = euler.x;
        if (pitch > 180f) pitch -= 360f;

        // Aplica entradas
        pitch -= pitchInput * rotationSpeed * Time.deltaTime;   // invertido (cima/baixo)
        float yaw = euler.y + yawInput * rotationSpeed * Time.deltaTime;

        // Limita o pitch
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Aplica rotação final (sempre z = 0 pra não "entortar" a nave)
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void MoveForward()
    {
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    void ClampHeight()
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
