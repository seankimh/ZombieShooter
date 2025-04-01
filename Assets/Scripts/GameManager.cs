using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject startScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI scoreText;
    public Button startButton;
    public Button restartButton;

    private int score = 0;
    private bool gameStarted = false;
    private bool gameOver = false;

    private FPSController playerController;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Time.timeScale = 0f;
    }
    void Start()
    {
        LockCursor(false);

        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);

        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        UpdateScoreUI();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1f;
        gameStarted = true;

        LockCursor(true);
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Final Score: " + score;

        LockCursor(false);
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int amount)
    {
        if (!gameStarted || gameOver) return;

        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void LockCursor(bool lockIt)
    {
        Cursor.lockState = lockIt ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockIt;

        if (playerController != null)
            playerController.SetCanMove(lockIt);
    }

}
