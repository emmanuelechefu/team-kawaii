using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 7f;
    public float jumpForce = 12f;

    private bool grounded;
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundMask;

    Rigidbody2D rb;
    Animator anim;
    bool jumpQueued;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Jump = W (Space is shooting)
        if (Input.GetKeyDown(KeyCode.W)) jumpQueued = true;

        // Sets Animation triggers based on state
        anim.SetBool("grounded", grounded);
        if (Mathf.Abs(rb.linearVelocity.x) > 0) 
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);
    }

    void FixedUpdate()
    {
        float x = 0f;
        if (Input.GetKey(KeyCode.A)) x = -1f;
        if (Input.GetKey(KeyCode.D)) x =  1f;

        // Moves player based on x and move speed
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

         // Flip player depending on movement :P
        if (x > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (x < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        // applies an upwards force to the player
        if (jumpQueued && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        jumpQueued = false;
    }

    // checks if the player is on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Barrel")
        {
            grounded = true;
        }
    }
}
