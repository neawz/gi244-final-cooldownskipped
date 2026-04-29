using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    

    [Header("UI Text Elements")]
    [SerializeField] private TextMeshProUGUI p1ScoreText;
    [SerializeField] private TextMeshProUGUI p2ScoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject summarizePanel;
    [SerializeField] private TextMeshProUGUI winnerText;

    [Header("Settings")]
    [SerializeField] private float gameTime = 60f; 

    private float timeRemaining;
    private bool isGameOver = false;

    private void Awake()
    {
        timeRemaining = gameTime;
    }

    private void Update()
    {
        if (isGameOver) return;

        HandleTimer();

        // ESC Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // ÍÑ»à´µ¤Ðá¹¹¨Ò¡ GameManager
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (GameManager.Instance != null)
        {
            p1ScoreText.text = GameManager.Instance.player1Score.ToString();
            p2ScoreText.text = GameManager.Instance.player2Score.ToString();
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
}