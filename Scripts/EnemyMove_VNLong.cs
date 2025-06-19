using UnityEngine;

public class GoombaMove_VNLong : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = false;
    private bool isDead = false;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (isDead) return;

        rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.linearVelocity.y);

        bool groundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        bool wallAhead = Physics2D.Raycast(wallCheck.position, movingRight ? Vector2.right : Vector2.left, 0.1f, groundLayer);

        if (!groundAhead || wallAhead)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.collider.CompareTag("Player"))
        {
            Transform player = collision.collider.transform;

            float playerBottom = player.position.y - (player.localScale.y / 1f);
            float goombaTop = transform.position.y + (transform.localScale.y / 1f);

            bool stomped = playerBottom > goombaTop - 0.1f;

            if (stomped)
            {
                Die();
                Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 8f); // bounce up
                }
            }
            else
            {
                PlayerCollision_VNLong playerScript = collision.collider.GetComponent<PlayerCollision_VNLong>();
                if (playerScript != null)
                {
                    playerScript.Die();
                }
            }
        }
    }


    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
        transform.localScale = new Vector3(transform.localScale.x, 0.3f, transform.localScale.z);

        if (anim != null)
        {
            anim.SetTrigger("GoombaSquash_VNLong");
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.3f, transform.localScale.z);
        }
        Destroy(gameObject, 0.5f);
    }
}
