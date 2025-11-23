using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text difficultyLabel;
    public TMP_Text rankingText;

    // 0 = Fácil, 1 = Médio, 2 = Difícil
    private int selectedDifficulty = 1;

    private void Start()
    {
        selectedDifficulty = PlayerPrefs.GetInt("Difficulty", 1);
        AtualizarDifficultyLabel();
        AtualizarRanking();
    }

    public void SelecionarFacil()
    {
        selectedDifficulty = 0;
        PlayerPrefs.SetInt("Difficulty", selectedDifficulty);
        AtualizarDifficultyLabel();
    }

    public void SelecionarMedio()
    {
        selectedDifficulty = 1;
        PlayerPrefs.SetInt("Difficulty", selectedDifficulty);
        AtualizarDifficultyLabel();
    }

    public void SelecionarDificil()
    {
        selectedDifficulty = 2;
        PlayerPrefs.SetInt("Difficulty", selectedDifficulty);
        AtualizarDifficultyLabel();
    }

    public void Jogar()
    {
        Debug.Log("BOTÃO JOGAR CLICADO");

        PlayerPrefs.SetInt("Difficulty", selectedDifficulty);
        PlayerPrefs.Save();

        // 👉 carrega pela POSIÇÃO no Build Settings (índice 1)
        // 0 = MainMenu, 1 = SampleScene
       SceneManager.LoadScene("SampleScene");
    }

    private void AtualizarDifficultyLabel()
    {
        if (difficultyLabel == null) return;

        string nome = selectedDifficulty switch
        {
            0 => "Fácil",
            1 => "Médio",
            2 => "Difícil",
            _ => "Médio"
        };

        difficultyLabel.text = $"Dificuldade: {nome}";
    }

    private void AtualizarRanking()
    {
        if (rankingText == null) return;

        int bestEasy   = PlayerPrefs.GetInt("BestScore_0", 0);
        int bestMedium = PlayerPrefs.GetInt("BestScore_1", 0);
        int bestHard   = PlayerPrefs.GetInt("BestScore_2", 0);

        rankingText.text =
            "Ranking (melhor nº de inimigos destruídos)\n" +
            $"Fácil:   {bestEasy}\n" +
            $"Médio:   {bestMedium}\n" +
            $"Difícil: {bestHard}";
    }
}
