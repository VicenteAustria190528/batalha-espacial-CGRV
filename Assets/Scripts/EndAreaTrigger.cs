using UnityEngine;

public class EndAreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo entrou no EndArea: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER chegou ao fim!");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.ChecarVitoriaAoChegarNoFim();
            }
        }
    }
}
