using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        int finalScore = ScoreManager.Instance.GetScore();
        finalScoreText.text = "Final Score: " + finalScore;
    }

    public void RestartGame()
    {
        ScoreManager.Instance.ResetScore();
        TimerManager.Instance.ResetTimer();

        SceneManager.LoadScene(0);
    }
}