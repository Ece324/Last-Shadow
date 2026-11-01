using UnityEngine;

public class HeatWaveManager : MonoBehaviour
{
    public static HeatWaveManager Instance;

    [Header("Sıcak Hava Dalgası Süreleri")]
    public float heatWaveInterval = 45f; // Dalgaya kalan normal süre
    public float heatWaveDuration = 15f; // Dalganın ekranda kalma/sürme süresi
    private float currentTimer;
    private float heatWaveTimer;
    public bool isHeatWaveActive = false;

    [Header("Yukarıdan Aşağıya Kayan Kırmızı Dalga (Çizgi) Paneli")]
    public RectTransform heatWaveSweepPanel; 
    private float startY = 600f;  
    private float endY = -600f;   
    private float currentY;

    [Header("Hasar Ayarları")]
    public float damageCooldown = 1.5f; 
    private float damageTimer = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentTimer = heatWaveInterval;
        if (heatWaveSweepPanel != null)
        {
            heatWaveSweepPanel.gameObject.SetActive(false);
            currentY = startY;
        }
    }

    void Update()
    {
        if (!isHeatWaveActive)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0f)
            {
                TriggerHeatWave();
            }
        }
        else
        {
            // Sıcak hava dalgası aktifken kendi süresi geri sayar
            heatWaveTimer -= Time.deltaTime;
            if (heatWaveTimer <= 0f)
            {
                ResolveHeatWave(); // Süresi bitince dalga kendiliğinden biter
            }

            // Dalga aşağıya doğru kaymaya devam eder
            if (heatWaveSweepPanel != null)
            {
                currentY -= Time.deltaTime * 450f; 
                
                if (currentY < endY)
                {
                    currentY = startY;
                }

                Vector2 pos = heatWaveSweepPanel.anchoredPosition;
                pos.y = currentY;
                heatWaveSweepPanel.anchoredPosition = pos;
            }

            // Hasar kontrolü: Sadece dışarıdaysak canımız azalır
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0f)
            {
                CheckPlayerHitByWave();
                damageTimer = damageCooldown;
            }
        }
    }

    void CheckPlayerHitByWave()
    {
        GameObject player = GameObject.Find("MCharacter");
        if (player != null)
        {
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            
            // Eğer oyuncunun sprite'ı AÇIKSA (yani çadırda DEĞİLSE) ve ışın ortadan geçiyorsa hasar alır
            if (sr != null && sr.enabled)
            {
                if (Mathf.Abs(currentY) < 150f) 
                {
                    if (PlayerHealth.Instance != null)
                    {
                        PlayerHealth.Instance.TakeDamage(15); 
                    }
                }
            }
            // Not: Eğer sr.enabled KAPALIYSA (yani çadırın içindeyse), kod buraya girer ama canın azalamaz, güvendesin!
        }
    }

    void TriggerHeatWave()
    {
        isHeatWaveActive = true;
        heatWaveTimer = heatWaveDuration; // Dalga süresini başlat
        Debug.Log("⚠️ DİKKAT: Sıcak Hava Dalgası Başladı!");

        if (heatWaveSweepPanel != null)
        {
            heatWaveSweepPanel.gameObject.SetActive(true);
            currentY = startY; 
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowWarning("SICAK HAVA DALGASI! Çadıra sığın ve dalganın geçmesini bekle!", 6f);
        }
    }

    public void ResolveHeatWave()
    {
        isHeatWaveActive = false;
        currentTimer = heatWaveInterval; // Yeni dalga için ana sayacı sıfırla

        if (heatWaveSweepPanel != null)
        {
            heatWaveSweepPanel.gameObject.SetActive(false);
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowWarning("Sıcak hava dalgası geçti. Hava normale döndü.", 3f);
        }
        
        Debug.Log("Sıcak hava dalgası bitti.");
    }

    public void AccelerateHeatWave(float amount)
    {
        currentTimer -= amount;
        Debug.Log("Karbon Salınımı Arttı! Kalan süre: " + Mathf.Max(0, Mathf.Round(currentTimer)) + " saniye.");
        
        if (UIManager.Instance != null && !isHeatWaveActive)
        {
            UIManager.Instance.ShowWarning("⚠️ Karbon salınımı arttı! Sıcak dalgası yaklaşıyor!", 2.5f);
        }
    }
}