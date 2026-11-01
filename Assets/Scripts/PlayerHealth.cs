using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Header("Can Ayarları")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI (İsteğe Bağlı Can Barı Slider)")]
    public Slider healthSlider;

    [Header("Game Over Paneli")]
    public GameObject gameOverPanel; // "Başarısız Oldun" yazısının olduğu panel

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Oyun başladığında Game Over panelinin kapalı olduğundan emin olalım
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        UpdateHealthUI();
        Debug.Log("Hasar Alındı! Kalan Can: " + currentHealth);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowWarning("⚠️ Sıcak hava dalgasına yakalandın! Canın azaldı!", 2f);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Oyuncu hayatını kaybetti! Oyun yeniden başlatılıyor...");

        // 1. Ekranda Game Over panelini (Başarısız Oldun yazısını) aç
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 2. 2 saniye bekleyip sahneyi yeniden başlat
        StartCoroutine(RestartAfterDelay(2f));
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Oyun dondurulduysa bile doğru sayar
        Time.timeScale = 1f; // Zamanı normale döndür
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Sahneyi baştan başlat
    }
}