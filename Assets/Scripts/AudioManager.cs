using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ses Kaynakları (AudioSource)")]
    public AudioSource sfxSource; // Alarm, rüzgar, hasar sesleri için
    public AudioSource musicSource; // Arka plan müziği için

    [Header("Ses Klipleri (AudioClip)")]
    public AudioClip heatWaveAlarmClip; // Sıcak hava dalgası alarm sesi
    public AudioClip damageClip;        // Can azalma sesi

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Sıcak hava dalgası başladığında bu fonksiyonu çağırabiliriz
    void PlayHeatWaveSound()
    {
        if (sfxSource != null && heatWaveAlarmClip != null)
        {
            sfxSource.PlayOneShot(heatWaveAlarmClip);
        }
    }
}