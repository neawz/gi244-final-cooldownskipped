using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject HUDPanel;
    [SerializeField] private TextMeshProUGUI p1ScoreText;
    [SerializeField] private TextMeshProUGUI p2ScoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pausePanel;

    [Header("Settings")]
    [SerializeField] private float gameTime = 60f;

    [Header("Game Over")]
    [SerializeField] private GameObject summarizePanel;
    [SerializeField] private TextMeshProUGUI p1FinalScoreText;
    [SerializeField] private TextMeshProUGUI p2FinalScoreText;
    [SerializeField] private TextMeshProUGUI winnerText;

    private float timeRemaining;
    private bool isGameOver = false;

    public static UIManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        timeRemaining = gameTime;

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (isGameOver) return;

        HandleTimer();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (GameManager.GetInstance() != null)
        {
            p1ScoreText.text = GameManager.GetInstance().player1Score.ToString();
            p2ScoreText.text = GameManager.GetInstance().player2Score.ToString();
        }
    }

    private void HandleTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            Time.timeScale = 0f;
            isGameOver = true;
            ShowSummarize();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TogglePause()
    {
        bool isPaused = !pausePanel.activeSelf;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void ShowSummarize()
    {
        Time.timeScale = 0f;
        HUDPanel.SetActive(false);
        summarizePanel.SetActive(true);

        int p1 = GameManager.GetInstance().player1Score;
        int p2 = GameManager.GetInstance().player2Score;

        if (p1 > p2)
        {
            winnerText.text = "Player 1 Wins!";
        }
        else if (p2 > p1)
        {
            winnerText.text = "Player 2 Wins!";
        }
        else
        {
            winnerText.text = "It's a Draw!";
        }

        p1FinalScoreText.text = $"{p1}";
        p2FinalScoreText.text = $"{p2}";
    }
}