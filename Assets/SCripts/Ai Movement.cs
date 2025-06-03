using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;
    private Vector2 startPos;
    private bool movingRight = true;
    private Animator animator;
    private static readonly int SpeedParam = Animator.StringToHash("Speed");

    private void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float move = movingRight ? speed : -speed;
        transform.Translate(Vector2.right * move * Time.deltaTime);

        if (animator != null)
            animator.SetFloat(SpeedParam, speed); 

        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = movingRight ? Mathf.Abs(newScale.x) : -Mathf.Abs(newScale.x);
        transform.localScale = newScale;
    }

 
}