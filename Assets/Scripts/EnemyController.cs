using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 10f;   // velocidade para "vir na sua direção"

    private float fixedY;       // altura fixa do inimigo

    private void Start()
    {
        // Guarda a altura em que o inimigo nasceu
        fixedY = transform.position.y;
    }

    private void Update()
    {
        // Posição atual
        Vector3 pos = transform.position;

        // Move no eixo Z em direção negativa (vem na sua direção)
        pos += Vector3.back * speed * Time.deltaTime;

        // TRAVA a altura no valor fixo
        pos.y = fixedY;

        transform.position = pos;
    }
}
