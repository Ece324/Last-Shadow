using UnityEngine;

public class CatCompass : MonoBehaviour
{
    private Transform player;
    private Transform targetCat;
    public SpriteRenderer arrowSpriteRenderer; // Ok görseli

    void Start()
    {
        GameObject p = GameObject.Find("MCharacter");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        // En yakın ve henüz kurtarılmamış kediyi bulalım
        FindClosestCat();

        if (targetCat != null && arrowSpriteRenderer != null)
        {
            arrowSpriteRenderer.enabled = true;

            // Kediye doğru olan açı hesaplanır
            Vector2 direction = (targetCat.position - player.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            // Ok oyuncunun etrafında yönü gösterecek şekilde döndürülür
            transform.position = player.position + (Vector3)(direction * 1.0f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else if (arrowSpriteRenderer != null)
        {
            arrowSpriteRenderer.enabled = false; // Kedi kalmadıysa oku gizle
        }
    }

    void FindClosestCat()
    {
        RescueAnimal[] cats = FindObjectsByType<RescueAnimal>(FindObjectsSortMode.None);
        float minDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (RescueAnimal cat in cats)
        {
            // Sadece yabani (henüz alınmamış) kedileri hedef al
            if (cat.currentState == RescueAnimal.State.Wild)
            {
                float dist = Vector2.Distance(player.position, cat.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = cat.transform;
                }
            }
        }
        targetCat = closest;
    }
}