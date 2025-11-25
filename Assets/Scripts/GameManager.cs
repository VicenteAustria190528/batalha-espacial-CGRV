using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Configuração da fase")]
    public int targetEnemies = 10;
    public float levelTime = 60f;

    [Header("Referências de UI")]
    public TMP_Text killsText;
    public TMP_Text timerText;
    public TMP_Text messageText;

    [Header("Menu de fim de jogo")]
    public GameObject endMenuPanel;

    public int EnemiesDestroyed { get; private set; }

    private float remainingTime;
    private bool isGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        int diff = PlayerPrefs.GetInt("Difficulty", 1);

        switch (diff)
        {
            case 0: targetEnemies = 5; break;
            case 1: targetEnemies = 5; break;
            case 2: targetEnemies = 10; break;
        }

        remainingTime = levelTime;
        EnemiesDestroyed = 0;

        if (messageText != null)
            messageText.gameObject.SetActive(false);

        if (endMenuPanel != null)
            endMenuPanel.SetActive(false);

        AtualizarHUD();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.R))
                ReiniciarFase();

            if (Input.GetKeyDown(KeyCode.M))
                VoltarParaMenu();
        }

        if (isGameOver)
            return;

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
        if (isGameOver) return;

        EnemiesDestroyed++;
        AtualizarKillsUI();
    }

    private void AtualizarHUD()
    {
        AtualizarKillsUI();
        AtualizarTimerUI();
    }

    private void AtualizarKillsUI()
    {
        if (killsText != null)
            killsText.text = $"Inimigos: {EnemiesDestroyed}/{targetEnemies}";
    }

    private void AtualizarTimerUI()
    {
        if (timerText != null)
            timerText.text = $"Tempo: {remainingTime:0.0}s";
    }

    private void MostrarMensagem(string msg)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = msg;
        }

        if (endMenuPanel != null)
            endMenuPanel.SetActive(true);
    }


    public void DerrotaPorTempo()
    {
        if (isGameOver) return;
        isGameOver = true;

        Time.timeScale = 0f;
        MostrarMensagem("DERROTA! Tempo esgotado.");
    }

    public void DerrotaPorColisao()
    {
        if (isGameOver) return;
        isGameOver = true;

        Time.timeScale = 0f;
        MostrarMensagem("DERROTA!");
    }

    public void Vitoria()
    {
        if (isGameOver) return;
        isGameOver = true;

        SalvarMelhorPontuacao();

        Time.timeScale = 0f;
        MostrarMensagem("VITÓRIA!");
    }

    public void ChecarVitoriaAoChegarNoFim()
    {
        if (isGameOver) return;

        if (EnemiesDestroyed >= targetEnemies)
            Vitoria();
        else
        {
            MostrarMensagem("Você chegou ao fim, mas não destruiu inimigos suficientes!");
            DerrotaPorColisao();
        }
    }

    private void SalvarMelhorPontuacao()
{
    int diff = PlayerPrefs.GetInt("Difficulty", 1);

    string key = $"BestScore_{diff}";

    int best = PlayerPrefs.GetInt(key, 0);

    if (EnemiesDestroyed > best)
    {
        PlayerPrefs.SetInt(key, EnemiesDestroyed);
        PlayerPrefs.Save();
        Debug.Log($"Novo recorde salvo! Dif: {diff}  Score: {EnemiesDestroyed}");
    }
}


    public void ReiniciarFase()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

  public void VoltarParaMenu()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu 1");
}

}
