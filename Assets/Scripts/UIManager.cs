using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI catCountText;
    public TextMeshProUGUI warningText; 

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        // Oyun açılır açılmaz başlangıç değerlerini ekrana yazdıralım
        UpdateScore(0);
        UpdateCatCount(0);
        if (warningText != null) warningText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null) scoreText.text = "Eko-Puan: " + score;
    }

    public void UpdateCatCount(int count)
    {
        if (catCountText != null) catCountText.text = "Peşindeki Kedi: " + count;
    }

    public void ShowWarning(string message, float duration = 3f)
    {
        if (warningText != null)
        {
            StopAllCoroutines();
            StartCoroutine(WarningRoutine(message, duration));
        }
    }

    private System.Collections.IEnumerator WarningRoutine(string msg, float time)
    {
        warningText.text = msg;
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        warningText.gameObject.SetActive(false);
    }
}