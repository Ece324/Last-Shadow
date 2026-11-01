using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public GameObject[] catPrefabs; 
    public int totalCatsToSpawn = 4; // Haritada çıkacak toplam kedi sayısı
    
    [Header("Doğma Alanı Sınırı")]
    public BoxCollider2D spawnArea; // Unity Inspector'dan o yeşil sınırlı alanı buraya sürükleyeceğiz

    public Vector3 catScale = new Vector3(1.6f, 1.6f, 1f);

    void Start()
    {
        SpawnRandomCats();
    }

    void SpawnRandomCats()
    {
        if (catPrefabs == null || catPrefabs.Length == 0 || spawnArea == null) return;

        // BoxCollider2D'nin dünya üzerindeki min ve max sınırlarını alıyoruz
        Bounds bounds = spawnArea.bounds;

        for (int i = 0; i < totalCatsToSpawn; i++)
        {
            int randomIndex = Random.Range(0, catPrefabs.Length);
            GameObject selectedCat = catPrefabs[randomIndex];

            // Sınırlar içinde tamamen rastgele bir X ve Y noktası seçiyoruz
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            GameObject spawnedCat = Instantiate(selectedCat, spawnPos, Quaternion.identity);
            spawnedCat.transform.localScale = catScale;
            
            SpriteRenderer sr = spawnedCat.GetComponent<SpriteRenderer>();
            if (sr != null) sr.sortingOrder = 3;
        }
    }
}