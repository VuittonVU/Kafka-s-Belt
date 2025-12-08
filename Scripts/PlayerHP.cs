using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 3;
    public int currentHP;

    void Start()
    {
        UIManager.Instance.gameOverPanel.SetActive(false);
        currentHP = maxHP;
        UIManager.Instance.UpdateHealthUI(currentHP, maxHP);
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP < 0)
            currentHP = 0;

        UIManager.Instance.UpdateHealthUI(currentHP, maxHP);

        if (currentHP == 0)
        {
            Die();
        }
    }

    void Die()
    {
        UIManager.Instance.ShowGameOver();

        Time.timeScale = 0f;
    }
}
