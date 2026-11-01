using UnityEngine;
using UnityEngine.Tilemaps;

public class HazardousTrash : MonoBehaviour
{
    private Tilemap groundTilemap;
    private Vector3Int cellPosition;

    void Start()
    {
        groundTilemap = FindFirstObjectByType<Tilemap>();

        if (groundTilemap != null)
        {
            // Pilin dünya konumuna denk gelen zemin karesini (hücreyi) buluyoruz
            cellPosition = groundTilemap.WorldToCell(transform.position);

            // Renk değiştirebilmek için bayrağı kaldırıyoruz
            groundTilemap.SetTileFlags(cellPosition, TileFlags.None);
            
            // Sadece o spesifik kareyi koyulaştırıyoruz (çölleşme)
            groundTilemap.SetColor(cellPosition, new Color(0.3f, 0.3f, 0.3f));
        }
    }

    void OnDestroy()
    {
        // Pil toplandığında veya yok olduğunda sadece o kare eski rengine döner
        if (groundTilemap != null)
        {
            groundTilemap.SetColor(cellPosition, Color.white);
        }
    }
}