using UnityEngine;

public class CatFollow : MonoBehaviour
{
    public Transform player;          // Takip edilecek oyuncu (MCharacter)
    public float followSpeed = 3.5f;  // Kedinin yürüme hızı (oyuncudan biraz yavaş veya eşit olabilir)
    public float stopDistance = 1.5f; // Oyuncuya çok yaklaşınca durma mesafesi

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Eğer Inspector'dan oyuncu atanmadıysa otomatik bulalım
        if (player == null)
        {
            GameObject pObj = GameObject.Find("MCharacter");
            if (pObj != null)
                player = pObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Kedi ile oyuncu arasındaki mesafe
        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = Vector2.zero;

        if (distance > stopDistance)
        {
            // Oyuncuya doğru pozisyonu hesapla
            Vector2 targetPosition = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
            direction = (targetPosition - (Vector2)transform.position).normalized;

            // Kediyi hareket ettir
            rb.MovePosition(targetPosition);
        }

        // Animatördeki parametrelere yönü bildiriyoruz
        if (animator != null)
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }
}