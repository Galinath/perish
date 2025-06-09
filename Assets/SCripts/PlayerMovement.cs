using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats stats; // ScriptableObject for stats
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private DialogueBox dialogueBox; // Reference to DialogueBox

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private float movementX;
    private bool isGrounded;
    private float coyoteTimer;
    private float jumpBufferTimer;
    private bool jumpRequested;
    private float targetVelocityX;

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
        rb.gravityScale = stats.gravityScale;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        coyoteTimer = stats.coyoteTime;
    }

    void Update()
    {
        // Skip input processing if dialogue is active
        if (dialogueBox != null && dialogueBox.IsDialogueActive)
        {
            movementX = 0f;
            animator.SetFloat(isWalkingHash, 0f);
            animator.SetFloat(moveXHash, 0f);
            return;
        }

        // Input handling
        movementX = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, stats.groundCheckRadius, groundLayer);

        // Coyote time
        if (isGrounded)
            coyoteTimer = stats.coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;

        // Jump buffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = stats.jumpBufferTime;
            jumpRequested = true;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        // Jump logic
        if (jumpRequested && coyoteTimer > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, stats.jumpForce);
            animator.SetTrigger(jumpHash);
            jumpRequested = false;
            coyoteTimer = 0f;
            jumpBufferTimer = 0f;
        }

        // Variable jump height
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * stats.jumpCutMultiplier);
        }

        // Animation updates
        animator.SetFloat(isWalkingHash, Mathf.Abs(movementX) > 0 ? 1f : 0f);
        animator.SetFloat(moveXHash, movementX);
        animator.SetBool(isGroundedHash, isGrounded);

        // Sprite flipping
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
    if (dialogueBox != null && dialogueBox.IsDialogueActive)
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        return;
    }

    float acceleration = isGrounded ? stats.acceleration : stats.acceleration * stats.airControl;
    float deceleration = isGrounded ? stats.deceleration : stats.deceleration * stats.airControl;
    float maxSpeed = stats.moveSpeed;

    targetVelocityX = movementX * maxSpeed;
    float currentVelocityX = rb.linearVelocity.x;
    float newVelocityX;

    // Check for wall collisions
    Vector2 direction = new Vector2(movementX, 0).normalized;
    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, groundLayer);
    if (hit.collider != null && Mathf.Abs(movementX) > 0)
    {
        // If hitting a wall, prevent pushing into it
        newVelocityX = 0f;
    }
    else if (Mathf.Abs(movementX) > 0)
    {
        newVelocityX = Mathf.MoveTowards(currentVelocityX, targetVelocityX, acceleration * Time.fixedDeltaTime);
    }
    else
    {
        newVelocityX = Mathf.MoveTowards(currentVelocityX, 0f, deceleration * Time.fixedDeltaTime);
    }

    rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);

   
}

    // Optional: Visualize ground check in editor
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, stats.groundCheckRadius);
        }
    }
}
