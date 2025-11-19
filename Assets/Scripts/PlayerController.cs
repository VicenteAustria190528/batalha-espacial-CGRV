using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Velocidade")]
    public float minSpeed = 10f;
    public float maxSpeed = 40f;
    public float acceleration = 10f; // quanto a velocidade muda por segundo
    public float currentSpeed = 15f;

    [Header("Rotação")]
    public float rotationSpeed = 60f; // graus por segundo

    void Start()
    {
        // Garante que a velocidade inicial esteja dentro do intervalo
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    void Update()
    {
        HandleSpeedControl();
        HandleRotation();
        MoveForward();
    }

    void HandleSpeedControl()
    {
        // Aumentar velocidade (W ou seta pra cima)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        // Diminuir velocidade (S ou seta pra baixo)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            currentSpeed -= acceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    void HandleRotation()
    {
        // Horizontal: A/D ou setas
        float yawInput = Input.GetAxis("Horizontal");   // esquerda/direita
        float pitchInput = Input.GetAxis("Vertical");   // cima/baixo

        // Yaw: girar para esquerda/direita (eixo Y)
        transform.Rotate(0f, yawInput * rotationSpeed * Time.deltaTime, 0f);

        // Pitch: girar para cima/baixo (eixo X)
        transform.Rotate(-pitchInput * rotationSpeed * Time.deltaTime, 0f, 0f);
    }

    void MoveForward()
    {
        // Avança sempre na direção em que a nave está apontando
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }
}
