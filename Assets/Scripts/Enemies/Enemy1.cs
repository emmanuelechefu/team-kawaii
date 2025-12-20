using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy1 : EnemyBase
{
    public float speed = 2f;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;
        float dir = Mathf.Sign(player.position.x - transform.position.x);
        if (Mathf.Abs(player.position.x - transform.position.x) < 13f)
            rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
    }
}
