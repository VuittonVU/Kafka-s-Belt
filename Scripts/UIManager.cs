using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider healthBar;
    public Text scoreText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHealthUI(int current, int max)
    {
        healthBar.maxValue = max;
        healthBar.value = current;
    }

    public void AddScore(int amount)
    {
        scoreText.text = "Score : " + amount;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame() 
    {
        Time.timeScale = 1f; //memastikan tidak dalam keadaan pause
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

