using UnityEngine;

public class GameMenuHandler : MonoBehaviour
{
    void Update()
    {
        // Klavyeden ESC tuşuna basıldığında
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Oyundan çıkılıyor...");
            Application.Quit(); // Oyunu kapatır (Build alındığında çalışır)
        }
    }
}