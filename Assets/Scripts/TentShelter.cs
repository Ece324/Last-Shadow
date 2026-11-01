using UnityEngine;

public class TentShelter : MonoBehaviour
{
    private bool playerInRange = false;
    private GameObject playerObj;
    private bool isSheltering = false;

    [Header("Çadır Kapasitesi")]
    public int maxCapacity = 3;       
    public int currentCats = 0;       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSheltering && (collision.CompareTag("Player") || collision.name.Contains("MCharacter")))
        {
            playerInRange = true;
            playerObj = collision.gameObject;
            Debug.Log("Çadıra ulaştın! Kedileri teslim etmek ve çadıra girmek için 'E' tuşuna bas.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isSheltering && (collision.CompareTag("Player") || collision.name.Contains("MCharacter")))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (isSheltering)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleShelter();
            }
            return;
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerObj != null)
            {
                TentBuilder builder = playerObj.GetComponent<TentBuilder>();
                
                if (currentCats >= maxCapacity)
                {
                    if (UIManager.Instance != null)
                    {
                        UIManager.Instance.ShowWarning("Bu çadır tamamen dolu! (Max 3 kedi).", 3.5f);
                    }
                    return;
                }

                if (builder != null && builder.rescuedCatCount > 0)
                {
                    RescueAnimal[] allCats = FindObjectsByType<RescueAnimal>(FindObjectsSortMode.None);
                    int deliveredCount = 0;
                    int spaceLeft = maxCapacity - currentCats;

                    foreach (RescueAnimal cat in allCats)
                    {
                        if (cat.currentState == RescueAnimal.State.Following && deliveredCount < spaceLeft)
                        {
                            cat.PlaceInTent(transform.position, currentCats);
                            currentCats++;
                            deliveredCount++;
                        }
                    }

                    if (deliveredCount > 0)
                    {
                        int earnedPoints = deliveredCount * 50; 
                        if (GameManager.Instance != null)
                        {
                            GameManager.Instance.AddScore(earnedPoints);
                        }

                        builder.rescuedCatCount -= deliveredCount; 
                        
                        if (UIManager.Instance != null)
                        {
                            UIManager.Instance.UpdateCatCount(builder.rescuedCatCount); 
                            UIManager.Instance.ShowWarning(deliveredCount + " kedi çadıra yerleştirildi!", 3.5f);
                        }
                    }
                }

                ToggleShelter();
            }
        }
    }

    void ToggleShelter()
    {
        isSheltering = !isSheltering;
        
        if (playerObj == null) return;

        PlayerMovement moveScript = playerObj.GetComponent<PlayerMovement>();
        SpriteRenderer playerSr = playerObj.GetComponent<SpriteRenderer>();
        Collider2D playerCol = playerObj.GetComponent<Collider2D>();

        if (isSheltering)
        {
            if (moveScript != null) moveScript.enabled = false;
            if (playerSr != null) playerSr.enabled = false;
            if (playerCol != null) playerCol.enabled = false;

            // DİKKAT: Artık çadıra girince dalgayı direkt yok etmiyoruz,
            // sadece sprite kapandığı için CheckPlayerHitByWave() fonksiyonu bize hasar veremiyor.
            // Böylece dalga dışarıda akmaya devam ediyor, biz içeride güvende bekliyoruz.

            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowWarning("Çadıra sığındın! Dalga geçene kadar güvendesin. Çıkmak için 'E' bas.", 4f);
            }
        }
        else
        {
            if (moveScript != null) moveScript.enabled = true;
            if (playerSr != null) playerSr.enabled = true;
            if (playerCol != null) playerCol.enabled = true;

            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowWarning("Çadırdan çıktın.", 2f);
            }
        }
    }
}