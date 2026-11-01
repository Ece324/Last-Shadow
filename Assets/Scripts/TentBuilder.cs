using UnityEngine;

public class TentBuilder : MonoBehaviour
{
    public GameObject tentPrefab; // İnşa edilecek çadır prefab'ı
    public int tentCost = 30;     // Çadır kurmanın maliyeti (Eco-puan)
    [HideInInspector]
public bool hasRescuedAnimal = false; // Hayvan taşıyor mu?

[HideInInspector]
public int rescuedCatCount = 0; // Peşimizdeki toplam kedi sayısı

    void Update()
    {
        // 'T' tuşuna bastığında çadır kurmayı denesin (İstediğin tuşu değiştirebilirsin)
        if (Input.GetKeyDown(KeyCode.T))
        {
            TryBuildTent();
        }
    }

    void TryBuildTent()
    {
        if (tentPrefab == null)
        {
            Debug.LogWarning("Çadır Prefab atanmamış!");
            return;
        }

        // GameManager üzerinden puan kontrolü ve harcama yapıyoruz
        if (GameManager.Instance != null)
        {
            // Eğer puanımız yetiyorsa
            if (GameManager.Instance.SpendScore(tentCost))
            {
                // Karakterin biraz önünde (sağ tarafında veya olduğu yerde) çadırı oluştur
                Vector2 spawnPos = (Vector2)transform.position + new Vector2(1.5f, 0f);
                
                Instantiate(tentPrefab, spawnPos, Quaternion.identity);
                Debug.Log("Çadır başarıyla inşa edildi!");
            }
        }
    }
}