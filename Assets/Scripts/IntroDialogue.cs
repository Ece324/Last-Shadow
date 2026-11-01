using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 10)]
    public string[] dialogueLines; 

    [Header("Karakter Portreleri")]
    public GameObject catPortrait;   // Kedi portresi objesi
    public GameObject humanPortrait; // İnsan portresi objesi

    public float textSpeed = 0.05f; 
    private int index = 0;

    void Start()
    {
        dialogueText.text = string.Empty;
        UpdatePortrait(); // İlk satıra göre portreyi ayarla
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueText.text == dialogueLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        UpdatePortrait();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            UpdatePortrait(); // Sonraki satıra göre portreyi güncelle
            StartCoroutine(TypeLine());
        }
        else
        {
            SceneManager.LoadScene("SampleScene"); // Oyun sahnesine geçiş
        }
    }

    // Hangi satırda olduğumuza göre hangi portrenin görüneceğini belirleyen fonksiyon
    void UpdatePortrait()
    {
        // 0, 1 ve 2 numaralı satırlarda kedi konuşur (index < 3)
        if (catPortrait != null) 
            catPortrait.SetActive(index < 3);

        // 3 numaralı son satırda insan konuşur (index >= 3)
        if (humanPortrait != null) 
            humanPortrait.SetActive(index >= 3);
    }
}