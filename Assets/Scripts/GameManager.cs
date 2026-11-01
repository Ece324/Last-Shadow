using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int ecoScore = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        ecoScore += amount;
        Debug.Log("Puan Eklendi! Toplam Puan: " + ecoScore);

        // UIManager üzerinden ekrandaki puanı güncelliyoruz
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(ecoScore);
        }
    }

    public bool SpendScore(int amount)
    {
        if (ecoScore >= amount)
        {
            ecoScore -= amount;
            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdateScore(ecoScore);
            }
            return true;
        }
        return false;
    }
}