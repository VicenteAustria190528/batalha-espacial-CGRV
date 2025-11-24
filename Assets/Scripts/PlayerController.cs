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

    private void Start()
    {
        // Garante que nunca vamos permitir Y menor que o chão (0)
        if (minY < 0f)
            minY = 0f;

        // Se o player começar abaixo do mínimo, já sobe para a altura mínima
        Vector3 pos = transform.position;
        if (pos.y < minY)
        {
            pos.y = minY;
            transform.position = pos;
        }
    }

    private void Update()
    {
        // -------- ACELERAÇÃO PARA FRENTE --------
        float accelInput = 0f;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            accelInput = 1f;
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            accelInput = -1f;

        currentSpeed += accelInput * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        // -------- INPUT DE MOVIMENTO --------
        float horizontal = Input.GetAxis("Horizontal");
        float vertical   = Input.GetAxis("Vertical");

        Vector3 position = transform.position;

        // ANDAR PARA FRENTE
        position += Vector3.forward * currentSpeed * Time.deltaTime;

        // MOVIMENTO LATERAL (X) E VERTICAL (Y)
        position += Vector3.right * horizontal * lateralSpeed * Time.deltaTime;
        position += Vector3.up    * vertical   * lateralSpeed * Time.deltaTime;

        // LIMITES HORIZONTAIS
        position.x = Mathf.Clamp(position.x, minX, maxX);

        // SOMENTE LIMITE INFERIOR
        if (position.y < minY)
            position.y = minY;

        transform.position = position;

        // -------- PITCH VISUAL --------
        float pitchDelta = -vertical * pitchSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch + pitchDelta, -maxPitchAngle, maxPitchAngle);

        // -------- ROLL VISUAL --------
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.Max(pos.y, minY);
            transform.position = pos;
        }
    }
}
