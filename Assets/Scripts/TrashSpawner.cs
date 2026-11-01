using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trashPrefab; // Üretilecek çöp prefab'ı
    public float spawnInterval = 4f; // Kaç saniyede bir çöp çıksın?
    
    // Haritada çöplerin çıkabileceği alan sınırları (X ve Y koordinatları)
    public float minX = -6f, maxX = 6f;
    public float minY = -3f, maxY = 3f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTrash();
            timer = 0f;
        }
    }

    void SpawnTrash()
{
    Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    
    // Quaternion.identity yerine prefab'ın kendi rotasyonunu kullanıyoruz:
    Instantiate(trashPrefab, randomPosition, trashPrefab.transform.rotation);
}
}