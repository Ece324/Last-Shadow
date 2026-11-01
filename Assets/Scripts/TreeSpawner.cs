using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject[] treePrefabs; // Farklı ağaç türleri (prefab olarak)
    public int totalTreesToSpawn = 6; // Haritada çıkacak toplam ağaç sayısı
    
    [Header("Ağaç Doğma Alanı Sınırı")]
    public BoxCollider2D spawnArea; // Sınır için kullanacağımız BoxCollider2D

    void Start()
    {
        SpawnRandomTrees();
    }

    void SpawnRandomTrees()
    {
        if (treePrefabs == null || treePrefabs.Length == 0 || spawnArea == null) return;

        Bounds bounds = spawnArea.bounds;

        for (int i = 0; i < totalTreesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, treePrefabs.Length);
            GameObject selectedTree = treePrefabs[randomIndex];

            // Belirtilen kutunun sınırları içinde rastgele X ve Y koordinatları seçiyoruz
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            GameObject spawnedTree = Instantiate(selectedTree, spawnPos, Quaternion.identity);
            
            // Derinlik algısı için Sprite sıralaması (Y koordinatına göre öncelik)
            SpriteRenderer sr = spawnedTree.GetComponent<SpriteRenderer>();
            if (sr != null) 
            {
                sr.sortingOrder = Mathf.RoundToInt(-spawnPos.y * 10f); 
            }
        }
    }
}