using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Velocidade para frente")]
    public float minSpeed = 20f;
    public float maxSpeed = 50f;
    public float acceleration = 5f;
    public float currentSpeed = 25f;

    [Header("Movimento lateral")]
    public float lateralSpeed = 15f;
    public float minX = -10f;    // limite esquerdo dentro do corredor
    public float maxX = 10f;     // limite direito dentro do corredor

    [Header("Visual da nave (opcional)")]
    public Transform shipModel;      // arraste seu modelo de nave aqui
    public float maxRollAngle = 25f; // inclinação visual ao mover
    public float rollSpeed = 5f;

    private void Update()
    {
        // Entrada vertical (W/S ou setas) apenas para acelerar/desacelerar
        float accelInput = Input.GetAxis("Vertical"); // pode deixar 0 se não quiser isso

        currentSpeed += accelInput * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        // Entrada horizontal (A/D ou setas) para andar para os lados
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 position = transform.position;

        // Movimento para frente contínuo
        position += transform.forward * currentSpeed * Time.deltaTime;

        // Movimento lateral no eixo X (mundo)
        position += Vector3.right * horizontal * lateralSpeed * Time.deltaTime;

        // Limita o player dentro do corredor
        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;

        // Mantém a nave sempre apontando para frente (sem girar com o input)
        // Se quiser rotação real, a gente adiciona depois; por enquanto é rail shooter.

        AtualizarRollVisual(horizontal);
    }

    private void AtualizarRollVisual(float horizontal)
    {
        if (shipModel == null)
            return;

        // Inclina a nave para o lado oposto ao movimento
        float targetRoll = -horizontal * maxRollAngle;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetRoll);

        shipModel.localRotation = Quaternion.Slerp(
            shipModel.localRotation,
            targetRot,
            Time.deltaTime * rollSpeed
        );
    }
}
