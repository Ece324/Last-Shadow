using UnityEngine;

public class RescueAnimal : MonoBehaviour
{
    public enum State { Wild, Following, SafeInTent }
    public State currentState = State.Wild;

    private Transform targetTransform;
    public float followSpeed = 4f;
    public float followDistance = 1.3f;

    void Start()
    {
        GameObject player = GameObject.Find("MCharacter");
        if (player != null) targetTransform = player.transform;
    }

    void Update()
    {
        if (currentState == State.Following && targetTransform != null)
        {
            float distance = Vector2.Distance(transform.position, targetTransform.position);
            if (distance > followDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, followSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState == State.Wild && (collision.CompareTag("Player") || collision.name.Contains("MCharacter")))
        {
            TentBuilder builder = collision.GetComponent<TentBuilder>();
            if (builder != null)
            {
                builder.rescuedCatCount++; // Peşimizdeki kedi sayısını artırıyoruz
                currentState = State.Following;

                Collider2D col = GetComponent<Collider2D>();
                if (col != null) col.enabled = false; 

                // Arayüzdeki kedi sayacını anında güncelliyoruz
                if (UIManager.Instance != null)
                {
                    UIManager.Instance.UpdateCatCount(builder.rescuedCatCount);
                }

                Debug.Log("Bir kedi daha ekibe katıldı! Toplam: " + builder.rescuedCatCount);
            }
        }
    }

    public void PlaceInTent(Vector3 tentPos, int index)
    {
        currentState = State.SafeInTent;
        Vector3 offset = new Vector3((index - 1) * 0.35f, 0f, 0f); 
        transform.position = tentPos + offset;
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.sortingOrder = 5 + index;
    }
}