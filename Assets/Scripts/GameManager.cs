using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configuração da fase")]
    public int targetEnemies = 10;   // meta de inimigos destruídos (usaremos na próxima sprint)
    public float levelTime = 60f;    // tempo total da fase em segundos

    [Header("Referências de UI")]
    public TMP_Text killsText;
    public TMP_Text timerText;
    public TMP_Text messageText;

    public int EnemiesDestroyed { get; private set; }

    private float remainingTime;
    private bool isGameOver;

    private void Awake()
    {
        // Singleton simples
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Se depois tivermos várias cenas (menu/jogo) podemos usar:
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        remainingTime = levelTime;
        EnemiesDestroyed = 0;

        if (messageText != null)
            messageText.gameObject.SetActive(false);

        AtualizarHUD();
    }

    private void Update()
    {
        if (isGameOver)
            return;

        // Atualiza o cronômetro
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            DerrotaPorTempo();
        }

        AtualizarTimerUI();
    }

    public void RegistrarInimigoDestruido()
    {
        if (isGameOver)
            return;

        EnemiesDestroyed++;
        AtualizarKillsUI();

        // Aqui futuramente vamos checar vitória (quando tiver linha de chegada, etc.)
    }

    private void AtualizarHUD()
    {
        AtualizarKillsUI();
        AtualizarTimerUI();
    }

    private void AtualizarKillsUI()
    {
        if (killsText != null)
        {
            killsText.text = $"Inimigos: {EnemiesDestroyed}/{targetEnemies}";
        }
    }

    private void AtualizarTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"Tempo: {remainingTime:0.0}s";
        }
    }

    private void MostrarMensagem(string mensagem)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = mensagem;
        }
    }

    public void DerrotaPorTempo()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        MostrarMensagem("DERROTA! Tempo esgotado.");
        Time.timeScale = 0f; // pausa o jogo
    }

    // Função de vitória que vamos usar na próxima sprint
    public void Vitoria()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        MostrarMensagem("VITÓRIA!");
        Time.timeScale = 0f;
    }

    // Podemos usar esta futuramente para colisão com inimigos/asteroides
    public void DerrotaPorColisao()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        MostrarMensagem("DERROTA! Nave destruída.");
        Time.timeScale = 0f;
    }
}
