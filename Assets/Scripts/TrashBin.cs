using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public GameObject treePrefab; // Dikilecek ağaç prefab'ı
    public float spawnAreaWidth = 6f; // Ağacın çıkabileceği yatay alan

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.name.Contains("MCharacter"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                if (player.trashCount > 0)
                {
                    int totalPoints = player.trashCount * 10; // Topladığı çöp başına 10 puan!
                    
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.AddScore(totalPoints);
                    }

                    Debug.Log(player.trashCount + " adet çöp geri dönüştürüldü! Kazanılan Puan: " + totalPoints);
                    
                    player.trashCount = 0; // Kutuya atıldığı için oyuncunun elindeki çöpler sıfırlanır
                    
                    // Ağaç dikme fonksiyonunu tetikleyelim
                    SpawnTree();
                }
                else
                {
                    Debug.Log("Elinde çöp yok! Önce yerdeki çöpleri toplamalısın.");
                }
            }
        }
    }

    void SpawnTree()
    {
        if (treePrefab != null)
        {
            // Çöp kutusunun çevresinde rastgele bir x koordinatında ağaç üret
            float randomX = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            Vector2 treePosition = new Vector2(transform.position.x + randomX, transform.position.y - 1.5f);
            
            Instantiate(treePrefab, treePosition, Quaternion.identity);
        }
    }
}