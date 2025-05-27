using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private float movementX;

    // Animator parameter hashes
    private readonly int isWalkingHash = Animator.StringToHash("IsWalking");
    private readonly int moveXHash = Animator.StringToHash("MoveX");
    private readonly int isGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int jumpHash = Animator.StringToHash("Jump");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        rb.gravityScale = 1f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger(jumpHash);
        }

        animator.SetFloat(isWalkingHash, Mathf.Abs(movementX) > 0 ? 1f : 0f);
        animator.SetFloat(moveXHash, movementX);
        animator.SetBool(isGroundedHash, isGrounded);

        if (movementX != 0)
        {
            bool flip = movementX < 0;
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                if (sr != null && !sr.gameObject.name.Contains("Leg"))
                    sr.flipX = flip;
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementX * moveSpeed, rb.linearVelocity.y);
    }
}