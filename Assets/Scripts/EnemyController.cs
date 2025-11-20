using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // move na direção "para trás" ao longo do corredor
        transform.position += -transform.forward * moveSpeed * Time.deltaTime;
    }
}
