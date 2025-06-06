using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private DialogueBox dialogueBox; // Reference to DialogueBox

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

        rb.gravityScale = 1f; // Fixed typo: was "gravity hullScale"
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Only process input if dialogue is not active
        if (dialogueBox != null && dialogueBox.IsDialogueActive)
        {
            movementX = 0f; // Prevent horizontal movement
            animator.SetFloat(isWalkingHash, 0f); // Stop walking animation
            animator.SetFloat(moveXHash, 0f);
            return; // Skip the rest of Update
        }

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
    // Only apply movement if dialogue is not active
    if (dialogueBox == null || !dialogueBox.IsDialogueActive)
    {
        rb.linearVelocity = new Vector2(movementX * moveSpeed, rb.linearVelocity.y);

        // Debug raycast to detect horizontal collisions
        if (movementX != 0) // Only cast ray when moving horizontally
        {
            Vector2 direction = new Vector2(movementX, 0).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, groundLayer);
            if (hit.collider != null)
            {
                Debug.Log("Horizontal Hit: " + hit.collider.gameObject.name + " at position: " + hit.point);
            }
        }

        // Debug ground check to detect what the groundCheck is hitting
        Collider2D[] groundHits = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundHits.Length > 0)
        {
            foreach (Collider2D hit in groundHits)
            {
                Debug.Log("Ground Check Hit: " + hit.gameObject.name + " at position: " + groundCheck.position);
            }
        }
    }
}
}