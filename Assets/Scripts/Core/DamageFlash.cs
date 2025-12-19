using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    public Image flashImage;
    public AudioClip damageSound;

    public float fadeSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f);

    private AudioSource audioSource;

    void Awake()
    {
        if (flashImage == null) flashImage = GetComponent<Image>();
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (flashImage != null) flashImage.color = Color.clear;
    }

    public void Flash()
    {
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (flashImage != null)
        {
            StopAllCoroutines();
            StartCoroutine(DoFlash());
        }
    }

    IEnumerator DoFlash()
    {
        flashImage.color = flashColor;
        while (flashImage.color.a > 0.01f)
        {
            flashImage.color = Color.Lerp(flashImage.color, Color.clear, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        flashImage.color = Color.clear;
    }
}