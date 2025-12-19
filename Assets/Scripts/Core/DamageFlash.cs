using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    public Image flashImage;
    public float fadeSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.5f);

    void Awake()
    {
        if (flashImage == null) flashImage = GetComponent<Image>();
        if (flashImage != null) flashImage.color = Color.clear;
    }

    public void Flash()
    {
        if (flashImage == null) return;
        StopAllCoroutines();
        StartCoroutine(DoFlash());
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