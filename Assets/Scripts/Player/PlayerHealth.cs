using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHP = 3;
    public int hp = 3;

    public float knockbackForce = 8f;
    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.3f;

    private Rigidbody2D rb;
    private DamageFlash flashScript;
    private CameraFollow camScript;

    void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            camScript = FindFirstObjectByType<CameraFollow>();
            flashScript = FindFirstObjectByType<DamageFlash>();
            if (camScript == null) camScript = Camera.main.GetComponent<CameraFollow>();
        }

    public void TakeDamage(int amount)
        {
            hp = Mathf.Max(0, hp - amount);

            if (camScript != null)
            {
                camScript.Shake(shakeDuration, shakeAmount);
            }

            if (flashScript != null)
            {
                flashScript.Flash();
            }

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(Vector2.up * knockbackForce, ForceMode2D.Impulse);
            }

            if (hp <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    public void Heal(int amount)
    {
        hp = Mathf.Min(maxHP, hp + amount);
    }
}
