using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public float lifeTime = 4f;

    private Rigidbody2D rb;
    private int damage;
    private bool passThroughWalls;
    private string ownerTag;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        //updates rotation of projectile every frame
        Vector2 v = rb.linearVelocity;

        if (v.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    public void Init(Vector2 direction, float speed, int dmg, bool passesWalls, string ownerTag)
    {
        this.damage = dmg;
        this.passThroughWalls = passesWalls;
        this.ownerTag = ownerTag;

        rb.linearVelocity = direction * speed;

        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ownerTag)) return;

        // Hit enemy/player health
        if (other.TryGetComponent<IDamageable>(out var dmgable))
        {
            dmgable.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Walls
        if (!passThroughWalls && (other.gameObject.layer == LayerMask.NameToLayer("Ground") ||
                                 other.gameObject.layer == LayerMask.NameToLayer("Walls")))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Barrel"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

public interface IDamageable
{
    void TakeDamage(int amount);
}
