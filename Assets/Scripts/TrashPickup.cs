using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpan nesne bizim karakterimiz mi kontrol ediyoruz
        if (collision.CompareTag("Player") || collision.name.Contains("MCharacter"))
        {
            Debug.Log("Atık toplandı! Çevre bir adım daha temizlendi.");
            
            // İleride burada temizlenen atık sayacını artırıp dünyayı yeşerteceğiz
            
            // Atığı sahneden kaldır
            Destroy(gameObject);
        }
    }
}