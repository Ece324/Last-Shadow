using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Animator animator; // 1. Animator değişkenini tanımladık

    // Çöp taşıma durumu:
    public int trashCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // 2. Animator bileşenini burada bağladık
    }

    void Update()
{
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");

    // Animatöre hareket parametrelerini gönderiyoruz
    if (animator != null)
    {
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
    }
}

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Yerdeki çöpe değdiğimizde çalışır
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Trash"))
    {
        trashCount++; // Her çöpte sayacı 1 artırıyoruz
        Debug.Log("Çöp toplandı! Toplanan Çöp Sayısı: " + trashCount);
        Destroy(collision.gameObject); 
    }
}
}