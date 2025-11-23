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
    public float minX = -10f;
    public float maxX = 10f;

    [Header("Limite vertical")]
    public float minY = -2f;
    public float maxY = 5f;

    [Header("Rotação (pitch)")]
    public float pitchSpeed = 45f;
    public float maxPitchAngle = 30f;

    [Header("Visual da nave (opcional)")]
    public Transform shipModel;
    public float maxRollAngle = 25f;
    public float rollSpeed = 5f;

    private float currentPitch = 0f;

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

        // -------- MOVIMENTO --------
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 position = transform.position;

        // forward
        position += transform.forward * currentSpeed * Time.deltaTime;

        // lateral
        position += Vector3.right * horizontal * lateralSpeed * Time.deltaTime;

        // vertical
        position += Vector3.up * vertical * lateralSpeed * Time.deltaTime;

        // limites
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);   // 🔥 AQUI LIMITA A ALTURA

        transform.position = position;

        // -------- ROTAÇÃO (pitch) --------
        float pitchDelta = -vertical * pitchSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch + pitchDelta, -maxPitchAngle, maxPitchAngle);

        Vector3 euler = transform.rotation.eulerAngles;
        euler.x = currentPitch;
        transform.rotation = Quaternion.Euler(euler);

        // -------- ROLL VISUAL --------
        AtualizarRollVisual(horizontal);
    }

    private void AtualizarRollVisual(float horizontal)
    {
        if (shipModel == null)
            return;

        float targetRoll = -horizontal * maxRollAngle;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, targetRoll);

        shipModel.localRotation = Quaternion.Slerp(
            shipModel.localRotation,
            targetRot,
            Time.deltaTime * rollSpeed
        );
    }
}
