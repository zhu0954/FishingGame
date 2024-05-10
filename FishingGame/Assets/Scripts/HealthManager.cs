using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public TMP_Text healthText;  // Use TMP_Text instead of Text
    private int health = 3;  // Initial health value

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateHealthText();
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        UpdateHealthText();

        if (health <= 0)
        {
            RestartGame();
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }

    private void RestartGame()
    {
        // Optionally, show a restart UI or delay the restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
