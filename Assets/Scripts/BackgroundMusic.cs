using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    [Header("Oyun Sahnesi Müziği")]
    public AudioClip gameplayMusic; // SampleScene'e geçince çalacak müzik
    private AudioSource audioSource;

    void Awake()
    {
        // Bu müziğin sahneler arası geçişte yok olmamasını ve kopyalanmamasını sağlar
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Yeni bir sahne yüklendiğinde otomatik çalışır
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Eğer açılan sahne oyun sahnesiyse ve farklı bir müzik atandıysa müziği değiştir
        if (scene.name == "SampleScene")
        {
            if (gameplayMusic != null && audioSource.clip != gameplayMusic)
            {
                audioSource.clip = gameplayMusic;
                audioSource.volume = 0.3f; // Ses seviyesi (0.0 ile 1.0 arası, örn: %30 ses)
                audioSource.Play();
            }
        }
    }
}