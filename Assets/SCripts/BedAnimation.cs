using UnityEngine;

public class BedAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInRange;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("SleepTrigger");
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = true;
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}