using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Settings")]
    public float smoothSpeed = 0.125f;

    private Vector3 shakeOffset;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset + shakeOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }


    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            
            shakeOffset = new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Reset everything when done
        shakeOffset = Vector3.zero;
    }
}